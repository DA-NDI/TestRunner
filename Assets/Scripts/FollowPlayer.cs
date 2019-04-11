using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

	private Transform chanTr;
	private Vector3 cameraStartTransform;
	private Vector3 moveVector;
	// Use this for initialization
	void Start () {
		chanTr = GameObject.FindWithTag("Player").transform;
		cameraStartTransform = transform.position - chanTr.position;
	}
	
	// Update is called once per frame
	void Update () {
		moveVector = chanTr.position + cameraStartTransform;
		moveVector.x = 0;
		transform.position = moveVector;
	}
}
