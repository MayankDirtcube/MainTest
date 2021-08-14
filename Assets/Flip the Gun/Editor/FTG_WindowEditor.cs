/*
* Copyright (c) DeckOfDev
* Email deckofdev@gmail.com
*/

using UnityEngine;
using UnityEngine.UI;
using UnityEditor;


public class FTG_WindowEditor : EditorWindow {

	FTG_Data data;

	public GUISkin customSkin;

    public Vector2 scrollPosition;


	//Starting window editor
	[MenuItem("Window/Flip The Gun Settings")]
	public static void ShowWindow()
	{
		//Setting window editor min size
		GetWindow<FTG_WindowEditor>("FTG Settings").minSize = new Vector2 (300, 500);
	}

	void OnEnable()
	{
		data = Resources.Load ("Data/Data") as FTG_Data;
	}

	void OnGUI()
	{
		GUI.skin = customSkin;
		SerializedObject so = new SerializedObject(data);
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(Screen.width), GUILayout.Height(Screen.height-20));


		Advertisements();
		Leaderboards();
		IAPurchase();

		if(GUILayout.Button("Search in Documentation", GUILayout.Height(22)))	
			Application.OpenURL((Application.dataPath) + "/Flip The Gun/Documentation/Manual.pdf");		

		GUILayout.EndScrollView();

		so.ApplyModifiedProperties();	
		EditorUtility.SetDirty(data);
	}

	void Advertisements()
	{
		GUILayout.BeginVertical("box");
			GUILayout.TextField("Unity Ads", EditorStyles.boldLabel);
			if(!FTG_AssemblyReferences.IsClassActive ("Advertisement"))
				EditorGUILayout.HelpBox("Unity Ads is not enabled in the services. Some parts of the game will not work! In order to enable Unity Ads go to Window/Services and turn on Ads then restart project.", MessageType.Error);
			else
				EditorGUILayout.HelpBox("Unity Ads is working!", MessageType.Info);

				if(GUILayout.Button("Visit Unity Ads Dashboard"))	
					Application.OpenURL("https://operate.dashboard.unity3d.com/");
				if(GUILayout.Button("Unity Ads Quick Start guide"))		
					Application.OpenURL("https://unity3d.com/services/ads/quick-start-guide");
		GUILayout.EndVertical();

		GUILayout.BeginVertical("box");
			GUILayout.TextField("AdMob", EditorStyles.boldLabel);
			if (!FTG_AssemblyReferences.IsClassActive ("MobileAds"))
			{
				EditorGUILayout.HelpBox("AdMob is not installed in this project. Please download and install AdMob package in order to show ads. After installing AdMob package restart project.", MessageType.Error);
				if(GUILayout.Button("Download package files"))
					Application.OpenURL("https://github.com/googleads/googleads-mobile-unity/releases/tag/v3.14.0");
			}
			else
			{
				AddDefine("ADMOB");				
				EditorGUILayout.HelpBox("AdMob is working!", MessageType.Info);
				data.ads.testMode = EditorGUILayout.Toggle("Test Mode", data.ads.testMode);
				data.ads.androidAppID = EditorGUILayout.TextField("Android App ID", data.ads.androidAppID);
				data.ads.iphoneAppID = EditorGUILayout.TextField("Iphone App ID", data.ads.iphoneAppID);					
				data.ads.androidUnitID = EditorGUILayout.TextField("Android Unit ID", data.ads.androidUnitID);
				data.ads.iphoneUnitID = EditorGUILayout.TextField("Iphone Unit ID", data.ads.iphoneUnitID);			
			}
			if(GUILayout.Button("Visit AdMob Dashboard"))	
				Application.OpenURL("https://apps.admob.com/v2");
			if(GUILayout.Button("AdMob Quick Start guide"))		
				Application.OpenURL("https://developers.google.com/admob/unity/start");
		
		GUILayout.EndVertical();
	}

	void Leaderboards()
	{
		GUILayout.BeginVertical("box");
			GUILayout.TextField("Leaderboard (Dreamlo)", EditorStyles.boldLabel);
			data.lead.privateCode = EditorGUILayout.TextField("Private Code", data.lead.privateCode);
			data.lead.publicCode = EditorGUILayout.TextField("Public Code", data.lead.publicCode);
			if(data.lead.privateCode != "")
				if(GUILayout.Button("Visit Dreamlo Dashboard"))	
					Application.OpenURL("http://dreamlo.com/lb/" + data.lead.privateCode);

			if(GUILayout.Button("Visit Dreamlo website"))	
					Application.OpenURL("http://dreamlo.com");
			
		GUILayout.EndVertical();
	}

	void IAPurchase()
	{
		GUILayout.BeginVertical("box");
			GUILayout.TextField("In-App Purchase", EditorStyles.boldLabel);	
			if (!FTG_AssemblyReferences.IsClassActive ("StandardPurchasingModule"))
			{
				EditorGUILayout.HelpBox("Unity In-App Purchase is not enabled in the services. Some parts of the game will not work! In order to enable Unity In-App Purchase go to Window/Services and turn on In-App Purchasing and select to import store package. Then restart project.", MessageType.Error);
				EditorGUILayout.HelpBox("If you still get this message Unity Analytics probably is disabled. Go to Window/Services and enable Unity Analytics.", MessageType.Error);
			}
			else
			{
				AddDefine("IAP");								
				EditorGUILayout.HelpBox("Unity In-App Purchase is working!", MessageType.Info);
				data.iap.removeAdsPrice = EditorGUILayout.FloatField(new GUIContent("Remove Ads Price", "removeads"), data.iap.removeAdsPrice);
				data.iap.pileOfCoinsPrice = EditorGUILayout.FloatField(new GUIContent("Pile of Coins Price","pileofcoins"), data.iap.pileOfCoinsPrice);
				data.iap.sniperPackPrice = EditorGUILayout.FloatField(new GUIContent("Sniper Pack Price", "sniperpack"), data.iap.sniperPackPrice);
				data.iap.rocketPackPrice = EditorGUILayout.FloatField(new GUIContent("Rocket Pack Price", "rocketpack"), data.iap.rocketPackPrice);
			}
			if(GUILayout.Button("Visit Google Play Dashboard"))	
				Application.OpenURL("https://play.google.com/apps/publish");
			if(GUILayout.Button("Visit Unity Analytics Dashboard"))	
				Application.OpenURL("https://analytics.cloud.unity3d.com");			
			if(GUILayout.Button("IAPurchase Quick Start guide"))		
				Application.OpenURL("https://unity3d.com/learn/tutorials/topics/ads-analytics/integrating-unity-iap-your-game");
		GUILayout.EndVertical();		
	}

	void AddDefine(string define)
	{
		//Get defines
		BuildTargetGroup buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
		string defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);

		//Append only if not defined already
		if (defines.Contains(define))
		{
			return;
		}

		//Append
		PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, (defines + ";" + define));
	}	
}