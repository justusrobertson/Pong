using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	// The ball's game object. Set this in the editor.
	public GameObject ball;
	
	// A record of the paddle's last position.
	private Vector3 lastPosition;
	
	// A record of the paddle's velocity.
	public Vector3 velocity { get; set; }
	
	// Use this for initialization.
	void Start ()
	{
		// Initialize the paddle's position.
		lastPosition = transform.position;
		
		// Initialize the paddle's velocity.
		velocity = new Vector3 (0, 0, 0);
	}
	
	// Update is called once per frame.
	void Update () 
	{		
		// If the ball in play...
		if (ball.transform.position.x < transform.position.x)
		{
			// ...tween to meet it.
			iTween.MoveUpdate(this.gameObject,new Vector3(this.transform.position.x, ball.transform.position.y, this.transform.position.z), .6f);	
		}
		// Otherwise, it is out of play...
		else
		{
			// ...so tween to the center of the screen.
			iTween.MoveUpdate(this.gameObject,new Vector3(this.transform.position.x, 0, this.transform.position.z), 3.0f);	
		}
		
		// If the paddle is above the top of the screen...
		if (transform.position.y > 15)
		{
			// ...move the paddle to the top of the screen.
			transform.position = new Vector3(transform.position.x, 15, transform.position.z);
		}
		// Otherwise, if the paddle is below the bottom of the screen...
		else if (transform.position.y < -13)
		{
			// ...move the paddle to the bottom of the screen.
			transform.position = new Vector3(transform.position.x, -13, transform.position.z);
		}
		
		// Update the paddle's velocity.
		this.velocity = (transform.position - lastPosition) / Time.deltaTime;
		
		// Update the paddle's position.
		this.lastPosition = transform.position;
	}
}