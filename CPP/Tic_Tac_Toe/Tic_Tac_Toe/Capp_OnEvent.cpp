#include "Capp.h"

void Capp::OnEvent(SDL_Event* Event){
	CEvent::OnEvent(Event);
}

void Capp::OnExit()
{
	Running = false;
}

void Capp::OnLButtonDown(int mx, int my)
{
	//if a game has ended reset the game
	if(GameOver == 1)
	{
		Reset();
		GameOver = 0;
		Winner = 3;
		CurrentPlayer = 0;
		return;
	}

	int ID = mx / 200;
	ID += (my / 200) * 3;

	if(Grid[ID] != GRID_TYPE_NONE)
		return;

	if(CurrentPlayer == 0)
	{
		SetCell(ID, GRID_TYPE_X);
		CurrentPlayer = 1;
	}
	else
	{
		SetCell(ID, GRID_TYPE_O);
		CurrentPlayer = 0;			
	}

	Winner = DetermineWinner();

	if(Winner == 1 || Winner == 2)
		GameOver = 1;
	else
		GameOverTest();//check if the board has been filled
}