using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{	
	// The minimum and maximum x velocity the ball is allowed to travel.
	private Range<float> xVelocityRange;
	
	// The minimum and maximum horizontal force that can be applied to the ball.
	private Range<float> xForceRange;
	
	// The ball's spin factor.
	// i.e. the amount by which to multiply the y velocity
	// when a player puts spin on the ball.
	private int spinFactor;
	
	// A script to hold information about the state of the game.
	public GameState state;
	
	// A script to store and play sounds.
	public Sounds sounds;
	
	// A script to help detect collisions.
	public DontGoThroughThings dontGo;
	
	// Use this for initialization.
	void Start () 
	{		
		// Initialize the x velocity range.
		xVelocityRange = new Range<float> (15, 50);
		
		// Initialize the horizontal force range.
		xForceRange = new Range<float> (750, 1500);
		
		// Initialize the spin factor.
		spinFactor = 20;
		
		// Add the initial force to the ball.
		rigidbody.AddForce(-xForceRange.Minimum, Random.Range(-100, 100), 0);
	}
	
	// Update is called once per frame.
	void Update () 
	{		
		// Check to see if the ball has passed out of the field of play.
		CheckBounds ();
		
		// Check to see if the ball has passed out of the allowed speed range.
		CheckSpeed ();
	}
	
	/// <summary>
	/// Checks whether the ball has passed out of the field of play.
	/// </summary>
	private void CheckBounds ()
	{
		// The player has scored.
		if (transform.position.x > 23)
		{
			// Pause the collision manager.
			dontGo.paused = true;
			
			// Update the player's score.
			state.playerScore++;
			
			// Reset the ball.
			ResetBall();
		}
		
		// The AI has scored.
		if (transform.position.x < -23)
		{
			// Pause the collision manager.
			dontGo.paused = true;
			
			// Update the enemy's score.
			state.enemyScore++;
			
			// Reset the ball.
			ResetBall();
		}
	}
	
	/// <summary>
	/// Ensures the ball stays within the speed range.
	/// </summary>
	private void CheckSpeed ()
	{
		// Store the direction the ball is traveling.
		float sign = Mathf.Sign (rigidbody.velocity.x);
		
		// If the ball is travelling slower than the minimum velocity...
		if (Mathf.Abs (rigidbody.velocity.x) < xVelocityRange.Minimum)
		{
			// ...set the ball's velocity to the minimum velocity.
			rigidbody.velocity = new Vector3 (xVelocityRange.Minimum * sign, rigidbody.velocity.y, rigidbody.velocity.z);	
		}
		// Otherwise, if the ball is travelling faster than the maximum velocity...
		else if (Mathf.Abs (rigidbody.velocity.x) > xVelocityRange.Maximum)
		{
			// ...set the ball's velocity to the maximum velocity.
			rigidbody.velocity = new Vector3 (xVelocityRange.Maximum * sign, rigidbody.velocity.y, rigidbody.velocity.z);	
		}
	}
	
	/// <summary>
	/// Resets the ball after a point is scored.
	/// </summary>
	private void ResetBall()
	{
		// Record the current x velocity of the ball.
		float xVel = rigidbody.velocity.x;
		
		// Reset the ball's location and velocity to zero.
		transform.position = new Vector3(0,0,0);
		rigidbody.velocity = new Vector3(0,0,0);
		
		// Reset the collision helper's position to zero.
		dontGo.previousPosition = new Vector3(0,0,0);
		
		// Unpause the collision helper.
		dontGo.paused = false;
		
		// Play the scoring sound.
		sounds.PlaySound (2);
		
		// Add a new x velocity in the direction of the player who was just scored on.
		if (xVel > 0)
		{
			rigidbody.AddForce(Random.Range(xForceRange.Minimum, xForceRange.Maximum),Random.Range(-1000,1000),0);
		}
		else
		{
			rigidbody.AddForce(-Random.Range(xForceRange.Minimum, xForceRange.Maximum),Random.Range(-1000,1000),0);
		}
	}
	
	// Is fired when a collision occurs.
	void OnCollisionEnter(Collision collision)
	{
		// Check to see if the ball has collided with the player.
		Player player = collision.gameObject.GetComponent<Player>();
		
		// Check to see if the player exists.
		if (player == null)
		{
			// Check to see of the ball has collided with the enemy.
			Enemy enemy = collision.gameObject.GetComponent<Enemy>();
			
			// Check to see if the enemy exists.
			if (enemy != null)
			{
				// If so, add force to the ball in the direction that the enemy's paddle is moving.
				rigidbody.AddForce (0, enemy.velocity.y * spinFactor, 0);
				
				// Play the paddle sound.
				sounds.PlaySound (0);
			}
			else
			{
				// The ball did not collide with a paddle,
				// so it must have collided with a wall.
				// Play the wall sound.
				sounds.PlaySound (1);	
			}
		}
		else
		{
			// Add force to the ball in the direction that the player's paddle is moving.
			rigidbody.AddForce (0, player.velocity.y * spinFactor, 0);
			
			// Play the paddle sound.
			sounds.PlaySound (0);
		}
	}
}