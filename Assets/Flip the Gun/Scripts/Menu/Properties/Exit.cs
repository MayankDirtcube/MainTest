/*
* Copyright (c) DeckOfDev
* Email deckofdev@gmail.com
*/

using UnityEngine;
using System.Collections;

public class Exit : MonoBehaviour {

	[Tooltip("Exit text animation.")]
	public Animation exitText;
	[Tooltip("Menu canvas object.")]
	public Transform menuCanvas;
	[Tooltip("Pause menu object.")]
	public PauseMenu pauseMenu;
	[Tooltip("Fade in and fade out animation.")]
	public Animation fade;

	//Used to check if player press two times back in order to exit.
	private bool exiting;


	void Update () 
	{
		//If player pressed exit.
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			//If escaped was pressed in menu.
			if(MainMenu.escape == 1)
			{
				//If exit was pressed first time.
				if(!exiting)
				{
					//Start exiting coroutine.
					StartCoroutine(IsExiting());
					//Fade in exiting text.
					exitText.Play("ExitFadeIn");
				}
				//If exit was pressed second time.
				else 
				{
					//Stop exiting coroutine.
					StopCoroutine(IsExiting());
					//Exit game.
					Application.Quit();
				}
			}
			//If escaped was pressed when player was in settings, gift, achievements, challenge etc. menu.
			else if(MainMenu.escape == 2)
			{
				//Disable all menu
				for(int i = 2; i < menuCanvas.childCount; i++)
					menuCanvas.GetChild(i).gameObject.SetActive(false);
				//Enable menu.
				MainMenu.mainMenu.Play("MainMenuButtonFlyIn");
				//Play fade out animation.
				fade.Play("FadeOut");
				//Set escape to menu escape.
				MainMenu.escape = 1;
			}
			//If escaped was pressed when playing.
			else if(MainMenu.escape == 3)
			{
				//Pause game.
				pauseMenu.Pause();
				//Disable escape button.
				MainMenu.escape = 0;
			}
		}
	}
	IEnumerator IsExiting()
	{
		//Enable exiting bool.
		exiting = true;
		//Player has to exit in 3 seconds otherwise it will reset.
		yield return new WaitForSeconds(3);
		exiting = false;
		exitText.Play("ExitFadeOut");
	}
}
