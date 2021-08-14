/*
* Copyright (c) DeckOfDev
* Email deckofdev@gmail.com
*/

using UnityEngine;

public class Data : MonoBehaviour {

	public static FTG_Data properties;
    public static GameObject[] layers;
	public static GameObject highscoreLine;
	public static GameObject[] gunsGame, gunsMenu;
	public static GameObject scoreLines;

	
	public static GameObject firstBullet, secondBullet, thirdBullet, bulletShell;
	public static GameObject laser, rocket;

	void Awake () 
	{
		properties = Resources.Load<FTG_Data>("Data/Data");
		gunsGame = Resources.LoadAll<GameObject>("Prefabs/Guns/Game");
		gunsMenu = Resources.LoadAll<GameObject>("Prefabs/Guns/Menu");
		layers = Resources.LoadAll<GameObject>("Prefabs/Layers");
		highscoreLine = Resources.Load<GameObject>("Prefabs/UI/HighscoreLine");
		scoreLines = Resources.Load<GameObject>("Prefabs/UI/ScoreLines");

		firstBullet = Resources.Load<GameObject>("Prefabs/Bullets/BulletFly (1)");	
		secondBullet = Resources.Load<GameObject>("Prefabs/Bullets/BulletFly (2)");	
		thirdBullet = Resources.Load<GameObject>("Prefabs/Bullets/BulletFly (3)");
		laser = Resources.Load<GameObject>("Prefabs/Bullets/BulletFly (4)");
		rocket = Resources.Load<GameObject>("Prefabs/Bullets/BulletFly (5)");
		bulletShell = Resources.Load<GameObject>("Prefabs/Bullets/Bullet Shell");		
	}
}
