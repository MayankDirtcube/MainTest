/*
* Copyright (c) DeckOfDev
* Email deckofdev@gmail.com
*/

using UnityEngine;
using UnityEngine.UI;

public class Ammo : MonoBehaviour {

	//Gun full ammo.
	public static int fullAmmo;
	//Custom colors.	
	public static Color32 green, red, grey;
	//Ammo text gameobject.
	private static Text ammoText;
	//Ammo glow gameobject.
	private static Transform ammo;
	//Bullet list images.
	private static Image[] bulletList = new Image[10];

	void Awake () 
	{
		//Get ammo text.
		ammoText = gameObject.GetComponent<Text>();
		//Get ammo transform.
		ammo = this.transform;

		//Set custom colors.
		green.r = 22; green.g = 188; green.b = 55; green.a = 255;
		red.r = 212; red.g = 21; red.b = 21; red.a = 255;
		grey.r = 60; grey.g = 60; grey.b = 60; grey.a = 255;
		
		//Get all bullet images.
		for(int i = 0; i < 10; i++)
			bulletList[i] = transform.GetChild(1).GetChild(i).gameObject.GetComponent<Image>();
	}

	public static void AmmoUpdate()
	{
		//Display ammo text.	
		ammoText.text = Gun.ammo.ToString();

		//If full ammo.
		if((float)Gun.ammo/(float)fullAmmo == 1)
		{
			//Set all bullets images color to green.
			for(int i = 0; i < 10; i++)
				bulletList[i].color = green;
			//Set ammo number text color to green. 
			ammoText.color = green;
			//Disable ammo glow object.
			ammo.GetChild(0).gameObject.SetActive(false);

		}
		//Checking what precentage ammo left and changing bullet colors to grey.
		else if((float)Gun.ammo/(float)fullAmmo >= 0.9f)
			bulletList[9].color = grey;
		else if((float)Gun.ammo/(float)fullAmmo >= 0.8f)
			bulletList[8].color = grey;
		else if((float)Gun.ammo/(float)fullAmmo >= 0.7f)
			bulletList[7].color = grey;
		else if((float)Gun.ammo/(float)fullAmmo >= 0.6f)
			bulletList[6].color = grey;
		else if((float)Gun.ammo/(float)fullAmmo >= 0.5f)
			bulletList[5].color = grey;
		else if((float)Gun.ammo/(float)fullAmmo >= 0.4f)
			bulletList[4].color = grey;
		else if((float)Gun.ammo/(float)fullAmmo >= 0.3f)
		{
			//Setting left bullets color to red.
			for(int i = 0; i < 3; i++)
				bulletList[i].color = red;
			for(int i = 3; i < 10; i++)
				bulletList[i].color = grey;
			//Enable ammo glow.
			ammo.GetChild(0).gameObject.SetActive(true);
			//Change ammo text color to red.
			ammoText.color = red;					
		}
		//Checking what precentage ammo left and changing bullet colors to grey.
		else if((float)Gun.ammo/(float)fullAmmo >= 0.2f)
			bulletList[2].color = grey;
		else if((float)Gun.ammo/(float)fullAmmo > 0)
			bulletList[1].color = grey;
		else if((float)Gun.ammo/(float)fullAmmo == 0)
			bulletList[0].color = grey;
	}
}
