/*
* Copyright (c) DeckOfDev
* Email deckofdev@gmail.com
*/

using UnityEngine;

public class Layer : MonoBehaviour {

    void OnTriggerExit2D(Collider2D col)
    {
		//If layer reaches bottom side collider.
        if (col.tag == "BSide")
        {
			//Destroy layer.
			Destroy(gameObject);
		}
	}
}
