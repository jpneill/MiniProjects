#ifndef _CEVENT_H_
#define _CEVENT_H_

#include "SDL.h"

class CEvent
{
public: CEvent();
		virtual ~CEvent();
		virtual void OnEvent(SDL_Event *Event);
		virtual void OnInputFocus();
		virtual void OnInputBlur();
		virtual void OnKeyDown(SDLKey sym, SDLMod mod, Uint16 unicode);
		virtual void OnKeyUp(SDLKey sym, SDLMod mod, Uint16 unicode);
		virtual void OnMouseFocus();
		virtual void OnMouseBlur();
		virtual void OnMouseMove(int mx, int my, int relx, int rely, bool left, bool right, bool middle);
		virtual void OnMouseWheel(bool up, bool down);
		virtual void OnLButtonDown(int mx, int my);
		virtual void OnLButtonUp(int mx, int my);
		virtual void OnRButtonDown(int mx, int my);
		virtual void OnRButtonUp(int mx, int my);
		virtual void OnMButtonDown(int mx, int my);
		virtual void OnMButtonUp(int mx, int my);
		virtual void OnMinimize();
		virtual void OnRestore();
		virtual void OnResize(int w, int h);
		virtual void OnExpose();
		virtual void OnExit();
		virtual void OnUser(Uint8 type, int code, void *data1, void *data2);
};

#endif