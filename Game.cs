using SFML.Graphics;
using SFML.System;
using SFML.Window;

class Game
{
	public RenderWindow Window;
	
	public void Run()
	{
		// Create the SFML window
		Window = new RenderWindow(new VideoMode(800, 800), "Pong");
		Window.SetFramerateLimit(60);
		Window.Closed += (sender, e) => Window.Close();


		// Create the delta time clock
		Clock deltaTimeClock = new Clock();

		// Game
		while (Window.IsOpen)
		{
			// Events and whatnot
			Window.DispatchEvents();




			// Clear the window
			Window.Clear(Color.Magenta);
			
			// Show the new frame
			Window.Display();
		}
	}
}