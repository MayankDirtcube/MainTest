/*
* Copyright (c) DeckOfDev
* Email deckofdev@gmail.com
*/

using UnityEngine;

public class CameraMovement : MonoBehaviour {

	[Tooltip("How fast camera moves to target")]
	public float smoothSpeed = 0.125f;

	//Default screen width.
	private float horizontalResolution = 550;
	//Gun will be attached to this target.
	private Transform target;
	//Camera position to target.
	private Vector3 offSet;

	void Start()
	{

		//Finding gun object.
        target = GameObject.FindWithTag("GunUI").transform;
		//Setting up camera offset.
		offSet = new Vector3(0,1,-1);
		//Getting screen aspect ratio.
        float currentAspect = (float) Screen.width / (float) Screen.height;
		//Changing size according to screen aspect ratio.
    	Camera.main.orthographicSize = horizontalResolution / currentAspect / 200;

	}

	void LateUpdate () 
	{

		//Setting new destination position.
		Vector3 destinationPos = target.position + offSet;
		//Smoothly changing camera position to destination position.
		Vector3 smoothPos = Vector3.Lerp(transform.position, destinationPos, smoothSpeed);
		//If gun falls down camera stops moving.
		if(transform.position.y < destinationPos.y)
			transform.position = new Vector3(transform.position.x, smoothPos.y, -1);
	}
}
