using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * this script is for randowm spawning of pickups.
 * in current version i'm using previously generated
 * platforms, as it's easier approach, but not scalable as good and easy
 */

public class SpawnPickups : MonoBehaviour {

	public GameObject[] pickups;
	private Transform playerTransform;
	private float spawnZ = -6.0f; //place where pickup is spawned
	private float platformLength = 6;
	private int amount = 5;
	private int pickAmount = 2;
	private List <GameObject> activePickups;
	private float[] xAxisPosition = {0, 1.0f, -1.0f};

	// Use this for initialization
	void Start () {
		activePickups = new List <GameObject>();
		playerTransform = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	// Update is called once per frame
	void Update () {
		if ((playerTransform.position.z - 15) > (spawnZ - amount * platformLength)) {
			for (int i = 0; i < Random.Range(0, 3); i++)
				SpawnTile ();
		}
		DeleteTile ();
	}

	private void SpawnTile ()
	{
		GameObject go;
		Vector3 pickupPos = new Vector3 (0, 1.0f, playerTransform.position.z + 5);

		while (Physics.CheckSphere(pickupPos, 0.6f))
		{
			pickupPos.z = Random.Range (playerTransform.position.z, playerTransform.position.z + 15);
			pickupPos.x = xAxisPosition[Random.Range (0, 3)];
		}
		go = Instantiate (pickups [Random.Range (0, pickAmount)], pickupPos, Quaternion.identity) as GameObject;
		activePickups.Add (go);
		spawnZ += platformLength;
	}
	private void DeleteTile ()
	{
		for (int i = 0; i < activePickups.Count; i++) {
			Debug.Log ("obstacl z = " + activePickups[i].transform.position.z);
			if (activePickups [i].transform.position.z < playerTransform.position.z) {
				if (activePickups [i])
				activePickups.Remove(activePickups [i]);
				Destroy (activePickups [i]);
				activePickups.RemoveAt (i);
			}
		}
	}
}
