/*
* Copyright (c) DeckOfDev
* Email deckofdev@gmail.com
*/

using UnityEngine;

public class ButtonAnimation : MonoBehaviour {

	//Start Shrink button animation.
	public void Shrink()
	{
		gameObject.GetComponent<Animation>().Play("ButtonShrink");
	}

	//Start Grow button animation.
	public void Grow()
	{
		gameObject.GetComponent<Animation>().Play("ButtonGrow");
	}
}
