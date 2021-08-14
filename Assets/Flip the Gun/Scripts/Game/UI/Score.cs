/*
* Copyright (c) DeckOfDev
* Email deckofdev@gmail.com
*/

using UnityEngine;
using UnityEngine.UI;


public class Score : MonoBehaviour {

	[Tooltip("Score/Highscore text gameobject.")]
	public Text scoreTxt, highScoreTxt;
	[Tooltip("Dead score/highscore text gameobject.")]
	public Text deadScoreTxt, deadHighScoreTxt;

	//Highscore 
	public static int highScore;

	//Background color.
	private Color32 newColor;	
	//For achievements and challenges.
	private int scoreCount, lastCount;
	//Changing background time.
	private float timer;
	//If background is changing.
	private bool change;
	//If highscore reached.
	private bool highscoreReached;

	void Start()
	{
		//Set Background changing time.
		timer = 2;
		//Reset highscore reached.
		highscoreReached = true;
		//Get highscore from player prefs.
		highScore = PlayerPrefs.GetInt("HighScore");
		//Display highscore to device.
		highScoreTxt.text = "BEST SCORE: " +  highScore.ToString();
		deadHighScoreTxt.text = "BEST SCORE: " +  highScore.ToString();
	}
	
	void Update () 
	{
		//Display score to device.
		scoreTxt.text = ((int)transform.position.y/2).ToString();
		
		//If higscore is reached then spawn highscore line.
		if(highscoreReached && (int)transform.position.y/2 == highScore - 10)
		{
			LayersSpawn.SpawnHighscoreLine();
			highscoreReached = false;
		}

		//If playing in challenge mode.
		if(ChallengeMenu.challengeActive[0])
		{
			Coroutines.AchieveChallenge((int)transform.position.y/2, 25, 50, 100);
		}
		//If playing in challenge mode.
		else if (ChallengeMenu.challengeActive[4])
		{
			if(GunCollider.boosterUsed)
			{
				lastCount = (int)transform.position.y/2;
				GunCollider.boosterUsed = false;
			}

			scoreCount = (int)transform.position.y/2-lastCount;			
			Coroutines.AchieveChallenge(scoreCount, 25, 50, 100);
		}
		//Change background color.
		if(!change && (int)transform.position.y/2 > 1 && (int)transform.position.y/2%50==0)
		{
			//Create random color.
			newColor.r = (byte) (Random.Range(0,20));
			newColor.g = (byte) (Random.Range(0,20));
			newColor.b = (byte) (Random.Range(0,20));
			timer = 0;
			change = true;
		}
		if(timer >= 2)
		{
			change = false;
		}

		if(timer < 2)
		{
			//Lerp background color from old to new.
			transform.GetComponent<Camera>().backgroundColor = Color.Lerp(transform.GetComponent<Camera>().backgroundColor, newColor, timer);	
			timer+= Time.deltaTime;		
		}
	}
}
