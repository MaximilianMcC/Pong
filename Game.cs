using SFML.Graphics;
using SFML.System;
using SFML.Window;

class Game
{
	public RenderWindow Window;
	public float DeltaTime;
	public Paddle LeftPaddle;
	public Paddle RightPaddle;
	
	public void Run()
	{
		// Create the SFML window
		Window = new RenderWindow(new VideoMode(800, 800), "Pong", Styles.Close);
		Window.SetFramerateLimit(60);
		Window.Closed += (sender, e) => Window.Close();

		// Create the delta time clock
		Clock deltaTimeClock = new Clock();

		// Create both paddles
		LeftPaddle = new Paddle(PaddleType.LEFT, this);
		RightPaddle = new Paddle(PaddleType.RIGHT, this);

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
			LeftPaddle.Update();
			RightPaddle.Update();
			ball.Update();


			// Clear the window
			Window.Clear(new Color(0x202124ff));
			
			// Draw the paddles
			LeftPaddle.Render();
			RightPaddle.Render();
			ball.Render();

			// Draw the line down the middle
			DrawLine();

			// Show the new frame
			Window.Display();
		}
	}




	private void DrawLine()
	{
		// Make the rectangle line thing to draw
		int dashCount = 51;
		float size = (Window.Size.Y / dashCount);
		float y = (size / 2);

		RectangleShape dash = new RectangleShape(new Vector2f(size, size));
		dash.Origin = new Vector2f((dash.Size.X / 2), ((dash.Size.Y / 2)));
		dash.FillColor = new Color(0x17181aff);

		for (int i = 0; i < dashCount; i++)
		{
			dash.Position = new Vector2f((Window.Size.X / 2), y);
			Window.Draw(dash);
			y += size * 2;
		}
	}
}