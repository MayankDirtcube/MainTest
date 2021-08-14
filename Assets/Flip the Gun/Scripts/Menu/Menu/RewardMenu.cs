/*
* Copyright (c) DeckOfDev
* Email deckofdev@gmail.com
*/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Advertisements;

public class RewardMenu : MonoBehaviour {

	[Tooltip("Dead menu gameobject.")]
	public GameObject deadMenu;

	void Start () 
	{
		//Set reward button text to how many player collected coins in game.
		transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>().text = (Wallet.coins - Wallet.startCoins).ToString();
		//Set reward button text to how many player collected coins in game and multiply it.
		transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<Text>().text = ((Wallet.coins - Wallet.startCoins)*8).ToString();
	}
	
	//If player pressed to claim coins with no ads.
	public void Claim()
	{
		//Start exiting.
		StartCoroutine(ExitReward());
	}

	//If player pressed to claim x8 coins with ads.
	public void ClaimX8()
	{
#if UNITY_ADS
		//If device is connected to the Internet.
		if(Coroutines.isConnected)
		{
			//Load reward video.
			if (Advertisement.IsReady("rewardedVideo"))
			{
				StartCoroutine(StartVideoAd());
			}
		}
#endif	
	}

	IEnumerator StartVideoAd()
	{
		yield return new WaitForSeconds(0.5f);
		//Show reward video.
#if UNITY_ADS
		Advertisement.Show("rewardedVideo");
		//Add x8 more coins to players wallet.
		var temp = Wallet.coins;
		Wallet.removeCoins(temp-Wallet.startCoins);
		Wallet.AddCoins((temp-Wallet.startCoins)*8);
		//Start exiting.
		StartCoroutine(ExitReward());		
#endif	
	}

	IEnumerator ExitReward()
	{
		//Enable coin blast animation.
		Wallet.CoinBlast();
		//Start fading out animation.
		gameObject.GetComponent<Animation>().Play("FadeOut");
		//Disable reward gameobject.
		transform.GetChild(0).gameObject.SetActive(false);
		//Enable dead menu gameobject.
		deadMenu.SetActive(true);
		deadMenu.GetComponent<Animation>().enabled = false;
		deadMenu.GetComponent<Image>().color = Color.black;
		yield return new WaitForSeconds(0.5f);
		gameObject.SetActive(false);		
	}
	
}
