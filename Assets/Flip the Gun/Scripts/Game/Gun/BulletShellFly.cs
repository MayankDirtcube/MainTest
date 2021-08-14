/*
* Copyright (c) DeckOfDev
* Email deckofdev@gmail.com
*/

using UnityEngine;
using System.Collections;

public class BulletShellFly : MonoBehaviour {

	//Used to get position where bullet shell should be spawned.	
	private Transform bulletShellPlace;

	void Start()
	{
		//Find bullet shell spawn position gameobject.
		bulletShellPlace = GameObject.FindGameObjectWithTag("BulletShellPlace").transform;
		//Setup bullet shell spawn position and rotation.		
		transform.position = bulletShellPlace.position;
		transform.rotation = bulletShellPlace.rotation;
		//Start smoke and flare particles.
		gameObject.GetComponent<ParticleSystem>().Play();

	}
}
