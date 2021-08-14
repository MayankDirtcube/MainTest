/*
* Copyright (c) DeckOfDev
* Email deckofdev@gmail.com
*/

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class Coroutines : MonoBehaviour {

	private static GameObject gun;

//	private string privateCode = "5Mw1tNlhJUijJBU5Jwu7vwTyYdiHJJ30mE69jfn3uywA";
//	private string publicCode = "5b35b8cb191a8a0bcccf7604";

	private string privateCode;
	private string publicCode;
	const string webURL = "http://dreamlo.com/lb/";

	public static bool isConnected;

	public static Highscore[] highscoresList;
	public static int playerPlace = -1;


	void Start()
	{	
		privateCode = Data.properties.lead.privateCode;
		publicCode = Data.properties.lead.publicCode;
		StartCoroutine(checkInternetConnection());		
	}

	public static void SpawnGun()
	{
           gun = GameObject.FindWithTag("Gun");
	}


	public void ResumeClick()
	{
		if(gun.transform.GetChild(0).GetComponent<Rigidbody2D>().simulated == false)
		{
			StopCoroutine(EnableGunPhysics());
			gun.GetComponent<Gun>().enabled = true;
			Gun.Resume();
		}		
	}
	
	void Update () 
	{
		if(gun != null)
		{
			if(gun.transform.GetChild(0).GetComponent<Rigidbody2D>().simulated == false && Input.GetKeyDown("space"))
			{
				StopCoroutine(EnableGunPhysics());
				gun.GetComponent<Gun>().enabled = true;
				Gun.Resume();
			}
		}		
	}

	public void EnableCoroutine(string coroutine)
	{
		StartCoroutine(coroutine);
	}

	IEnumerator RestartScene()
	{
		yield return new WaitForSeconds(0.5f);
		SceneManager.LoadScene("FlipTheGun");	
	}

	IEnumerator EnableGunPhysics() {
        yield return new WaitForSeconds(1);
			gun.GetComponent<Gun>().enabled = true;
			Gun.Resume();
	}

	public void AddNewHighscore(string username, int score)
	{
			StartCoroutine(UploadNewhighscore(username, score));
	}

	IEnumerator UploadNewhighscore(string username, int score)
	{
		WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(username) + "/" + score);
		yield return www;

		if(string.IsNullOrEmpty(www.error))
		{
			Debug.Log("Upload Successful");
		}
		else
		{
			Debug.Log("Error uploading: " + www.error);
		}
	}

	public void DownloadHighscores()
	{
			StartCoroutine(DownloadHighscoresFromDatabase());
	}

	IEnumerator DownloadHighscoresFromDatabase()
	{
		WWW www = new WWW(webURL + publicCode + "/pipe/");
		yield return www;

		if(string.IsNullOrEmpty(www.error))
		{
			Debug.Log(www.text);
			FormatHighscores(www.text);
		}
		else
		{
			Debug.Log("Error downloading: " + www.error);
		}
	}

	void FormatHighscores(string textStream)
	{
		string[] entries = textStream.Split(new char[] {'\n'}, System.StringSplitOptions.RemoveEmptyEntries);
		highscoresList = new Highscore[entries.Length];
		for (int i = 0; i < entries.Length; i++)
		{
			string[] entryInfo = entries[i].Split(new char[] {'|'});
			string username = entryInfo[0];
			int score = int.Parse(entryInfo[1]);
			highscoresList[i] = new Highscore(username, score);
			if(highscoresList[i].username == MainMenu.username)
				playerPlace = i+1;
//			Debug.Log(highscoresList[i].username + ": " + highscoresList[i].score);
		}
		if(playerPlace == -1)
			playerPlace = highscoresList.Length+1;
	}

	IEnumerator checkInternetConnection()
	{
		WWW www = new WWW("http://google.com");
		yield return www;
		if(www.error != null)
			isConnected = false;
		else
		{
			isConnected = true;
			AddNewHighscore(MainMenu.username, Score.highScore);			
			DownloadHighscores();
		}
	}

	public static void AchieveChallenge(int achieve, int first, int second, int third)
	{		
		if(achieve == first)
		{
			if(PlayerPrefs.GetInt("Challenge" + Challenges.gunNumber + Challenges.challengeNumber) == 0)
				PlayerPrefs.SetInt("Challenge" + Challenges.gunNumber + Challenges.challengeNumber, 1);
			MainMenu.challengeGame.SetActive(true);
		}
		else if(achieve == second)
		{
			MainMenu.challengeGame.SetActive(true);
			if(PlayerPrefs.GetInt("Challenge" + Challenges.gunNumber + Challenges.challengeNumber) == 1)
				PlayerPrefs.SetInt("Challenge" + Challenges.gunNumber + Challenges.challengeNumber, 2);
			MainMenu.challengeGame.SetActive(true);
		}	
		else if(achieve == third)
		{
			MainMenu.challengeGame.SetActive(true);
			if(PlayerPrefs.GetInt("Challenge" + Challenges.gunNumber + Challenges.challengeNumber) == 2)
				PlayerPrefs.SetInt("Challenge" + Challenges.gunNumber + Challenges.challengeNumber, 3);	
			MainMenu.challengeGame.SetActive(true);
		}
	}
}

public struct Highscore 
{

	public string username;
	public int score;

	public Highscore(string username, int score)
	{
		this.username = username;
		this.score = score; 
	}
}
