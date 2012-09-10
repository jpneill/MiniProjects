#ifndef _CSURFACE_H
#define _CSURFACE_H

#include "SDL.h"

class CSurface
{
public: CSurface();
		static SDL_Surface *OnLoad(char *File);
		static bool OnDraw(SDL_Surface *Surface_Dest, SDL_Surface *Surface_Src, int x, int y);
		static bool OnDraw(SDL_Surface *Surface_Dest, SDL_Surface *Surface_Src, int x, int y, int x2, int y2, int w, int h);
		static bool Transparent(SDL_Surface *Surface_Dest, int R, int G, int B);
};

#endif