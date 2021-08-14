/*
* Copyright (c) DeckOfDev
* Email deckofdev@gmail.com
*/

using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

	//Camera target.
	private static Transform cam;

	void Start()
	{
		//Attaching camera.
		cam = this.transform;
	}

	//Duration - how long camera will vibrate. Magnitude - how much camera will move to sides when vibrating.
	public static IEnumerator Shake(float duration, float magnitude)
	{
		//Setting original position.
		Vector3 originalPos = cam.localPosition;
		//Reseting timer.
		float elapsed = 0.0f;

		while(elapsed < duration)
		{
			//Getting random place.
			float x = Random.Range(-1f, 1f) * magnitude;


			//Setting up new camera position.
			cam.localPosition = new Vector3(x, cam.position.y, -1);

			elapsed += Time.deltaTime;

			//Starting function again.
			yield return null;
		}
		//Reseting camera position to original.
		cam.localPosition = new Vector3(0, cam.position.y, -1);
	}
}
