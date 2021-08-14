/*
* Copyright (c) DeckOfDev
* Email deckofdev@gmail.com
*/

using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	
    void OnTriggerEnter2D(Collider2D col)
    {
		//If gun touched bullet.
        if (col.tag == "GunUI")
        {
			StartCoroutine(BulletDestroy());
		}
	}

	IEnumerator BulletDestroy()
	{
		//Start bullet destroy animation.
		transform.parent.GetComponent<Animation>().Play("BulletDestroy");
		//Wait when animation ends.
		yield return new WaitForSeconds(0.7f);
		//Destroy bullet gameobject.
		Destroy(transform.parent.gameObject);
	}
}
