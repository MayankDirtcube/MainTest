/*
* Copyright (c) DeckOfDev
* Email deckofdev@gmail.com
*/

using UnityEngine;

public class HighscoreLine : MonoBehaviour {

	//Used to fade out highscore line.
	private Animation fadeOut;

	void Start()
	{
		//Attach highscore fade out line.
		fadeOut = gameObject.GetComponent<Animation>();
	}

    void OnTriggerEnter2D(Collider2D col)
	{
		//If gun reaches highscore line.
        if (col.tag == "GunUI")
        {
			//Start fade out animation.
			fadeOut.Play("HighscoreFadeOut");
		}		
	}

	//After animation ends destroy gameobject.
	public void DestroyObject()
	{
		Destroy(gameObject);
	}
}
