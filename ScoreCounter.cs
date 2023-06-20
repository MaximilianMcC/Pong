using SFML.Graphics;
using SFML.System;
using SFML.Window;

class ScoreCounter
{
	private static int leftScore = 0;
	private static int rightScore = 0;
	private static Font font = new Font("./EndlessBossBattleRegular.ttf");

	// Update the scores
	public static void UpdateScore(PaddleType paddle)
	{
		// Check for what player score is being increased
		if (paddle == PaddleType.LEFT) leftScore++;
		if (paddle == PaddleType.RIGHT) rightScore++;
	}

	// Reset the scores
	public static void ResetScores()
	{
		leftScore = 0;
		rightScore = 0;
	}

	// Draw scores
	//TODO: Don't get the window as an argument
	// TODO: Don't create a new text object every single time
	public static void DrawScores(RenderWindow window)
	{
		// Create the left score text
		Text leftText = new Text(leftScore.ToString(), font, 150);
		leftText.FillColor = new Color(0x17181aff);
		leftText.Origin = new Vector2f((leftText.GetGlobalBounds().Width / 2), (leftText.GetGlobalBounds().Height / 2));
		leftText.Position = new Vector2f(((window.Size.X / 2) / 2), ((window.Size.Y - leftText.CharacterSize) / 2));
		window.Draw(leftText);

		// Create the right score text
		Text rightText = new Text(rightScore.ToString(), font, 150);
		rightText.FillColor = new Color(0x17181aff);
		rightText.Origin = new Vector2f((rightText.GetGlobalBounds().Width / 2), (rightText.GetGlobalBounds().Height / 2));
		rightText.Position = new Vector2f(window.Size.X - ((window.Size.X / 2) / 2), ((window.Size.Y - rightText.CharacterSize) / 2));
		window.Draw(rightText);	
	}
}