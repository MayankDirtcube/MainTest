/*
* Copyright (c) DeckOfDev
* Email deckofdev@gmail.com
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//[CreateAssetMenu(fileName = "Data", menuName = "Flip The Gun/Data")]
public class FTG_Data : ScriptableObject 
{
    public Ads ads = new Ads();
    public Leaderboards lead = new Leaderboards();
    public IAPurchase iap = new IAPurchase();
}

[System.Serializable]
public class Ads
{
    public bool testMode;
    public string androidAppID;
    public string iphoneAppID;
    public string androidUnitID;
    public string iphoneUnitID;
}

[System.Serializable]
public class Leaderboards
{
    public string privateCode;
	public string publicCode;
}

[System.Serializable]
public class IAPurchase
{
    public float removeAdsPrice = 3.99f;
	public float pileOfCoinsPrice = 5.99f;
	public float sniperPackPrice = 2.99f;
	public float rocketPackPrice = 3.99f;
}