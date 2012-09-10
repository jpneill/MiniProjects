#include "Capp.h"

void Capp::OnCleanup(){
	SDL_FreeSurface(Surface_Display);
	SDL_FreeSurface(Surface_Grid);
	SDL_FreeSurface(Surface_X);
	SDL_FreeSurface(Surface_O);
	SDL_FreeSurface(Surface_XWin);
	SDL_FreeSurface(Surface_OWin);
	SDL_FreeSurface(Surface_GameDraw);
	SDL_Quit();
}