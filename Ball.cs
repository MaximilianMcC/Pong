using SFML.Graphics;
using SFML.System;
using SFML.Window;

class Ball
{
	private Game game;
	private float size = 15;
	private RectangleShape sprite;
	private Vector2f movementDirection;
	private Vector2f position;
	private float speed = 15f;
	private float speedMultiplier = 1f;

	public Ball(Game game)
	{
		this.game = game;

		// Create the sprite
		this.sprite = new RectangleShape(new Vector2f(size, size));
		sprite.FillColor = new Color(0xf1f1f1ff);
		sprite.Origin = new Vector2f((size / 2), (size / 2));
		
		// Put the sprite in the middle of the game
		position = new Vector2f((game.Window.Size.X / 2), (game.Window.Size.Y / 2));
		sprite.Position = position;
	}

	public void Update()
	{
		Movement();
		if (Collision(position)) FlipDirection();
	}

	public void Render()
	{
		game.Window.Draw(sprite);
	}









	// Begin a new game
	public void Begin()
	{
		// Set the speed multiplier to 1
		speedMultiplier = 1;

		// Create a random "launch" vector that will make the ball
		// travel to the top-left, top-right, bottom-left, or bottom-right
		Random random = new Random();

		// Decide if its going right/up (true) or down/left (false)
		bool x = random.NextSingle() >= 0.5;
		bool y = random.NextSingle() >= 0.5;

		// Create the vector
		float top = x ? speed : -speed;
		float left = y ? speed : -speed;
		movementDirection = new Vector2f(top, left);
	}

	// Flip the direction of the ball
	public void FlipDirection()
	{
		// Reverse the movement vector
		movementDirection = -movementDirection;
	}


	// Move the ball according to the movement vector
	private void Movement()
	{
		Vector2f newPosition = position;
		float movementSpeed = (speed * speedMultiplier);
		Vector2f movement = (movementDirection * movementSpeed) * game.DeltaTime;

		// Move the ball according to the movement vector
		newPosition += movement;

		// Update the position
		position = newPosition;
		sprite.Position = position;
	}

	// Check for collision
	private bool Collision(Vector2f newPosition)
	{
		// Check for if the ball is about to hit the window bounds
		if ((newPosition.Y < 0) || ((newPosition.Y + size) > game.Window.Size.Y)) return true;
		else if ((newPosition.X < 0) || ((newPosition.X + size) > game.Window.Size.X)) return true;

		// Check for if the ball is about to hit a paddle
		if (sprite.GetGlobalBounds().Intersects(game.RightPaddle.Bounds)) return true;
		if (sprite.GetGlobalBounds().Intersects(game.LeftPaddle.Bounds)) return true;

		return false;
	}
}