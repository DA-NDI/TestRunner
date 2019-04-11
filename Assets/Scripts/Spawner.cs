using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public GameObject[] bridges;
	private Transform playerTransform;
	private float spawnZ = -6.0f; //place where bridge is spawned
	private float platformLength = 6;
	private int amount = 5;
	private List <GameObject> activePlatforms;
	private int currentIndex = 0; // current index of bridge generated

	// Use this for initialization
	void Start () {
		activePlatforms = new List <GameObject>();
		playerTransform = GameObject.FindGameObjectWithTag ("Player").transform;
		SpawnTile (0);
		SpawnTile (0);
		for (int i = 0; i < (amount - 2); i++) {
			SpawnTile ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if ((playerTransform.position.z - 15) > (spawnZ - amount * platformLength)) {
			SpawnTile ();
			DeleteTile ();
		}
	}

	private void SpawnTile (int prefabIndex = -1)
	{
		GameObject go;
		if (prefabIndex == -1) //means default, with no input index
			go = Instantiate (bridges [GetRandomBridge()]) as GameObject;
		else
			go = Instantiate (bridges [prefabIndex]) as GameObject;
		activePlatforms.Add (go);
		go.transform.SetParent (transform);
		go.transform.position = Vector3.forward * spawnZ;
		spawnZ += platformLength;
	}
	private void DeleteTile ()
	{
		Destroy (activePlatforms [0]);
		activePlatforms.RemoveAt (0);
	}
	private int GetRandomBridge() {
		if (bridges.Length <= 1)
			return (0);
		int retIndex = currentIndex;
		while (retIndex == currentIndex)
			retIndex = Random.Range (0, bridges.Length);
		currentIndex = retIndex;
		return (retIndex);
	}
}
