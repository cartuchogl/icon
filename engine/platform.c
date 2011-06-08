#include <SDL.h>
#include <SDL_cpuinfo.h>

void setWindowTitle(char *caption) {
	//Sets are window Caption
  SDL_WM_SetCaption(caption,NULL);
}

SDL_Surface *surface;

int setWindow(char *caption,int width,int height,int fullscreen) {
	setWindowTitle(caption);

  //Sets the sdl video mode width and height as well has creates are opengl context.
	Uint32 flags = SDL_OPENGL;
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

int getWidth(void) {
	return surface ? surface->w : -1;
}

int getHeight(void) {
	return surface ? surface->h : -1;
}

int getTime(void) {
	return SDL_GetTicks();
}

int global_quit = 0;

void quit(void) {
	global_quit = 1;
}

int global_reseting = 1;

void reset(void) {
	global_reseting = 1;
}

const char *getPlatform(void) {
  #if __AIX__
      return "AIX";
  #elif __HAIKU__
  /* Haiku must appear here before BeOS, since it also defines __BEOS__ */
      return "Haiku";
  #elif __BEOS__
      return "BeOS";
  #elif __BSDI__
      return "BSDI";
  #elif __DREAMCAST__
      return "Dreamcast";
  #elif __FREEBSD__
      return "FreeBSD";
  #elif __HPUX__
      return "HP-UX";
  #elif __IRIX__
      return "Irix";
  #elif __LINUX__
      return "Linux";
  #elif __MINT__
      return "Atari MiNT";
  #elif __MACOS__
      return "MacOS Classic";
  #elif __MACOSX__
      return "Mac OS X";
  #elif __NETBSD__
      return "NetBSD";
  #elif __NDS__
      return "Nintendo DS";
  #elif __OPENBSD__
      return "OpenBSD";
  #elif __OS2__
      return "OS/2";
  #elif __OSF__
      return "OSF/1";
  #elif __QNXNTO__
      return "QNX Neutrino";
  #elif __RISCOS__
      return "RISC OS";
  #elif __SOLARIS__
      return "Solaris";
  #elif __WIN32__
  #ifdef _WIN32_WCE
      return "Windows CE";
  #else
      return "Windows";
  #endif
  #elif __IPHONEOS__
      return "iPhone OS";
  #else
      return "Unknown (see SDL_platform.h)";
  #endif
}

const char *getCpuFlags(void) {
  char *cad=malloc(1024);
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

