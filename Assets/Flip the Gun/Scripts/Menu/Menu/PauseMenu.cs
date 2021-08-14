/*
* Copyright (c) DeckOfDev
* Email deckofdev@gmail.com
*/

using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

	[Tooltip("Main Camera gameobject.")]
	public CameraMovement cameraMovement;
 	[Tooltip("Fading gameobject.")]
    public Animation fade, fadeRestart;
	
	//Used to disable gun when game is paused.
	private GameObject gun;

	//If game paused.
	public void Pause()
	{
		//Find gun
        gun = GameObject.FindWithTag("Gun");
		//Pause gun.
		Gun.Pause();		
		//Disable gun physics.
		gun.GetComponent<Gun>().enabled = false;
		//Disable camera movement.
		cameraMovement.enabled = false;
		//Start fade in animation.
		fade.Play("FadeIn");
		//Enable pause menu.
		gameObject.SetActive(true);
	}

	//If game resumed.
	public void Resume () 
	{
		//Fade out
		fade.Play("FadeOut");
		//Enable camera movement.
		cameraMovement.enabled = true;
		//Enable gun physics.
		cameraMovement.GetComponent<Coroutines>().EnableCoroutine("EnableGunPhysics");
		//Disable pause menu.
		gameObject.SetActive(false);
	}

	//If go to menu button pressed.
	public void Menu()
	{
		//Fade in pause menu.
		fadeRestart.Play("FadeIn");
		//Restart game.
		cameraMovement.GetComponent<Coroutines>().EnableCoroutine("RestartScene");
	}
}
