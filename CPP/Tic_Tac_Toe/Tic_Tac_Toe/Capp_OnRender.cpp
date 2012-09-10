#include "Capp.h"

void Capp::OnRender(){
	CSurface::OnDraw(Surface_Display, Surface_Grid, 0, 0);

	for(int i = 0; i < 9; i++)
	{
		int X = (i % 3) * 200;
		int Y = (i / 3) * 200;

		if(Grid[i] == GRID_TYPE_X)
			CSurface::OnDraw(Surface_Display, Surface_X, X, Y);
		else if(Grid[i] == GRID_TYPE_O)
			CSurface::OnDraw(Surface_Display, Surface_O, X, Y);
	}

	if(GameOver == 1)
		switch(Winner)
	{
		case 1:
			CSurface::OnDraw(Surface_Display, Surface_XWin, 70, 160);
			break;
		case 2:
			CSurface::OnDraw(Surface_Display, Surface_OWin, 70, 160);
			break;
		case 3:
			CSurface::OnDraw(Surface_Display, Surface_GameDraw, 70, 160);
			break;			
	}
	
	SDL_Flip(Surface_Display);
}