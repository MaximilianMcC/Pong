using SFML.Graphics;
using SFML.System;
using SFML.Window;

class Game
{
	public RenderWindow Window;
	public float DeltaTime;
	
	public void Run()
	{
		// Create the SFML window
		Window = new RenderWindow(new VideoMode(800, 800), "Pong");
		Window.SetFramerateLimit(60);
		Window.Closed += (sender, e) => Window.Close();

		// Create the delta time clock
		Clock deltaTimeClock = new Clock();


		// Create both the left and right paddles
		Paddle leftPaddle = new Paddle(PaddleType.LEFT, Color.Blue, this);
		Paddle rightPaddle = new Paddle(PaddleType.RIGHT, Color.Red, this);


		// Game
		while (Window.IsOpen)
		{
			// Events and whatnot
			Window.DispatchEvents();
			DeltaTime = deltaTimeClock.Restart().AsSeconds();


			// Update the paddles
			leftPaddle.Update();
			rightPaddle.Update();



			// Clear the window
			Window.Clear(Color.Magenta);
			
			// Draw the paddles
			leftPaddle.Render();
			rightPaddle.Render();

			// Show the new frame
			Window.Display();
		}
	}
}