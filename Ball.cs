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
}