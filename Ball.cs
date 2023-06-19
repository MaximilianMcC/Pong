using SFML.Graphics;
using SFML.System;
using SFML.Window;

class Ball
{
	private Game game;
	private float size = 15;
	private RectangleShape sprite;
	private Vector2f position;
	private float speed = 15f;
	private Vector2f direction;

	public Ball(Game game)
	{
		this.game = game;

		// Create the sprite
		this.sprite = new RectangleShape(new Vector2f(size, size));
		sprite.FillColor = new Color(0xf1f1f1ff);
		
		// Put the sprite in the middle of the game
		position = new Vector2f(((game.Window.Size.X - size) / 2), ((game.Window.Size.Y - size) / 2));
		sprite.Position = position;

		// Set the initial direction to start the game
		direction = new Vector2f(speed, -speed);
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
		float movementSpeed = speed * game.DeltaTime;
		Vector2f newPosition = position;

		// Move the ball using the velocity
		newPosition += (direction * movementSpeed);

		// Update the position
		position = newPosition;
		sprite.Position = position;



		// Check for collision with the top boundary
		// TODO: Put this in a method
		if (newPosition.Y < 0 || newPosition.Y + size > game.Window.Size.Y)
		{
			// Reverse/flip/mirror the direction on the y
			direction.Y = -direction.Y;
		}


		// Check for if the ball collides with the left paddle
		if (sprite.GetGlobalBounds().Intersects(game.LeftPaddle.Bounds))
		{
			// Reverse/flip/mirror the direction on the x
			direction.X = -direction.X;
		}

		// Check for if the ball collides with the right paddle
		if (sprite.GetGlobalBounds().Intersects(game.RightPaddle.Bounds))
		{
			// Reverse/flip/mirror the direction on the x
			direction.X = -direction.X;
		}
	}
}