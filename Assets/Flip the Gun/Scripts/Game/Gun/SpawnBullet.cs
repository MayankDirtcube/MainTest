/*
* Copyright (c) DeckOfDev
* Email deckofdev@gmail.com
*/

using UnityEngine;

public class SpawnBullet : MonoBehaviour {

	//If gun need first bullet and gun is in camera range then spawn bullet.
	public void BulletSpawn()
	{
		if(transform.GetChild(0).gameObject.activeSelf)
			Instantiate(Data.firstBullet);
	}
	//If gun need second bullet and gun is in camera range then spawn bullet.
	public void SecondBulletSpawn()
	{
		if(transform.GetChild(0).gameObject.activeSelf)
			Instantiate(Data.secondBullet);
	}	
	//If gun need third bullet and gun is in camera range then spawn bullet.
	public void ThirdBulletSpawn()
	{
		if(transform.GetChild(0).gameObject.activeSelf)
			Instantiate(Data.thirdBullet);
	}	
	//If gun need laser and gun is in camera range then spawn laser.
	public void LaserSpawn()
	{
		if(transform.GetChild(0).gameObject.activeSelf)
			Instantiate(Data.laser);		
	}
	//If gun need rocket and gun is in camera range then spawn rocket.
	public void RocketSpawn()
	{
		if(transform.GetChild(0).gameObject.activeSelf)
			Instantiate(Data.rocket);		
	}	

	//If gun need bullet shell and gun is in camera range then spawn bullet shell.
	public void BulletShellSpawn()
	{
		if(transform.GetChild(1).gameObject.activeSelf)
			Instantiate(Data.bulletShell);	
	}	
}
