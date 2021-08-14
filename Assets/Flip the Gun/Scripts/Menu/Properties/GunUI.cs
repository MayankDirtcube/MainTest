/*
* Copyright (c) DeckOfDev
* Email deckofdev@gmail.com
*/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GunUI : MonoBehaviour {

	[Tooltip("Whole gun image.")]
	public Sprite gunSprite;
	[Tooltip("Gun price")]
	public int price;
	[Tooltip("Used to check if gun is selected(Don't Change)")]
	public bool selected;

	//Used to check if gun is not purchased.
	public static bool isBuyable;

	//Play button.
	private Transform play;
	//Used to change gun name.
	private Transform gunName;
	//Used to make gun sounds.
	private AudioSource shootSound;

	void Awake()
	{
		//Reset all guns.
		selected = false;
		//Find gun name gameobject place.
		gunName = transform.parent.parent.parent.GetChild(0);
		//Find play button gameobject place.
		play = transform.parent.parent.parent.parent.GetChild(1).GetChild(0);
		//Get shoot sound gameobject.
		shootSound = gameObject.GetComponent<AudioSource>();
		//Shrink all guns.
		gameObject.GetComponent<Animation>().Play("Shrink");
	}

	void Start()
	{
		//Change  button to Play.
		play.GetComponent<Animation>().Play("BuyToPlay");
	}

	void OnMouseDown()
	{
		//If gun is touched enable gun animation and play shoot sound.
		transform.GetChild(0).GetComponent<Animation>().Play();
		shootSound.Play();
	}

    void OnTriggerEnter2D(Collider2D col)
    {
		//If gun is in center(selected)
        if (col.tag == "ScrollCenter")
        {
			//Enables bulletplace and bulletshellplace gameobjects.
			transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
			transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
			//Gun is selected.
			selected = true;
			//Changes selected gun number.
			MainMenu.gunNumber = transform.GetSiblingIndex();
			//Gun growing animation.
			gameObject.GetComponent<Animation>().Play("Grow");
			
			//If gun has price and it hasn't been bought.
			if(PlayerPrefs.GetInt("GunBought" + MainMenu.gunNumber) == 0 && price > 0)
			{
				//It can be bought.
				isBuyable = true;
				//Change play button to buy.
				if(play.GetChild(0).gameObject.activeSelf)
					play.GetComponent<Animation>().Play("PlayToBuy");
				//Change play text to gun price.
				play.GetChild(1).GetComponent<Text>().text = price.ToString();
			}
			//If gun is free or has been bought.
			else
			{
				//It can't be purchased.
				isBuyable = false;
				//Change buy button to play.
				if(!play.GetChild(0).gameObject.activeSelf)
					play.GetComponent<Animation>().Play("BuyToPlay");
			}
			//Start changing gun name.
			StartCoroutine(ChangeName());
		}
	}

    void OnTriggerExit2D(Collider2D col)
    {       
		//If gun left center(not selected).
		if (col.tag == "ScrollCenter")
        {
			//Disable bulletplace and bulletshellplace gameobjects.
			transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
			transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
			//Gun is not selected.
			selected = false;	
			//Shrink gun animation.	
			gameObject.GetComponent<Animation>().Play("Shrink");
		}
	}

	IEnumerator ChangeName()
	{
		//Play gun name fading out animation.
		gunName.GetComponent<Animation>().Play("GunNameFadeOut");
		//Wait when animation ends.
		yield return new WaitForSeconds(0.085f);
		//Change gun name to gun which is in center(selected).
		gunName.GetComponent<Text>().text = Data.gunsMenu[MainMenu.gunNumber].name.Substring(10);
		//Play gun name fading in animation.
		gunName.GetComponent<Animation>().Play("GunNameFadeIn");
	}

	public void ShrinkGun()
	{
			//Shrink guns to scale (0,0)
			gameObject.GetComponent<Animation>().Play("FullyShrink");
	}
}
