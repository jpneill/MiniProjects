#include "Capp.h"

bool Capp::OnInit(){
	if(SDL_Init(SDL_INIT_EVERYTHING)<0)
		return false;	

	if((Surface_Display = SDL_SetVideoMode(600, 600, 32, SDL_HWSURFACE | SDL_DOUBLEBUF)) == NULL)
		return false;

	if((Surface_Grid = CSurface::OnLoad("./gfx/grid.bmp"))==NULL)
		return false;

	if((Surface_X = CSurface::OnLoad("./gfx/x.bmp"))==NULL)
		return false;

	if((Surface_O = CSurface::OnLoad("./gfx/o.bmp"))==NULL)
		return false;

	if((Surface_XWin = CSurface::OnLoad("./gfx/Xwins.bmp"))==NULL)
		return false;

	if((Surface_OWin = CSurface::OnLoad("./gfx/Owins.bmp"))==NULL)
		return false;

	if((Surface_GameDraw = CSurface::OnLoad("./gfx/Draw.bmp"))==NULL)
		return false;

	CSurface::Transparent(Surface_X, 255, 0, 255);
	CSurface::Transparent(Surface_O, 255, 0, 255);

	Reset();

	return true;
}