/*
* Copyright (c) DeckOfDev
* Email deckofdev@gmail.com
*/

using UnityEngine;

public class Speed : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D col)
    {
		//If gun touched speed.
        if (col.tag == "GunUI")
        {
            //Play speed sound.
            gameObject.GetComponent<AudioSource>().Play();
            //Play speed animation.
		    transform.parent.GetComponent<Animation>().Play("SpeedDestroy");
		}
	}
}
