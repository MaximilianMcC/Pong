using SFML.Graphics;
using SFML.System;
using SFML.Window;

class Paddle
{
	private Game game;
	private RectangleShape sprite;
	private float position;
	private float x;
	private float speed = 727f;
	private PaddleType paddleType;
	private float width = 25f;
	private float height = 200f;

	// Create a new paddle
	public Paddle(PaddleType paddleType, Game game)
	{
		this.game = game;
		this.paddleType = paddleType;

		// Create the sprite
		this.sprite = new RectangleShape(new Vector2f(width, height));

		// Set the position
		if (paddleType == PaddleType.LEFT) this.x = 0;
		else if (paddleType == PaddleType.RIGHT) this.x = game.Window.Size.X - width;
	}

	public void Update()
	{
		Movement();
	}

	public void Render()
	{
		game.Window.Draw(sprite);
	}





	private void Movement()
	{
		/*
		2 years ago. 45 degree.
		mirror.
		*/

		// Movement stuff
		float newPosition = position;
		float movement = speed * game.DeltaTime;

		// Get input depending on the paddle type
		if (paddleType == PaddleType.LEFT)
		{
			if(Keyboard.IsKeyPressed(Keyboard.Key.W)) newPosition -= movement;
			if(Keyboard.IsKeyPressed(Keyboard.Key.S)) newPosition += movement;
		}
		else if (paddleType == PaddleType.RIGHT)
		{
			if(Keyboard.IsKeyPressed(Keyboard.Key.Up)) newPosition -= movement;
			if(Keyboard.IsKeyPressed(Keyboard.Key.Down)) newPosition += movement;
		}

		// Update the position
		if (!Collision(newPosition)) this.position = newPosition;
		this.sprite.Position = new Vector2f(x, position);

	}

	private bool Collision(float newPosition)
	{
		// Check for if the paddle is about to hit the window
		if ((newPosition < 0) || (newPosition + height) > game.Window.Size.Y) return true;

		return false;
	}

}









enum PaddleType
{
	LEFT,
	RIGHT
}