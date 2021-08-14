/*
* Copyright (c) DeckOfDev
* Email deckofdev@gmail.com
*/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Gun : MonoBehaviour {

	[Tooltip("How much gun will fly up.")]
    public float gunPower;
	[Tooltip("How fast gun will rotate.")]
	public float rotationSpeed;
	[Tooltip("How long gun will recoil.")]
	public float recoilTime;
	[Tooltip("How much ammo gun has.")]
	public int fullAmmo;

	//Static ammo variable.
	public static int ammo;
	//Used to check if gun needs to go fast up.
	public static bool speedUp = false;
	//Used to check if gun reloads.
	public static bool reload = false;

	//Used to check how many times gun has been shot(For challenges, achievements).
	private int shotsFired;
	//USed to check if gun shot was first in the game.
	private static bool firstShot = true;
	//Used to check if gun is recoiling.
	private static bool recoil = false;

	//Gun shot sound.
	private AudioSource shootSound;
	//Gun empty ammo sound.
	private AudioSource emptySound;
	//Get both guns.
	private static Rigidbody2D gunRight, gunLeft;

    void Awake()
	{
		//Reset shot count.
		shotsFired = 0;
		//Load both guns.
        gunRight = transform.GetChild(0).GetComponent<Rigidbody2D>();
		gunLeft = transform.GetChild(1).GetComponent<Rigidbody2D>();
		//Load ammo.
		ammo = fullAmmo;
		Ammo.fullAmmo = fullAmmo;
		//Set sounds.
		shootSound = gameObject.GetComponent<AudioSource>();
		emptySound = transform.GetChild(0).GetComponent<AudioSource>();

		//Reset stats.
		reload = false;
		firstShot = true;
		recoil = false;
	}

	void Start()
	{
		//Update ammo.
		Ammo.AmmoUpdate();

		//If in challenge mode make gun to rise from bottom.
		if(ChallengeMenu.challengeMode)		
			transform.position = new Vector2(0, -4);
		
		//First time gun will fly up automatically.
		StartCoroutine(ShootVerticallyUp(1f));			
	}

	void FixedUpdate()
	{
		//If player pressed space, gun still has ammo and it's not recoiling.
		if(Input.GetKey("space") && ammo > 0 && !recoil)
		{
			//Shoot.
			StartCoroutine(Shoot());
			//Update challenge stats.
			if(ChallengeMenu.challengeActive[1])
			{
				shotsFired++;
				Coroutines.AchieveChallenge(shotsFired, 30, 60, 100);
			}			
		}
		//If player pressed space and gun has no ammo.
		else if(Input.GetKey("space") && ammo == 0)
		{
			//Play empty ammo sound.
			emptySound.Play();
			//Start red screen glow animation.
			ScreenGlow.StartGlow();

			//Start shaking camera.
			StartCoroutine(CameraShake.Shake(0.1f, 0.2f));
		}
		//If player touch screen with finger, gun still has ammo and it's not recoiling.
		if(FingerInput.isTouching && ammo > 0 && !recoil)
		{
			//Shoot.
			StartCoroutine(Shoot());
			//Update challenge stats.
			if(ChallengeMenu.challengeActive[1])
			{
				shotsFired++;
				Coroutines.AchieveChallenge(shotsFired, 30, 60, 100);
			}				
		}
		//If player pressed touch screen but gun has no ammo.
		else if(FingerInput.isTouching && ammo == 0)
		{		
			//Play empty ammo sound.
			emptySound.Play();
			//Start red screen glow animation.
			ScreenGlow.StartGlow();
#if UNITY_ANDROID
			//If vibrate is on vibrate device.
			if(SettingsMenu.vibration == 1)
				Vibration.Vibrate(50);
#endif
			//Start shaking camera.
			StartCoroutine(CameraShake.Shake(0.15f, 0.2f));
		}

		//If gun collide with booster.
		if(speedUp)
		{
			//Shoot gun up.
			StartCoroutine(ShootVerticallyUp(1f));	
			//Start shaking camera.
			StartCoroutine(CameraShake.Shake(0.1f, 0.2f));	
			//Disable speed bool.
			speedUp = false;
		}

		//If gun is reloading.	
		if(reload)
		{
			//Reset ammo.
			ammo = fullAmmo;
			Ammo.AmmoUpdate();	
			reload = false;						
		}
				
	}

	//If gun shoot.
	IEnumerator Shoot()
	{
		//Start recoil.
		recoil = true;

		//Reset spinning forces.
		ResetVelocity();

		//Enable gun shot animations.
		transform.GetChild(0).GetChild(0).GetComponent<Animation>().Play();
		transform.GetChild(1).GetChild(0).GetComponent<Animation>().Play();

		//Fly gun up according to it's own rotation.
		StartCoroutine(ShootVerticallyUp(Mathf.Abs(1-(transform.GetChild(0).eulerAngles.z/180))));		
    	ShootUp();		

		//Change gun rotation.
		if(transform.GetChild(0).eulerAngles.z < 180)
		{
			ChangeTorque(rotationSpeed);	
		}
		else
		{
			ChangeTorque(-rotationSpeed);
		}
			
#if UNITY_ANDROID
		//If vibrate is on vibrate device.
		if(SettingsMenu.vibration == 1)
				Vibration.Vibrate(50);
#endif

		//If gun shot is vertically down.
		if(transform.GetChild(0).eulerAngles.z >= -2.5f && transform.GetChild(0).eulerAngles.z <= 2.5f)
		{
			//If gun shot is not the first.
			if(!firstShot)
			{
				//Enable vertical shot animation and update achievements stats.
				VerticalShot.EnableAnimation();
				PlayerPrefs.SetInt("VerticalShots", (PlayerPrefs.GetInt("VerticalShots")+1));				
			}
		}

		//Decrease and update ammo.
		ammo--;
		Ammo.AmmoUpdate();

		//If out of ammo update achievements stats.
		if(ammo == 0)
			PlayerPrefs.SetInt("RunOutAmmo", (PlayerPrefs.GetInt("RunOutAmmo")+1));

		//Update achievements stats.
		PlayerPrefs.SetInt("ShootTimes", (PlayerPrefs.GetInt("ShootTimes")+1));

		//If gun shot was first disable bool.
		if(firstShot)
			firstShot = false;

		//Wait custom time to recoil.
		yield return new WaitForSeconds(recoilTime);
		recoil = false;
	}

	//Change gun rotation.
	void ChangeTorque(float rotSpeed)
	{
	 	gunRight.AddTorque(rotSpeed, ForceMode2D.Impulse);
		gunLeft.AddTorque(rotSpeed, ForceMode2D.Impulse);
	}

	//Shoot gun up.
	void ShootUp()
	{
		//Play gun sound.
		shootSound.Play();
		ResetForces();	
		//Force gun to fly up with custom amount of force.
		gunRight.AddForce(transform.GetChild(0).up * gunPower * 0.25f);						
		gunLeft.AddForce(transform.GetChild(0).up * gunPower * 0.25f);			
	}

	//Shoot gun vertically up.
	IEnumerator ShootVerticallyUp(float speed)
	{
		ResetForces();	
		//Force gun to fly up.
	 	gunRight.AddForce(new Vector2(0,1) * gunPower * speed);	
	 	gunLeft.AddForce(new Vector2(0,1) * gunPower * speed);	
		yield return new WaitForSeconds(0.25f);
	}

	//Stop gun moving.
	public static void ResetForces()
	{
		gunRight.velocity = Vector2.zero;
		gunLeft.velocity = Vector2.zero;
	}

	//Stop gun rotating.
	void ResetVelocity()
	{
		gunRight.angularVelocity = 0;	
		gunLeft.angularVelocity = 0;
	}

	//Enable gun physics.
	public static void Resume()
	{
		gunRight.simulated = true;
		gunLeft.simulated = true;
	}

	//Disable gun physics.
	public static void Pause()
	{
		gunRight.simulated = false;
		gunLeft.simulated = false;
	}
}