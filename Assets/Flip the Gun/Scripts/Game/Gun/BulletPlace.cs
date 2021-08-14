/*
* Copyright (c) DeckOfDev
* Email deckofdev@gmail.com
*/

using UnityEngine;

public class BulletPlace : MonoBehaviour {

	[Tooltip("If gun used in menu.")]
	public bool menu;
	[Tooltip("Second gun bulletPlace gameobject.")]
	public GameObject bulletPlace;

	void OnTriggerExit2D(Collider2D col)
	{
		//If gun exits camera then enable bullet place in second gun.
		if (col.tag == "MainCamera" && !menu)
        {
			bulletPlace.SetActive(true);
			gameObject.SetActive(false);
		}
	}
}
