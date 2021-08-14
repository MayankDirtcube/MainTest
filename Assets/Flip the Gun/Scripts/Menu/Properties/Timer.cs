/*
* Copyright (c) DeckOfDev
* Email deckofdev@gmail.com
*/

using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	[Tooltip("Gift timer gameobject.")]
	public Text giftTimerTxt;
	[Tooltip("Get now/Claim button gameobject")]
	public GameObject getNow, claim;
	[Tooltip("Gift button gameobject")]
	public GameObject giftButton;	
	[Tooltip("Gift animation")]
	public Animation gift;

	//Used to set gift time.
	public static float giftTimer;
	
	//Used to set timer.
	private int seconds, minutes, hours;

	//Custom gold color.
	private static Color32 gold;	

	void Start () 
	{
		//Set gold color.
		gold.r = 255; gold.g = 215; gold.b = 0; gold.a = 255;
		//Get timer from last session.
		giftTimer = PlayerPrefs.GetFloat("GiftTimer");
	}
	
	void Update () 
	{
		//If gift timer not 0
		if(giftTimer > 0)
		{
			//Decrease timer.
			giftTimer -= Time.deltaTime;
			//Set timer to player prefs.
			PlayerPrefs.SetFloat("GiftTimer", giftTimer);		

			//Convert timer to hours, minutes and seconds.
			seconds = (int)(giftTimer % 60);
			minutes = (int)(giftTimer / 60) % 60;
			hours = (int)(giftTimer / 3600) % 60;

			//Display timer on screen.
			giftTimerTxt.text = string.Format("{0:00}:{1:00}:{2:00}",hours,minutes,seconds);
			//Enable get now gift with ads.
			getNow.SetActive(true);
			//Disable claim button.
			claim.SetActive(false);
			
			//Disable gift animaions.
			gift.Rewind();
			gift.enabled = false;
		}
		//If timer ended.
		else
		{
			//Enable gift available animation.
			gift.enabled = true;
			//Change timer text to available.
			giftTimerTxt.text = "AVAILABLE";
			//Disable get now gift with ads.
			getNow.SetActive(false);
			//Enable claim button.
			claim.SetActive(true);
			//Enable gift animation.
			gift.Play("GiftAvailable");
			//Change gift button color to gold.
			giftButton.GetComponent<Image>().color = gold;
		}
	}
}
