/*
* Copyright (c) DeckOfDev
* Email deckofdev@gmail.com
*/

using UnityEngine;

public class Blocker : MonoBehaviour {


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "GunUI")
		{
			transform.parent.GetComponent<Animation>().Play();	
			transform.parent.GetChild(1).GetComponent<ParticleSystem>().Play();
		}	
	}
}
