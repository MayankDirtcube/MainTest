/*
* Copyright (c) DeckOfDev
* Email deckofdev@gmail.com
*/

using UnityEngine;

public class GunCollider : MonoBehaviour {

	[Tooltip("Second gun place.")]
	public Transform clonePos;

	//Used to check if booster was used.
	public static bool boosterUsed;

	//Used to check how many boosters and bullets was collected.
	private int boosterCount;
	private int bulletCount;

	void Start()
	{
		//Reset stats.
		boosterCount = 0;
		bulletCount = 0;
	}

    void OnTriggerEnter2D(Collider2D col)
    {
		//If gun enters left side collider then second gun position changes to the right side.
        if (col.tag == "LSide")
        {
			transform.position = new Vector2(clonePos.position.x+5.64f, transform.position.y);
        }
		//If gun enters right side collider then second gun position changes to the left side.
		if (col.tag == "RSide")
        {
			transform.position = new Vector2(clonePos.position.x-5.64f, transform.position.y);
        }

		//If gun touch speed object.
		if (col.tag == "Speed")
        {
			//Gun gets flying up boost.
			Gun.speedUp = true;

#if UNITY_ANDROID
			//Device vibrates.
			if(SettingsMenu.vibration == 1)
				Vibration.Vibrate(50);
#endif
			
			//If challenge mode.
			if(ChallengeMenu.challengeActive[2])
			{
				//Adding boosters count.
				boosterCount++;
				//Updating challenge stats.
				Coroutines.AchieveChallenge(boosterCount, 5, 15, 25);
			}
			//If challenge mode.
			if(ChallengeMenu.challengeActive[4])
			{
				//Enabling booster bool.
				boosterUsed = true;
			}

			//Adding how many boosters has been used to achievements.
			PlayerPrefs.SetInt("CollectSpeed", (PlayerPrefs.GetInt("CollectSpeed")+1));				
		}

		//If gun touched bullet.
	 	if (col.tag == "Bullet")
        {
			//Gun is reloaded.
			Gun.reload = true;
			
			//Adding how many bullets has been used to achievements.
			PlayerPrefs.SetInt("CollectBullets", (PlayerPrefs.GetInt("CollectBullets")+1));	

			//If in challenge mode.
			if(ChallengeMenu.challengeActive[5])
			{
				//Adding bullet count.
				bulletCount++;
				//Updating challenge stats.
				Coroutines.AchieveChallenge(bulletCount, 5, 15, 25);
			}			
		}

		if (col.tag == "Blocker")
		{
			Gun.ResetForces();

#if UNITY_ANDROID
			//If vibrate is on vibrate device.
			if(SettingsMenu.vibration == 1)
				Vibration.Vibrate(50);
#endif			

			//Start shaking camera.
			StartCoroutine(CameraShake.Shake(0.1f, 0.2f));
		}

		//If gun reached bottom side collider then gun gameobject is disabled.
		if(col.tag == "BSide")
		{
			transform.parent.gameObject.SetActive(false);
		}
    }
}
