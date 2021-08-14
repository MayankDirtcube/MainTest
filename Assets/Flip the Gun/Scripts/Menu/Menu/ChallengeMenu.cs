/*
* Copyright (c) DeckOfDev
* Email deckofdev@gmail.com
*/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChallengeMenu : MonoBehaviour {

	[Tooltip("Fade Menu(Animation)")]
	public Animation fadeMenu;
	[Tooltip("Fade Game(Animation)")]
	public Animation fadeGame;
	[Tooltip("GameCanvas(Gameobject)")]
	public GameObject gameCanvas;
	[Tooltip("MenuCanvas(Gameobject)")]
	public GameObject menuCanvas;
	[Tooltip("Main Camera(CameraMovement)")]
	public CameraMovement cameraMov;
	[Tooltip("Fade(Animation)")]
	public Animation fade;
	[Tooltip("Scroll View(Rect Transform)")]
	public Transform scrollPanel;
	[Tooltip("Challenge(Gameobject)")]
	public GameObject challenge;
	[Tooltip("Challenge Window(Rect Transform)")]
	public Transform challengeWindow;
	
	//Used to check which challenge is active.
	public static bool[] challengeActive = new bool[6];
	//Used to check if playing challenge.
	public static bool challengeMode = false;

	//Custom gold color.
	private static Color32 gold;	

	void Start()
	{
		//Set custom gold color.
		gold.r = 255; gold.g = 215; gold.b = 0; gold.a = 255;
		//Expand challenge scroll size according to gun amount.
		scrollPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(-12.4f, Data.gunsMenu.Length*300+300);
 
		for(int i = 1; i < Data.gunsMenu.Length; i++)
		{
			//Spawn gun challenge window.
			GameObject challengeClone = Instantiate(challenge,scrollPanel) as GameObject; 
			
			//Change name to gun number(In order to make programming a bit less chunky).
			challengeClone.transform.GetChild(1).name = i.ToString();
			//Get gun image.
			challengeClone.transform.GetChild(0).GetChild(3).GetComponent<Image>().sprite = Data.gunsMenu[i].GetComponent<GunUI>().gunSprite;

			//If gun is bought or free.
			if(PlayerPrefs.GetInt("GunBought" + i) == 1 || Data.gunsMenu[i].GetComponent<GunUI>().price == 0)
			{
				//Enable challenges.
				challengeClone.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
			}
			//If gun is not bought.
			else
			{
				//Enable locked window.
				challengeClone.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
			}
		}

		//Setup original challenge.
		challenge.transform.GetChild(1).name = "0";
		challenge.transform.GetChild(0).GetChild(3).GetComponent<Image>().sprite = Data.gunsMenu[0].GetComponent<GunUI>().gunSprite;
		challenge.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
		
	}

	//Play fade in animation.
	public void FadeIn()
	{
		fade.Play("FadeIn");
	}

	//Play fade out animation.
	public void FadeOut()
	{
		fade.Play("FadeOut");
	}

	//First Challenge(Set challenge number, check how many stars it got and write challenge).
	public void Award1()
	{
		Challenges.challengeNumber = 0;
		challengeWindow.GetChild(1).GetComponent<Text>().text = "1";
		SetStars();
		challengeWindow.GetChild(3).GetComponent<Text>().text = "Gain 25/50/100 points";
	}

	//Second Challenge(Set challenge number, check how many stars it got and write challenge).
	public void Award2()
	{
		Challenges.challengeNumber = 1;
		challengeWindow.GetChild(1).GetComponent<Text>().text = "2";
		SetStars();		
		challengeWindow.GetChild(3).GetComponent<Text>().text = "Shoot 30/60/100 times";		
	}

	//Third Challenge(Set challenge number, check how many stars it got and write challenge).
	public void Award3()
	{
		Challenges.challengeNumber = 2;
		challengeWindow.GetChild(1).GetComponent<Text>().text = "3";
		SetStars();	
		challengeWindow.GetChild(3).GetComponent<Text>().text = "Use booster 5/15/25 times";		
	}

	//Fourth Challenge(Set challenge number, check how many stars it got and write challenge).
	public void Award4()
	{
		Challenges.challengeNumber = 3;
		challengeWindow.GetChild(1).GetComponent<Text>().text = "4";
		SetStars();	
		challengeWindow.GetChild(3).GetComponent<Text>().text = "Collect 25/50/100 coins";		
	}

	//Fifth Challenge(Set challenge number, check how many stars it got and write challenge).
	public void Award5()
	{
		Challenges.challengeNumber = 4;
		challengeWindow.GetChild(1).GetComponent<Text>().text = "5";
		SetStars();	
		challengeWindow.GetChild(3).GetComponent<Text>().text = "Fly 25/50/100 meters without boosters";		
	}

	//Sixth Challenge(Set challenge number, check how many stars it got and write challenge).
	public void Award6()
	{
		Challenges.challengeNumber = 5;
		challengeWindow.GetChild(1).GetComponent<Text>().text = "6";
		SetStars();
		challengeWindow.GetChild(3).GetComponent<Text>().text = "Collect 5/15/25 bullets";		
	}

	//If pressed play button.
	public void Play()
	{
		//Start game.
		StartCoroutine(StartGame());
	}

	public IEnumerator StartGame()
	{	
		//Enable challenge
		challengeActive[Challenges.challengeNumber] = true;
		challengeMode = true;
		fadeMenu.Play("FadeIn");
		//Spawn selected gun.
		GameObject gun = Instantiate<GameObject>(Data.gunsGame[Challenges.gunNumber]);
		yield return new WaitForSeconds(0.25f);
		//Enable game menu.
		menuCanvas.SetActive(false);
		gameCanvas.SetActive(true);	
		//Enable gun.
		gun.SetActive(true);
		Coroutines.SpawnGun();
		fadeGame.Play("FadeOut");
		//Enable camera movement.
		cameraMov.enabled = true;
		//Update achievements stats.	
		PlayerPrefs.SetInt("GamesPlayed", (PlayerPrefs.GetInt("GamesPlayed")+1));	
	}
	void SetStars()
	{
		//Reset all stars to black color.
		challengeWindow.GetChild(2).GetChild(0).GetComponent<Image>().color = Color.black;
		challengeWindow.GetChild(2).GetChild(1).GetComponent<Image>().color = Color.black;
		challengeWindow.GetChild(2).GetChild(2).GetComponent<Image>().color = Color.black;	

		//If player got one star.
		if(PlayerPrefs.GetInt("Challenge" + Challenges.gunNumber + Challenges.challengeNumber) == 1)
		{
			//Change one star color to gold.
			challengeWindow.GetChild(2).GetChild(0).GetComponent<Image>().color = gold;
		}
		//If player got two stars.
		else if(PlayerPrefs.GetInt("Challenge" + Challenges.gunNumber + Challenges.challengeNumber) == 2)
		{
			//Change two stars colors to gold.
			challengeWindow.GetChild(2).GetChild(0).GetComponent<Image>().color = gold;
			challengeWindow.GetChild(2).GetChild(1).GetComponent<Image>().color = gold;
		}
		//If player got three stars.
		else if(PlayerPrefs.GetInt("Challenge" + Challenges.gunNumber + Challenges.challengeNumber) == 3)
		{
			//Change three stars colors to gold.
			challengeWindow.GetChild(2).GetChild(0).GetComponent<Image>().color = gold;
			challengeWindow.GetChild(2).GetChild(1).GetComponent<Image>().color = gold;
			challengeWindow.GetChild(2).GetChild(2).GetComponent<Image>().color = gold;
		}
	}
}

