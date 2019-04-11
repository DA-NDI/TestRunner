using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChanAnimation : MonoBehaviour {

	public Animator anim;
	private Vector3 moveVector;
	private bool isJumping = false;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("space"))
		{
			anim.Play ("JUMP00", -1, 0f); // -1 is basic layer of animation, 0f - play from start of animation, will work with just 1 arg
			moveVector.y = 1.0f;
			isJumping = true;
		}
		if (Input.GetKey (KeyCode.LeftArrow)) //on mac we need check keycode
			anim.Play ("RUN00_L", -1, 0.99f);
		if (Input.GetKey (KeyCode.RightArrow))
			anim.Play ("RUN00_R", -1, 0.99f);
	}

	public bool IsJumping()
	{
		return isJumping;
	}

	public void SetJump(bool setter)
	{
		isJumping = setter;
	}
}
