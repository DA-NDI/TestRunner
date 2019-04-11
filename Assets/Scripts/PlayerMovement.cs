using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public CharacterController controller;
	public float speed = 1.0f; //public for easier speed change
	private Vector3 moveVector;
	private float verticalSpeed = 0;
	private float gravity = 10;
	private float timeFromStart = 0.0f;
	private float startSpeed = 1.0f; 
	private bool isAlive = true;
	private bool boost = false;
	private bool jump = false;
	private float jumpTime = 0.0f;
	private Vector3 swipeVector; // for swipe on x axis

	void Start () {
		controller = GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!isAlive)
			return;
		CheckPlayerHeight ();
		CheckAndPerformJump ();
		timeFromStart += Time.deltaTime;
		if (timeFromStart > 2.0f) {//player will run 1.5 faster for 2 sec at the beginning and if boosted
			startSpeed = 2.0f;
			boost = false;
		}
		moveVector = Vector3.zero;
		if (controller.isGrounded) //check if player is on ground
			verticalSpeed = 0;
		else
			verticalSpeed -= gravity * Time.deltaTime;
		/* because of poor float precision we need to check at every move
		 * right offset and transform.position.x. instead of 0.0 or 1.0 it
		 * could be 0.00011432, 0.98213, etc and very fast we will loose player, so we need such a correction
		 */
		if (Input.GetKeyDown (KeyCode.LeftArrow) && transform.position.x >= 0.5f) {
			swipeVector.x = -transform.position.x;
			transform.position += swipeVector;
		}
		else if (Input.GetKeyDown (KeyCode.LeftArrow) && transform.position.x > -0.5f && transform.position.x < 0.5f) {
			swipeVector.x = -1.0f - transform.position.x;
			transform.position += swipeVector;
		}
		else if (Input.GetKeyDown (KeyCode.RightArrow) && transform.position.x <= -0.5f) {
			swipeVector.x = -transform.position.x;
			transform.position += swipeVector;
		}
		else if (Input.GetKeyDown (KeyCode.RightArrow) && transform.position.x > -0.5f && transform.position.x < 0.5f) {
			swipeVector.x = 1.0f - transform.position.x;
			transform.position += swipeVector;
		}
		/* For smooth movement, not swiping, using keyboard (A, D, left, rightarrow), replace code above
		**		if (Input.GetKey (KeyCode.RightArrow))
		**			moveVector.x += ((transform.position.x + 0.6f) < 0.7f) ? 0.6f : 0;
		**		moveVector.x = Input.GetAxisRaw("Horizontal") * speed;
		*/
		moveVector.y = verticalSpeed;
		moveVector.z = (boost == false) ? (speed * startSpeed) : (speed * startSpeed * 1.5f);
		controller.Move(moveVector * Time.deltaTime);
	}

	public void SetSpeed(float i) {
		speed = i;
	}

	//function, that is called on every collision with player
	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		/* we use character controller now, as it is eaiser for computing
		 * and rigidbody physics could be excessive. 
		 * that's why on jumping, collider will stay on ground
		 * and we can't avoid obstacles. should be changed later if needed.
		 */
		if (hit.gameObject.tag == "obstacle")
			Death ();
		if (hit.gameObject.tag == "coin") {
			Destroy (hit.gameObject);
			GetComponent<Score> ().AddScore (1);
		}
		if (hit.gameObject.tag == "speed") { //it will boost player for 2 seconds. we could use separate variable for that.
			Destroy (hit.gameObject);
			timeFromStart = 0;
			boost = true;
		}
			
	}

	private void CheckPlayerHeight()
	{
		if (transform.position.y < -5)
			Death ();
	}

	private void Death ()
	{
		isAlive = false;
		GetComponent<Score> ().IfDead ();
	}

	public bool GetAlive()
	{
		return isAlive;
	}

	private void CheckAndPerformJump()
	{
		if (jump || GetComponent<ChanAnimation> ().IsJumping ()) {
			jump = true;
			jumpTime += Time.deltaTime;
			if (jumpTime > 0.3f)
				verticalSpeed = 2.0f;
			if (jumpTime > 1.0f) {
				jumpTime = 0.0f;
				verticalSpeed = 0.0f;
				jump = false;
				GetComponent<ChanAnimation> ().SetJump(false);
				return;
			}
		}
	}
}
