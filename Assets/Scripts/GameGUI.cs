using UnityEngine;
using System.Collections;

public class GameGUI : MonoBehaviour
{
	// GUI Text score containers.
	public GUIText pScore;
	public GUIText eScore;
	
	// The game state.
	private GameState state;
	
	// Use this for initialization.
	void Start ()
	{
		// Find and store the state script.
		state = (GameState)FindObjectOfType(typeof(GameState));	
	}
	
	// Called each time the GUI is refreshed.
	void OnGUI()
	{
		// Update the score display with the values in the game state.
		pScore.text = "" + state.playerScore;
		eScore.text = "" + state.enemyScore;
	}
}