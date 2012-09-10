#ifndef _CAPP_H
	#define _CAPP_H

#include "SDL.h"
#include "CSurface.h"
#include "CEvent.h"

class Capp : public CEvent
{
private: bool Running;
		 
		 SDL_Surface *Surface_Display;
		 SDL_Surface *Surface_Grid;
		 SDL_Surface *Surface_X;
		 SDL_Surface *Surface_O;
		 SDL_Surface *Surface_XWin;
		 SDL_Surface *Surface_OWin;
		 SDL_Surface *Surface_GameDraw;
		 
		 int Grid[9];

		 enum {
			GRID_TYPE_NONE = 0,
			GRID_TYPE_X,
			GRID_TYPE_O
		 };

public: Capp();
		
		int OnExecute();
		int CurrentPlayer;
		int GameOver;
		int Winner;
		int EndCondition;
		
		bool OnInit();
		
		void OnEvent(SDL_Event* Event);
		void OnExit();
		void OnLButtonDown(int mx, int my);
		void OnLoop();
		void OnRender();
		void OnCleanup();
		void Reset();
		void SetCell(int ID, int Type);
		int DetermineWinner();
		void GameOverTest();
};

#endif