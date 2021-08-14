/*
* Copyright (c) DeckOfDev
* Email deckofdev@gmail.com
*/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LeaderboardMenu : MonoBehaviour {

	[Tooltip("No internet gameobject.")]
	public GameObject noInternet;

	[Tooltip("Transform gameobject.")]
	public Transform leaderboard;
	[Tooltip("Player score.")]
	public Transform mainPlayerScore;

	private int i;

	void OnEnable()
	{
		//If device has Internet connection.
		if(Coroutines.isConnected)
		{
			//Get first 10 highscores.
			for(i = 0; i < Coroutines.highscoresList.Length; i++)
			{
				if(i == 10)
					break;
				else
				{
					//Display to device top 10 players names and their scores.
					leaderboard.GetChild(i).GetChild(2).GetComponent<Text>().text = Coroutines.highscoresList[i].username;
					leaderboard.GetChild(i).GetChild(3).GetComponent<Text>().text = Coroutines.highscoresList[i].score.ToString();
				}
			}
			while(i < 10)
			{
				leaderboard.GetChild(i).gameObject.SetActive(false);
				i++;
			}
			//Display player name and score.
			mainPlayerScore.GetChild(0).GetComponent<Text>().text = Coroutines.playerPlace.ToString() + "." + MainMenu.username;
			mainPlayerScore.GetChild(1).GetComponent<Text>().text = Score.highScore.ToString();
		}
		//If device has no Internet connection.
		else
		{
			//Enable no Internet gameobject.
			noInternet.SetActive(true);
			//Disable leaderboard menu.
			gameObject.SetActive(false);
		}
	}
}
