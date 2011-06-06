#include <SDL.h>

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

