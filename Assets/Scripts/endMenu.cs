using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class endMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnMenu() {
		gameObject.SetActive (true);
	}

	public void Continue ()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}
	public void MainMenu ()
	{
		SceneManager.LoadScene ("Menu");
	}
}
