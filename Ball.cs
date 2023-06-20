using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML.Audio;

class Ball
{
	private Game game;
	private float size = 15;
	private RectangleShape sprite;
	private Vector2f position;
	private Vector2f direction;
	private float speed = 15f;
	private float speedMultiplier;
	private Sound collisionSound;
	private Sound deathSound;

	public Ball(Game game)
	{
		this.game = game;

		// Create the sprite
		this.sprite = new RectangleShape(new Vector2f(size, size));
		sprite.FillColor = new Color(0xf1f1f1ff);

		// Create the sound effects
		collisionSound = new Sound(new SoundBuffer("./Assets/sounds/collision.wav"));
		deathSound = new Sound(new SoundBuffer("./Assets/sounds/die.wav"));
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
		// Put the sprite in the middle of the game and reset the speed
		position = new Vector2f(((game.Window.Size.X - size) / 2), ((game.Window.Size.Y - size) / 2));
		sprite.Position = position;
		speedMultiplier = 1f;

		// Create a random launch vector that will make the ball
		// travel to the top-left, top-right, bottom-left, or bottom-right
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
		// Calculate movement stuff
		float movementSpeed = (speed * speedMultiplier) * game.DeltaTime;
		Vector2f newPosition = position;

		// Move the ball using the direction vector
		newPosition += (direction * movementSpeed);

		// Update the position
		position = newPosition;
		sprite.Position = position;
	}

	// Check for if the ball is going to hit something, and reflect the ball if it is
	private void Collision()
	{
		// Check for collision on the window and paddles
		{
			// Check for collision on the top/bottom of the screen
			if ((position.Y < 0) || ((position.Y + size) > game.Window.Size.Y))
			{
				// Reverse/flip/mirror the direction on the y
				direction.Y = -direction.Y;
				speedMultiplier += 0.08f;

				// Play a sound effect
				collisionSound.Play();
			}

			// Check for if the ball collides with the left paddle
			if (sprite.GetGlobalBounds().Intersects(game.LeftPaddle.Bounds))
			{
				// Move the ball to be a few pixels off of the paddle
				// to avoid triggering the direction flip multiple times
				position.X += 5;

				// Make the ball bounce
				FlipDirection();
			}

			// Check for if the ball collides with the right paddle
			if (sprite.GetGlobalBounds().Intersects(game.RightPaddle.Bounds))
			{
				// Move the ball to be a few pixels off of the paddle
				// to avoid triggering the direction flip multiple times
				position.X -= 5;

				// Make the ball bounce
				FlipDirection();
			}
		}

		// Check for collision on the x for if the game is over
		{
			// The ball hits the left side
			if (position.X < 0)
			{
				game.NewGame(PaddleType.RIGHT);
				deathSound.Play();
			}

			// The ball hits the right side
			if ((position.X + size) > game.Window.Size.X)
			{
				game.NewGame(PaddleType.LEFT);
				deathSound.Play();
			}
		}

	}


	private void FlipDirection()
	{
		// Reverse/flip/mirror the direction on the x
		direction.X = -direction.X;
		speedMultiplier += 0.08f;

		// Play a sound effect
		collisionSound.Play();
	}
}