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
		Window = new RenderWindow(new VideoMode(800, 800), "Pong", Styles.Close);
		Window.SetFramerateLimit(60);
		Window.Closed += (sender, e) => Window.Close();

		// Create the delta time clock
		Clock deltaTimeClock = new Clock();

		// Create both paddles
		Paddle leftPaddle = new Paddle(PaddleType.LEFT, this);
		Paddle rightPaddle = new Paddle(PaddleType.RIGHT, this);

		// Create the ball
		Ball ball = new Ball(this);
		ball.Begin();

		// Game
		while (Window.IsOpen)
		{
			// Events and whatnot
			Window.DispatchEvents();
			DeltaTime = deltaTimeClock.Restart().AsSeconds();


			// Update the paddles and ball
			leftPaddle.Update();
			rightPaddle.Update();
			ball.Update();


			// Clear the window
			Window.Clear(new Color(0x202124ff));
			
			// Draw the paddles
			leftPaddle.Render();
			rightPaddle.Render();
			ball.Render();

			// Show the new frame
			Window.Display();
		}
	}
}