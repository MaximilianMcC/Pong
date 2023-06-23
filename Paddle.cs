using SFML.Graphics;
using SFML.System;
using SFML.Window;

class Paddle
{
	public bool Moving;
	public FloatRect Bounds;
	public float Position;
	public float Height = 200f;

	private Game game;
	private float width = 25f;
	private RectangleShape sprite;
	private float x;
	private float speed = 727f;
	private PaddleType paddleType;

	// Create a new paddle
	public Paddle(PaddleType paddleType, Game game)
	{
		this.game = game;
		this.paddleType = paddleType;

		// Create the sprite
		this.sprite = new RectangleShape(new Vector2f(width, Height));

		// Set the position
		if (paddleType == PaddleType.LEFT) this.x = 0;
		else if (paddleType == PaddleType.RIGHT) this.x = game.Window.Size.X - width;
		ResetPosition();
	}

	public void Update()
	{
		Movement();
	}

	public void Render()
	{
		game.Window.Draw(sprite);
	}



	public void ResetPosition()
	{
		this.Position = (game.Window.Size.Y - Height) / 2;
		this.sprite.Position = new Vector2f(x, Position);
	}

	private void Movement()
	{
		// Movement stuff
		float newPosition = Position;
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

		// Check for if the paddle is moving
		float velocity = (newPosition - Position) / game.DeltaTime;
		Moving = velocity >= 1;

		// Update the position
		if (!Collision(newPosition)) this.Position = newPosition;
		this.sprite.Position = new Vector2f(x, Position);
		this.Bounds = sprite.GetGlobalBounds();
	}

	private bool Collision(float newPosition)
	{
		// Check for if the paddle is about to hit the window
		if ((newPosition < 0) || (newPosition + Height) > game.Window.Size.Y) return true;

		return false;
	}

}









enum PaddleType
{
	LEFT,
	RIGHT
}