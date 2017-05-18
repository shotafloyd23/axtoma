using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
	public Transform player;
	public GameObject[] Platforms; 
	public int NumberOfStartingPlatforms;
	public Vector3 PlatformSpawnpoint = Vector3.zero;
	public float destroytime;

	void Start() 
	{
		int counter = 0; 
		while(counter < NumberOfStartingPlatforms){
		CreateNextPlatform ();
			counter+= 1;

	}
	}
	int PP = 0;
	void Update ()
	{
		int pp = (int)player.position.z / 50;
		if (pp > PP) {
			CreateNextPlatform ();
		}
		PP = pp;
	
	}
	void CreateNextPlatform()
	{ 
		GameObject clone;
		PlatformSpawnpoint.z = PlatformSpawnpoint.z + 50f;
		int PlatformChoser = Random.Range (0, Platforms.Length);
		clone = Instantiate (Platforms[PlatformChoser]);
		clone.transform.transform.position = PlatformSpawnpoint;
		Debug.Log ("creating plartform");
		Destroy (clone, destroytime);

	}

}
