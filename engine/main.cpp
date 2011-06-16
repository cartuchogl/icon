#include <mono/jit/jit.h>
#include <mono/metadata/mono-config.h> 
#include <mono/metadata/object.h>
#include <mono/metadata/environment.h>
#include <mono/metadata/assembly.h>
#include <stdlib.h>

#include <SDL.h>

#include "definitions.h"
#include "platform.h"

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

extern char *extern_relaunch;

int main(int argc, char* argv[]) {
  MonoDomain *domain;
  const char *file,*filetmp;
  int retval;

  if (argc < 2) {
    fprintf (stderr, "Please provide an assembly to load.\n");
    return 1;
  }
  file = argv [1];
  
  #ifdef _MONO_PATH
  mono_set_dirs(_MONO_PATH_LIB,_MONO_PATH_ETC);
  #endif

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

  while(file!=NULL) {
    filetmp = file;
    file = NULL;
    // Inits sdl with only the video extension.
    if (SDL_Init(SDL_INIT_VIDEO) != 0) {
      printf("SDL_Init failed: %s\n", SDL_GetError());
      return 0;
    }
    printf("%dms to dotNet.\n", getTime());
    main_function (domain, filetmp, argc - 1, argv + 1);
    // Crashed when executing ruby tests
    // mono_jit_cleanup(domain);
    SDL_Quit();
    retval = mono_environment_exitcode_get ();
    printf("%s\n",file);
    file = extern_relaunch;
    printf("%s\n",file);
    extern_relaunch = NULL;
    printf("%s\n",file);
  }
  return retval;
}
