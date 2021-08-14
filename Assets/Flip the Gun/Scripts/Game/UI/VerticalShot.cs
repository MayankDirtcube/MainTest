/*
* Copyright (c) DeckOfDev
* Email deckofdev@gmail.com
*/

using UnityEngine;

public class VerticalShot : MonoBehaviour {

	//Used to get one of three vertical shot texts in this gameobject.
	private static Transform childs;

	void Start()
	{
		//Attach transfrom.
		childs = this.transform;
	}

	//Start vertical shot animation.
	public static void EnableAnimation()
	{
		//Play random animation.
		childs.GetChild(Random.Range(0,3)).GetComponent<Animation>().Play("VerticalShot");
	}
}
