using SFML.Graphics;
using SFML.System;
using SFML.Window;

class Ball
{
	private Game game;
	private float size = 15;
	private RectangleShape sprite;
	private Vector2f position;
	private Vector2f direction;
	private float speed = 15f;
	private float speedMultiplier;

	public Ball(Game game)
	{
		this.game = game;

		// Create the sprite
		this.sprite = new RectangleShape(new Vector2f(size, size));
		sprite.FillColor = new Color(0xf1f1f1ff);
	}

	public void Update()
	{
		Movement();
		Collision();
	}

	public void Render()
	{
		game.Window.Draw(sprite);
	}



	public void SpawnBall()
	{
		// Put the sprite in the middle of the game
		position = new Vector2f(((game.Window.Size.X - size) / 2), ((game.Window.Size.Y - size) / 2));
		sprite.Position = position;

		// Create a random "launch" vector that will make the ball
		// travel to the top-left, top-right, bottom-left, or bottom-right
		speedMultiplier = 1f;
		Random random = new Random();

		// Decide if its going right/up (true) or down/left (false)
		bool x = random.NextSingle() >= 0.5;
		bool y = random.NextSingle() >= 0.5;

		// Create the vector
		float left = x ? speed : -speed;
		float top = y ? speed : -speed;
		direction = new Vector2f(left, top) * speedMultiplier;
	}


	private void Movement()
	{
		float movementSpeed = (speed * speedMultiplier) * game.DeltaTime;
		Vector2f newPosition = position;

		// Move the ball using the velocity
		newPosition += (direction * movementSpeed);

		// Update the position
		position = newPosition;
		sprite.Position = position;
	}

	// Check for if the ball is going to hit something, and reflect the ball if it is
	private void Collision()
	{
		// Check for collision on the window and paddles
		//TODO: Don't write the flip and speed change every time. Put in method
		{
			// Check for collision on the top/bottom of the screen
			if ((position.Y < 0) || ((position.Y + size) > game.Window.Size.Y))
			{
				// Reverse/flip/mirror the direction on the y
				direction.Y = -direction.Y;
				speedMultiplier += 0.08f;
			}

			// Check for if the ball collides with the left paddle
			if (sprite.GetGlobalBounds().Intersects(game.LeftPaddle.Bounds))
			{
				// Reverse/flip/mirror the direction on the x
				direction.X = -direction.X;
				speedMultiplier += 0.08f;
			}

			// Check for if the ball collides with the right paddle
			if (sprite.GetGlobalBounds().Intersects(game.RightPaddle.Bounds))
			{
				// Reverse/flip/mirror the direction on the x
				direction.X = -direction.X;
				speedMultiplier += 0.08f;
			}
		}

		// Check for collision on the x for if the game is over
		{
			// The ball hits the left side
			if (position.X < 0)
			{
				game.NewGame(PaddleType.RIGHT);
			}

			// The ball hits the right side
			if ((position.X + size) > game.Window.Size.X)
			{
				game.NewGame(PaddleType.LEFT);
			}
		}

	}
}