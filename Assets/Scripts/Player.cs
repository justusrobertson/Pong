using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	// Holds the speed at which the player's paddle moves.
	private float speed;
	
	// Holds the player's previous position.
	private Vector3 lastPosition;
	
	// Holds the velocity at which the player is moving.
	public Vector3 velocity { get; set; }
	
	// Use this for initialization.
	void Start ()
	{
		// Initialize the player's speed.
		speed = 30;
		
		// Initialize the player's position.
		lastPosition = transform.position;
		
		// Initialize the player's velocity.
		velocity = new Vector3 (0, 0, 0);
	}
	
	// Update is called once per frame.
	void Update () 
	{
		// If the player presses the up button...
		if (Input.GetButton("Up"))
		{
			// ...move the player upwards.
			transform.Translate(speed * Vector3.up * Time.deltaTime);
		}
		// Otherwise, if the player presses the down button...
		else if (Input.GetButton("Down"))
		{
			// ...move the player downwards.
			transform.Translate(speed * Vector3.down * Time.deltaTime);
		}
		
		// If the player's paddle is above the screen...
		if (transform.position.y > 15)
		{
			// ...reset the player's paddle to the top of the screen.
			transform.position = new Vector3(transform.position.x, 15, transform.position.z);
		}
		// Otherwise, if the player's paddle is below the screen...
		else if (transform.position.y < -13)
		{
			// ...reset the player's paddle to the bottom of the screen.
			transform.position = new Vector3(transform.position.x, -13, transform.position.z);
		}
		
		// Store the player's current velocity.
		this.velocity = (transform.position - lastPosition) / Time.deltaTime;
		
		// Store the player's previous position.
		this.lastPosition = transform.position;
	}
}