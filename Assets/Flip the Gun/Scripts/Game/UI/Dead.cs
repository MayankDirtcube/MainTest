/*
* Copyright (c) DeckOfDev
* Email deckofdev@gmail.com
*/

using UnityEngine;
using UnityEngine.UI;

public class Dead : MonoBehaviour {

	[Tooltip("Reward/Dead menu gameobject.")]
	public GameObject rewardMenu, deadMenu;
	[Tooltip("Coroutines gameobject.")]	
	public Coroutines coroutines;
	[Tooltip("High score text gameobject.")]	
	public Text highScoreTxt;
	[Tooltip("Dead score/highscore text gameobject.")]	
	public Text deadScoreTxt, deadHighScoreTxt;
	[Tooltip("Main camera object.")]	
	public Transform cameraPosition;

    void OnTriggerEnter2D(Collider2D col)
    {
		//If gun enters bottom collider.
        if (col.tag == "GunUI")
        {
			//If score is higher than highscore.
			if((int)cameraPosition.position.y/2 > Score.highScore)
			{
				//Set new highscore.
				Score.highScore = (int)cameraPosition.position.y/2;
				PlayerPrefs.SetInt("HighScore", Score.highScore);
				highScoreTxt.text = "BEST SCORE: " + Score.highScore.ToString();
				deadHighScoreTxt.text = "BEST SCORE: " + Score.highScore.ToString();
				coroutines.AddNewHighscore(MainMenu.username, Score.highScore);
			}
			//Reset coin count(for rewards, challenges).
			Coin.coinCount=0;
			//Display score to device.
			deadScoreTxt.text = ((int)cameraPosition.position.y/2).ToString();	
			//If score is bigger than 100 then enable reward menu.
			if((int)cameraPosition.position.y/2 >= 100)
				rewardMenu.SetActive(true);
			//Otherwise display dead menu.
			else 
				deadMenu.SetActive(true);
			//Disable escape button.
			MainMenu.escape = 0;
		}
	}
}
