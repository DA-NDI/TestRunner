using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	private static int score = 0;
	public Text scoreText;
	private int difficult = 1;
	private bool isAlive = true;
	public endMenu end;
	// Use this for initialization
	void Start () {
	 
	}
	
	// Update is called once per frame
	void Update () {
		if (!isAlive)
			return;
		scoreText.text = score.ToString ();
		
	}

	void SpeedUp () {
		GetComponent<PlayerMovement>().SetSpeed(difficult);
	}

	public void IfDead ()
	{
		isAlive = false;
		end.OnMenu ();
		return;

	}

	public void AddScore (int value)
	{
		score += value;
	}
}
