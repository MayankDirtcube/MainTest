/*
* Copyright (c) DeckOfDev
* Email deckofdev@gmail.com
*/

using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class GiftMenu : MonoBehaviour {

	[Tooltip("No Internet gameobject.")]
	public GameObject noInternet;
	[Tooltip("Gift button gameobject.")]
	public GameObject giftButton;
	[Tooltip("Fade in/out animation.")]
	public Animation fade;

	//If player pressed get now gift with ad.
	public void GetNow()
	{
		//Start fade in animation.
		fade.Play("FadeIn");
		//Show ads.
		StartCoroutine(ShowAd());
	}

	//Fade out animation.
	public void FadeOut()
	{
		fade.Play("FadeOut");
	}	

	IEnumerator ShowAd()
	{
		//Wait when fade animation ends.
		yield return new WaitForSeconds(0.5f);
		//If device has Internet connection.
#if UNITY_ADS
		if(Coroutines.isConnected)
		{
			//Load ad.
			if (Advertisement.IsReady("rewardedVideo"))
			{
				StartCoroutine(StartVideoAd());				
			}	
		}	
		//If device has no Internet connection.
		else
		{
			//Enable No Internet gameobject and disable gift menu.
			noInternet.SetActive(true);
			gameObject.SetActive(false);
		}
#endif
	}

	IEnumerator StartVideoAd()
	{
		yield return new WaitForSeconds(0.5f);		
		//Show ad.
#if UNITY_ADS
		Advertisement.Show("rewardedVideo");
		//Add coins to player wallet.
		Wallet.AddCoins(100);
		Wallet.CoinBlast();
		//Reset gift timer.
		Timer.giftTimer = 1800;
		//Update achievements stats.
		PlayerPrefs.SetInt("CollectGifts", (PlayerPrefs.GetInt("CollectGifts")+1));
		FadeOut();			
#endif
	}

	//If pressed claim button with no ads.
	public void Claim()
	{
		//Add coins to player wallet.
		Wallet.AddCoins(100);
		Wallet.CoinBlast();	
		//Reset timer.
		Timer.giftTimer = 1800;
		//Reset gift image and animation.
		giftButton.GetComponent<Image>().color = Color.white;
		transform.GetChild(4).GetChild(0).GetComponent<Image>().color = Color.white;
		//Update achievements stats.
		PlayerPrefs.SetInt("CollectGifts", (PlayerPrefs.GetInt("CollectGifts")+1));
	}
}
