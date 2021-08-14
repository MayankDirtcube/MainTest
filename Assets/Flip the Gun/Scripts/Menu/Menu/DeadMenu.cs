/*
* Copyright (c) DeckOfDev
* Email deckofdev@gmail.com
*/

using UnityEngine;
using System.Collections;

public class DeadMenu : MonoBehaviour {

	[Tooltip("Fade in animation when restarting scene.")]
	public Animation fade;

	[Tooltip("Coroutines gameobject in MainCamera.")]
	public Coroutines coroutines;

	void Start()
	{
		//Fade in animation when player is dead.
		gameObject.GetComponent<Animation>().Play("FadeIn");
	}

	public void Continue()
	{
		//Fade in animation to restart game.
		fade.Play("FadeIn");
		//Start restart coroutine.
		coroutines.StartCoroutine("RestartScene");
	}
}
