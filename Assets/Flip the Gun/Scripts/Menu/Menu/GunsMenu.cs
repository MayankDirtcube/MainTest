/*
* Copyright (c) DeckOfDev
* Email deckofdev@gmail.com
*/

using UnityEngine;
using System.Collections;

public class GunsMenu : MonoBehaviour {

	[Tooltip("menu/game fade animation.")]
	public Animation fadeMenu, fadeGame;
	[Tooltip("Gun gameobject.")]	
	public GameObject Gun;
	[Tooltip("Menu/Game menu")]	
	public GameObject menuCanvas, gameCanvas;
	[Tooltip("Main camera gameobject.")]	
	public CameraMovement cameraMov;

	//If player pressed to start game.
	public IEnumerator StartGame()
	{
		//Fade in animation.
		fadeMenu.Play("FadeIn");
		yield return new WaitForSeconds(0.25f);
		//Enable gun.
		Gun.SetActive(true);
		//Disable menu.
		menuCanvas.SetActive(false);
		//Start fade out animation.
		fadeGame.enabled = true;
		//Enable game canvas.
		gameCanvas.SetActive(true);
		fadeGame.Play("FadeOut");
	}
}
