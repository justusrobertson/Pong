using UnityEngine;
using System.Collections;
 
public class DontGoThroughThings : MonoBehaviour 
{
	public LayerMask layerMask; //make sure we aren't in this layer 
	public float skinWidth = 0.1f; //probably doesn't need to be changed 
 
	private float minimumExtent; 
	private float partialExtent; 
	private float sqrMinimumExtent; 
	public Vector3 previousPosition {get; set;}
	private Rigidbody myRigidbody; 
	
	public bool paused {get; set;}
 
	//initialize values 
	void Awake() 
	{ 
		paused = false;
		myRigidbody = rigidbody; 
		previousPosition = myRigidbody.position; 
		minimumExtent = Mathf.Min(Mathf.Min(collider.bounds.extents.x, collider.bounds.extents.y), collider.bounds.extents.z); 
		partialExtent = minimumExtent * (1.0f - skinWidth); 
		sqrMinimumExtent = minimumExtent * minimumExtent; 
	} 
 
	void FixedUpdate() 
	{ 
		if (!paused)
		{
			//have we moved more than our minimum extent? 
			Vector3 movementThisStep = myRigidbody.position - previousPosition; 
			float movementSqrMagnitude = movementThisStep.sqrMagnitude;
			
			if (movementSqrMagnitude > sqrMinimumExtent) 
			{ 
			  float movementMagnitude = Mathf.Sqrt(movementSqrMagnitude);
			  RaycastHit hitInfo; 
			
			  //check for obstructions we might have missed 
			  if (Physics.Raycast(previousPosition, movementThisStep, out hitInfo, movementMagnitude, layerMask.value)) 
			     myRigidbody.position = hitInfo.point - (movementThisStep/movementMagnitude)*partialExtent; 
			} 
			
			previousPosition = myRigidbody.position; 
		}
	}
}