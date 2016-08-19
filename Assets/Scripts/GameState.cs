using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour 
{
	// Keep track of the player and enemy scores.
	public int playerScore { get; set; }
	public int enemyScore { get; set; }
	
	// Use this for initialization
	void Start () 
	{
		// Initialize the scores to zero.
		playerScore = 0;
		enemyScore = 0;
	}
}
