#include "CSurface.h"

CSurface::CSurface()
{
}

SDL_Surface *CSurface::OnLoad(char *File)
{
	SDL_Surface *Surf_Temp = NULL;
	SDL_Surface *Surf_Return = NULL;

	if((Surf_Temp = SDL_LoadBMP(File)) == NULL)
		return NULL;

	Surf_Return = SDL_DisplayFormat(Surf_Temp);
	SDL_FreeSurface(Surf_Temp);

	return Surf_Return;
}

bool CSurface::OnDraw(SDL_Surface *Surface_Dest, SDL_Surface *Surface_Src, int x, int y)
{
	if(Surface_Dest == NULL || Surface_Src == NULL)
		return false;

	SDL_Rect DestR;

	DestR.x = x;
	DestR.y = y;

	SDL_BlitSurface(Surface_Src, NULL, Surface_Dest, &DestR);

	return true;
}

bool CSurface::OnDraw(SDL_Surface *Surface_Dest, SDL_Surface *Surface_Src, int x, int y, int x2, int y2, int w, int h)
{
	if(Surface_Dest == NULL || Surface_Src == NULL)
		return false;

	SDL_Rect DestR;

	DestR.x = x;
	DestR.y = y;

	SDL_Rect SrcR;
	
	SrcR.x = x2;
	SrcR.y = y2;
	SrcR.h = h;
	SrcR.w = w;

	SDL_BlitSurface(Surface_Src, &SrcR, Surface_Dest, &DestR);

	return true;
}

bool CSurface::Transparent(SDL_Surface *Surface_Dest, int R, int G, int B)
{
	if(Surface_Dest == NULL)
		return false;

	SDL_SetColorKey(Surface_Dest, SDL_SRCCOLORKEY | SDL_RLEACCEL, SDL_MapRGB(Surface_Dest->format, R, G, B));
	
	return true;
}