#include "Capp.h"

Capp::Capp(){
	CurrentPlayer = 0;
	GameOver = 0;
	Winner = 3;
	EndCondition = 0;

	Surface_Display = NULL;
	Surface_X = NULL;
	Surface_O = NULL;
	Surface_Grid = NULL;
	Surface_XWin = NULL;
	Surface_OWin = NULL;
	Surface_GameDraw = NULL;

	Running = true;
}

int Capp::OnExecute()
{
	if(OnInit()==false)
		return -1;
	
	SDL_Event Event;
	
	while(Running){
		while(SDL_PollEvent(&Event)){
			OnEvent(&Event);
		}
		OnLoop();
		OnRender();
	}

	OnCleanup();

	return 0;
}

void Capp::Reset()
{
	for(int i = 0; i < 9; i++)
		Grid[i] = GRID_TYPE_NONE;
}

void Capp::SetCell(int ID, int Type)
{
	if(ID < 0 || ID >= 9)
		return;

	if(Type < 0 || Type > GRID_TYPE_O)
		return;

	Grid[ID] = Type;
}

int Capp::DetermineWinner()
{
	int result, Square_One, Square_Two, Square_Three;
	result = 3;
	Square_One = 0;
	Square_Two = 0;
	Square_Three = 0;

	//check first row
	Square_One = Grid[0];
	Square_Two = Grid[1];
	Square_Three = Grid[2];
	if(Square_One!=0)
		if(Square_One == Square_Two)
			if(Square_Two == Square_Three)
				result = Square_One;

	//check second row
	Square_One = Grid[3];
	Square_Two = Grid[4];
	Square_Three = Grid[5];
	if(Square_One!=0)
		if(Square_One == Square_Two)
			if(Square_Two == Square_Three)
				result = Square_One;

	//check third row
	Square_One = Grid[6];
	Square_Two = Grid[7];
	Square_Three = Grid[8];
	if(Square_One!=0)
		if(Square_One == Square_Two)
			if(Square_Two == Square_Three)
				result = Square_One;

	//Check first column
	Square_One = Grid[0];
	Square_Two = Grid[3];
	Square_Three = Grid[6];
	if(Square_One!=0)
		if(Square_One == Square_Two)
			if(Square_Two == Square_Three)
				result = Square_One;

	//check second column
	Square_One = Grid[1];
	Square_Two = Grid[4];
	Square_Three = Grid[7];
	if(Square_One!=0)
		if(Square_One == Square_Two)
			if(Square_Two == Square_Three)
				result = Square_One;

	//check third column
	Square_One = Grid[2];
	Square_Two = Grid[5];
	Square_Three = Grid[8];
	if(Square_One!=0)
		if(Square_One == Square_Two)
			if(Square_Two == Square_Three)
				result = Square_One;

	//check first diagonal
	Square_One = Grid[0];
	Square_Two = Grid[4];
	Square_Three = Grid[8];
	if(Square_One!=0)
		if(Square_One == Square_Two)
			if(Square_Two == Square_Three)
				result = Square_One;

	//check second diagonal
	Square_One = Grid[2];
	Square_Two = Grid[4];
	Square_Three = Grid[6];
	if(Square_One!=0)
		if(Square_One == Square_Two)
			if(Square_Two == Square_Three)
				result = Square_One;

	return result;
}

void Capp::GameOverTest()
{	
	EndCondition = 0;

	for(int i = 0; i < 9; i++)
	{
		if(Grid[i] != GRID_TYPE_NONE)
			EndCondition++;
	}

	if(EndCondition == 9)
		GameOver = 1;
}

int main(int argc, char* args[])
{
	Capp theApp;

	return theApp.OnExecute();
}