/*
* Copyright (c) DeckOfDev
* Email deckofdev@gmail.com
*/

using UnityEngine;
using System.Collections;

public class BulletFly : MonoBehaviour {

	//Used to get position where bullet should be spawned.
	private Transform bulletPlace;

	void Start()
	{
		//Find bullet spawn position gameobject.
		bulletPlace = GameObject.FindGameObjectWithTag("BulletPlace").transform;
		//Setup bullet spawn position and rotation.
		transform.parent.position = bulletPlace.position;
		transform.parent.rotation = bulletPlace.rotation;

	}

	public void BulletDestroy()
	{
		//When animation ends destroy bullet gameobject.
		Destroy(transform.parent.gameObject);
	}
}
