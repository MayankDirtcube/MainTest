/*
* Copyright (c) DeckOfDev
* Email deckofdev@gmail.com
*/

using UnityEngine;

public class FingerInput : MonoBehaviour {

	//Used to check if player is touching screen.
	public static bool isTouching = false;
	
	//If finger is down.
	public void FingerDown()
	{
		isTouching = true;
	}

	//If finger is up.
	public void FingerUp()
	{
		isTouching = false;
	}
}
