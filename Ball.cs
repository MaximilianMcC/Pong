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






	private void Movement()
	{
		
	}
}