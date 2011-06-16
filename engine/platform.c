#include <SDL.h>
#include <SDL_cpuinfo.h>

#include "definitions.h"

CALL_API void setWindowTitle(char *caption) {
	//Sets are window Caption
  SDL_WM_SetCaption(caption,NULL);
}

SDL_Surface *surface;

CALL_API int setWindow(char *caption,int width,int height,int fullscreen) {
  Uint32 flags = SDL_OPENGL;
  
	setWindowTitle(caption);

  //Sets the sdl video mode width and height as well has creates are opengl context.
	if(fullscreen!=0) {
		flags = SDL_OPENGL|SDL_FULLSCREEN;
	}
	surface = SDL_SetVideoMode(width,height,32,flags);
  if (!surface) {
    printf("SDL_SetVideoMode failed: %s\n",SDL_GetError());
    return 0;
  }
	return 1;
}

CALL_API int getWidth(void) {
	return surface ? surface->w : -1;
}

CALL_API int getHeight(void) {
	return surface ? surface->h : -1;
}

CALL_API int getTime(void) {
	return SDL_GetTicks();
}

CALL_API void swapBuffers(void) {
  SDL_GL_SwapBuffers();
}

CALL_API char *getPlatform(void) {
  char *ret = malloc(32);
  #if __AIX__
      sprintf( ret, "%s", "AIX" );
  #elif __HAIKU__
  /* Haiku must appear here before BeOS, since it also defines __BEOS__ */
      sprintf( ret, "%s", "Haiku" );
  #elif __BEOS__
      sprintf( ret, "%s", "BeOS" );
  #elif __BSDI__
      sprintf( ret, "%s", "BSDI" );
  #elif __DREAMCAST__
      sprintf( ret, "%s", "Dreamcast" );
  #elif __FREEBSD__
      sprintf( ret, "%s", "FreeBSD" );
  #elif __HPUX__
      sprintf( ret, "%s", "HP-UX" );
  #elif __IRIX__
      sprintf( ret, "%s", "Irix" );
  #elif __LINUX__
      sprintf( ret, "%s", "Linux" );
  #elif __MINT__
      sprintf( ret, "%s", "Atari MiNT" );
  #elif __MACOS__
      sprintf( ret, "%s", "MacOS Classic" );
  #elif __MACOSX__
      sprintf( ret, "%s", "Mac OS X" );
  #elif __NETBSD__
      sprintf( ret, "%s", "NetBSD" );
  #elif __NDS__
      sprintf( ret, "%s", "Nintendo DS" );
  #elif __OPENBSD__
      sprintf( ret, "%s", "OpenBSD" );
  #elif __OS2__
      sprintf( ret, "%s", "OS/2" );
  #elif __OSF__
      sprintf( ret, "%s", "OSF/1" );
  #elif __QNXNTO__
      sprintf( ret, "%s", "QNX Neutrino" );
  #elif __RISCOS__
      sprintf( ret, "%s", "RISC OS" );
  #elif __SOLARIS__
      sprintf( ret, "%s", "Solaris" );
  #elif __WIN32__
  #ifdef _WIN32_WCE
      sprintf( ret, "%s", "Windows CE" );
  #else
      sprintf( ret, "%s", "Windows" );
  #endif
  #elif __IPHONEOS__
      sprintf( ret, "%s", "iPhone OS" );
  #else
      sprintf( ret, "%s", "Unknown (see SDL_platform.h)" );
  #endif
      return ret;
}

CALL_API char *getCpuFlags(void) {
  char *cad = malloc(1024);
  sprintf(cad,
    "{'3DNow':%d,'AltiVec':%d,'MMX':%d,'RDTSC':%d,'SSE':%d,'SSE2':%d}",
    SDL_Has3DNow(),
    SDL_HasAltiVec(),
    SDL_HasMMX(),
    SDL_HasRDTSC(),
    SDL_HasSSE(),
    SDL_HasSSE2()
  );
  return cad;
}

char *handle_mouse_event(SDL_Event *event,const char *type) { 
  char *cad = malloc(1024);
  sprintf(cad,"%s:{'x':%d,'y':%d,'xrel':%d,'yrel':%d}",
    type,
    event->motion.x,
    event->motion.y,
    event->motion.xrel,
    event->motion.yrel
  );
  return cad;
}

char *handle_mousebutton_event( SDL_Event *event, const char *type ) {
  // FIXME: need free this memory
  char *cad = malloc(1024);
  sprintf(cad,"%s:{'x':%d,'y':%d,'state':%d,'button':%d}",
    type,
    event->button.x,
    event->button.y,
    event->button.state,
    event->button.button
  );
  return cad;
}

char *handle_keyboard_event( SDL_Event *event, const char *type ) {
  char *cad = malloc(1024);
  sprintf(cad,"%s:{'state':%d,'scancode':%d,'unicode':%d,'sym':%d,'mod':%d}",
    type,
    event->key.state,
    event->key.keysym.scancode,
    event->key.keysym.unicode,
    event->key.keysym.sym,
    event->key.keysym.mod
  );
  return cad;
}

CALL_API char *checkEvents(void) {
  //Creates Our Event Reciver
  SDL_Event event;
  char *ret;
  //Checks if there is a event that needs processing
  if( SDL_PollEvent( &event ) ) {  
    switch( event.type ) {
      case SDL_QUIT:
        ret = malloc(8);
        sprintf( ret, "%s", "quit" );
        return ret;
      case SDL_KEYDOWN:
        return handle_keyboard_event( &event, "onkeydown" );
      case SDL_KEYUP:
        return handle_keyboard_event( &event, "onkeyup" );
      case SDL_MOUSEMOTION:
        return handle_mouse_event( &event, "onmousemove" );
      case SDL_MOUSEBUTTONDOWN:
        return handle_mousebutton_event( &event, "onmousedown" );
      case SDL_MOUSEBUTTONUP:
        return handle_mousebutton_event( &event, "onmouseup" );
      case SDL_JOYAXISMOTION:
      case SDL_JOYHATMOTION:
      case SDL_JOYBALLMOTION:
      case SDL_JOYBUTTONDOWN:
      case SDL_JOYBUTTONUP:
        return NULL;
      default:
        return NULL;
    }
  } else {
    return NULL;
  }
}

char *extern_relaunch = NULL;

CALL_API void setRelaunchAssembly(char *assembly) {
  printf("%s\n",assembly);
  extern_relaunch = malloc(1024);
  sprintf(extern_relaunch,"%s",assembly);
}






