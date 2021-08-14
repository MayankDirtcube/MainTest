/*
* Copyright (c) DeckOfDev
* Email deckofdev@gmail.com
*/

using UnityEngine;

public class ScreenGlow : MonoBehaviour {

	//Used to get glow animation in gameobject.
	private static Animation glow;

	void Start () 
	{
		//Attach gamobject animation.
		glow = gameObject.GetComponent<Animation>();
	}

	//Enable glow animation.	
	public static void StartGlow()
	{
		//Play glow animation.
		glow.Play("ScreenDangerGlow");
	}
}
