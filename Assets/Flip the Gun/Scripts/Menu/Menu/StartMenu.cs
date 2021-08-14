/*
* Copyright (c) DeckOfDev
* Email deckofdev@gmail.com
*/

using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour {

	[Tooltip("Main Menu button fly in and fly out animation.")]
	public Animation mainMenu;
	[Tooltip("User name gameobject.")]
	public Text usernameTxt;

	public void Start()
	{
		//Disable main menu.
		mainMenu.Play("MainMenuButtonFlyOut");
	}

	//If player choose name.
	public void ChooseName()
	{
		//Save username to player prefs.
		PlayerPrefs.SetString("Username", usernameTxt.text);
		MainMenu.username = usernameTxt.text;
		//Enable main menu.
		mainMenu.Play("MainMenuButtonFlyIn");
	}
}
