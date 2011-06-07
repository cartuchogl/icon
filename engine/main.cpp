#include <mono/jit/jit.h>
#include <mono/metadata/mono-config.h> 
#include <mono/metadata/object.h>
#include <mono/metadata/environment.h>
#include <mono/metadata/assembly.h>
#include <mono/metadata/debug-helpers.h>
#include <stdlib.h>

#include <Horde3DUtils.h>
#include <SDL.h>

extern int global_quit;

static void fire(MonoDomain *domain, MonoAssembly *assembly, const char *event,const char *arg) {
  MonoImage *i = mono_assembly_get_image(assembly);

  MonoClass *klass=
  mono_class_from_name (i, "GameEngine", "CoreEvents");
  if(klass==NULL){
    printf("not found class\n");
    return;
  }

  MonoMethodDesc* mdesc;
  MonoMethod *vtmethod;
  MonoString *str;
  MonoObject *exception;

  /* A different way to search for a method */
  mdesc = mono_method_desc_new (":fireEvent(string,string)", 0);
  vtmethod = mono_method_desc_search_in_class (mdesc, klass);
  if(vtmethod==NULL){
    printf("not found method\n");
    return;
  }
  void* args [2];
  str = mono_string_new (domain, event);
  args[0] = str;
  args[1] = mono_string_new(domain,arg);
  exception = NULL;
  mono_runtime_invoke (vtmethod, NULL, args, &exception);
  if(exception){
    printf("Exception!!\n");
  }
}

int acum_x = 0;
int acum_y = 0;
int acum_xrel = 0;
int acum_yrel = 0;
int acum = 0; 

void handle_mouse_event(SDL_Event *event,const char *type,MonoDomain *domain, MonoAssembly *assembly) {
  acum = 1;
  acum_x = event->motion.x;
  acum_y = event->motion.y;
  acum_xrel += event->motion.xrel;
  acum_yrel += event->motion.yrel;
}

void fire_mouseacum(MonoDomain *domain, MonoAssembly *assembly) {
  char cad[1024];
  sprintf(cad,"{'x':%d,'y':%d,'xrel':%d,'yrel':%d}",
    acum_x,
    acum_y,
    acum_xrel,
    acum_yrel
  );
  fire(domain,assembly,"onmousemove",cad);
  acum_x = 0;
  acum_y = 0;
  acum_xrel = 0;
  acum_yrel = 0;
  acum = 0;
}

void handle_mousebutton_event (SDL_Event *event, const char *type, MonoDomain *domain, MonoAssembly *assembly) {
  char cad[1024];
  sprintf(cad,"{'x':%d,'y':%d,'state':%d,'button':%d}",
    event->button.x,
    event->button.y,
    event->button.state,
    event->button.button
  );
  fire(domain,assembly,type,cad);
}

void handle_keyboard_event (SDL_Event *event, const char *type, MonoDomain *domain, MonoAssembly *assembly) {
  char cad[1024];
  sprintf(cad,"{'state':%d,'scancode':%d,'unicode':%d,'sym':%d,'mod':%d}",
    event->key.state,
    event->key.keysym.scancode,
    event->key.keysym.unicode,
    event->key.keysym.sym,
    event->key.keysym.mod
  );
  fire(domain,assembly,type,cad);
}

bool check_events (MonoDomain *domain, MonoAssembly *assembly) {
  //Creates Our Event Reciver
  SDL_Event event;
  //Checks if there is a event that needs processing
  while(SDL_PollEvent(&event)) {
    switch(event.type) {
      case SDL_QUIT:
        return false;
      case SDL_KEYDOWN:
        handle_keyboard_event (&event, "onkeydown", domain, assembly); break;
      case SDL_KEYUP:
        handle_keyboard_event (&event, "onkeyup", domain, assembly); break;
      case SDL_MOUSEMOTION:
        handle_mouse_event (&event, "onmousemove", domain, assembly); break;
      case SDL_MOUSEBUTTONDOWN:
        handle_mousebutton_event (&event, "onmousedown", domain, assembly); break;
      case SDL_MOUSEBUTTONUP:
        handle_mousebutton_event (&event, "onmouseup", domain, assembly); break;
      case SDL_JOYAXISMOTION:
      case SDL_JOYHATMOTION:
      case SDL_JOYBALLMOTION:
      case SDL_JOYBUTTONDOWN:
      case SDL_JOYBUTTONUP:
        break;
      default:
        break;
    }
  }
  if(acum) {
    fire_mouseacum (domain, assembly);
  }
  return true;
}

/*
 * Very simple mono embedding example.
 * Compile with: 
 * 	gcc -o teste teste.c `pkg-config --cflags --libs mono-2` -lm
 * 	mcs test.cs
 * Run with:
 * 	./teste test.exe
 */

static MonoAssembly *main_function (MonoDomain *domain, const char *file, int argc, char** argv)
{
  MonoAssembly *assembly;

  assembly = mono_domain_assembly_open (domain, file);
  if (!assembly)
    exit (2);
  /*
  * mono_jit_exec() will run the Main() method in the assembly.
  * The return value needs to be looked up from
  * System.Environment.ExitCode.
  */
  mono_jit_exec (domain, assembly, argc, argv);
  return assembly;
}


int main(int argc, char* argv[]) {
  MonoDomain *domain;
  const char *file;
  int retval;

  bool running = true;

  if (argc < 3) {
    fprintf (stderr, "Please provide two assemblies to load, one with Main and other with GameEngine\n");
    return 1;
  }
  file = argv [1];

  /*
  * Load the default Mono configuration file, this is needed
  * if you are planning on using the dllmaps defined on the
  * system configuration
  */
  mono_config_parse (NULL);
  /*
  * mono_jit_init() creates a domain: each assembly is
  * loaded and run in a MonoDomain.
  */
  domain = mono_jit_init (file);

  //Inits sdl with only the video extention.
  if (SDL_Init(SDL_INIT_VIDEO) != 0) {
    printf("SDL_Init failed: %s\n", SDL_GetError());
    return 0;
  }

  main_function (domain, file, argc - 1, argv + 1);
  MonoAssembly *assembly2;

  assembly2 = mono_domain_assembly_open (domain, argv[2]);
  if (!assembly2)
    exit (2);
  

  h3dInit();
  fire(domain,assembly2,"onpostinit","");
  // Our While loop
  while(running == true) {
    fire(domain,assembly2,"onframe","");
    running = check_events(domain,assembly2);
    if(global_quit) {
      running = false;
    }
    //Swaps the sdl opengl buffers
    SDL_GL_SwapBuffers();
    fire(domain,assembly2,"onendframe","");
  }

  fire(domain,assembly2,"onend","");
  SDL_Delay(200);
  //Releases the Horde3D Engine
  h3dRelease();
  // FIXME: crash on exit when using ironruby
  printf("xxxxxx\n");
  mono_jit_cleanup (domain);
  printf("xxxxxx\n");
  SDL_Quit();
  SDL_Delay(500);
  printf("xxxxxx\n");
  retval = mono_environment_exitcode_get ();
  printf("xxxxxx\n");
  return retval;
}
