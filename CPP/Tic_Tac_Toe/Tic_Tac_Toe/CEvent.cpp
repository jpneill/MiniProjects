#include "CEvent.h"

CEvent::CEvent()
{
}

CEvent::~CEvent(){}

void CEvent::OnEvent(SDL_Event *Event)
{
	switch(Event->type)
	{

	case SDL_ACTIVEEVENT:
		{
			switch(Event->active.state)
			{
			case SDL_APPMOUSEFOCUS:
				{
					if(Event->active.gain)	OnMouseFocus();
					else OnMouseBlur();
					break;
				}
			case SDL_APPINPUTFOCUS:
				{
					if(Event->active.gain) OnInputFocus();
					else OnInputBlur();
					break;
				}
			case SDL_APPACTIVE:
				{
					if(Event->active.gain) OnRestore();
					else OnMinimize();
					break;
				}
			}
			break;
		}

	case SDL_KEYDOWN:
		{
			OnKeyDown(Event ->key.keysym.sym, Event ->key.keysym.mod, Event ->key.keysym.unicode);
			break;
		}

	case SDL_KEYUP:
		{
			OnKeyUp(Event ->key.keysym.sym, Event ->key.keysym.mod, Event ->key.keysym.unicode);
			break;
		}

	case SDL_MOUSEMOTION:
		{
			OnMouseMove(Event->motion.x, Event->motion.y, Event->motion.xrel, Event->motion.yrel, (Event->motion.state &SDL_BUTTON(SDL_BUTTON_LEFT))!=0, (Event->motion.state &SDL_BUTTON(SDL_BUTTON_RIGHT))!=0, (Event->motion.state &SDL_BUTTON(SDL_BUTTON_MIDDLE))!=0);
			break;
		}

	case SDL_MOUSEBUTTONDOWN:
		{
			switch(Event->button.button)
			{
			case SDL_BUTTON_LEFT:
				{
					OnLButtonDown(Event->button.x, Event->button.y);
					break;
				}
			case SDL_BUTTON_RIGHT:
				{
					OnRButtonDown(Event->button.x, Event->button.y);
					break;
				}
			case SDL_BUTTON_MIDDLE:
				{
					OnMButtonDown(Event->button.x, Event->button.y);
					break;
				}
			}
			break;
		}

	case SDL_MOUSEBUTTONUP:
		{
			switch(Event->button.button)
			{
			case SDL_BUTTON_LEFT:
				{
					OnLButtonUp(Event->button.x, Event->button.y);
					break;
				}
			case SDL_BUTTON_RIGHT:
				{
					OnRButtonUp(Event->button.x, Event->button.y);
					break;
				}
			case SDL_BUTTON_MIDDLE:
				{
					OnMButtonUp(Event->button.x, Event->button.y);
					break;
				}
			}
			break;
		}

	case SDL_QUIT:
		{
			OnExit();
			break;
		}

	case SDL_SYSWMEVENT:		
			break;
		
	case SDL_VIDEORESIZE:
		{
			OnResize(Event->resize.w, Event->resize.h);
			break;
		}

	case SDL_VIDEOEXPOSE:
		{
			OnExpose();
			break;
		}

	default:
		{
			OnUser(Event->user.type, Event->user.code, Event->user.data1, Event->user.data2);
			break;
		}
	}
}

void CEvent::OnInputFocus(){}

void CEvent::OnInputBlur(){}

void CEvent::OnKeyDown(SDLKey sym, SDLMod mod, Uint16 unicode){}

void CEvent::OnKeyUp(SDLKey sym, SDLMod mod, Uint16 unicode){}

void CEvent::OnMouseFocus(){}

void CEvent::OnMouseBlur(){}

void CEvent::OnMouseMove(int mx, int my, int relx, int rely, bool left, bool right, bool middle){}

void CEvent::OnMouseWheel(bool up, bool down){}

void CEvent::OnLButtonDown(int mx, int my){}

void CEvent::OnLButtonUp(int mx, int my){}

void CEvent::OnRButtonDown(int mx, int my){}

void CEvent::OnRButtonUp(int mx, int my){}

void CEvent::OnMButtonDown(int mx, int my){}

void CEvent::OnMButtonUp(int mx, int my){}

void CEvent::OnMinimize(){}

void CEvent::OnRestore(){}

void CEvent::OnResize(int w, int h){}

void CEvent::OnExpose(){}

void CEvent::OnExit(){}

void CEvent::OnUser(Uint8 type, int code, void* data1, void* data2){}