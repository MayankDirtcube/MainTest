/*
* Copyright (c) DeckOfDev
* Email deckofdev@gmail.com
*/

using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

	//Used to check how many coins are collected in one game.
	public static int coinCount; 

    void OnTriggerEnter2D(Collider2D col)
    {
		//If gun touched coin.
        if (col.tag == "GunUI")
        {
			//Add coin to wallet.
			Wallet.AddCoins(1);
			//Add coin to Achievements stats.
			PlayerPrefs.SetInt("CollectCoins", (PlayerPrefs.GetInt("CollectCoins")+1));
			coinCount++;

			//If collected 10 coins in one session then first achievement has been reached.
			if(coinCount == 10 && PlayerPrefs.GetInt("OneSessionCoins") < 10)
				PlayerPrefs.SetInt("OneSessionCoins", 10);
			//If collected 10 coins in one session then second achievement has been reached.
			else if(coinCount == 25 && PlayerPrefs.GetInt("OneSessionCoins") < 25)
				PlayerPrefs.SetInt("OneSessionCoins", 25);
			//If collected 10 coins in one session then third achievement has been reached.
			else if(coinCount == 50 && PlayerPrefs.GetInt("OneSessionCoins") < 50)
				PlayerPrefs.SetInt("OneSessionCoins", 50);

			//If playing in challenge mode.
			if(ChallengeMenu.challengeActive[3])
			{
				//Update challenge stats.
				Coroutines.AchieveChallenge(coinCount, 25, 50, 100);
			}
			//Start destroy coroutine.
			StartCoroutine(CoinDestroy());
		}
	}

	IEnumerator CoinDestroy()
	{
		//Disable coin image.
		gameObject.GetComponent<SpriteRenderer>().enabled = false;
		//Play coin destroy animation.
		transform.parent.GetComponent<Animation>().Play("CoinDestroy");
		//Wait when animation ends.
		yield return new WaitForSeconds(0.7f);
		//Destroy coin gameobject.
		Destroy(transform.parent.gameObject);
	}
}
