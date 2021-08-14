/*
* Copyright (c) DeckOfDev
* Email deckofdev@gmail.com
*/

using UnityEngine;

public class LayersSpawn : MonoBehaviour {

	private int n;
	//How much layers was spawned.
	private static int amount;

	void Start () 
	{
		//Reset amount.
		amount = 0;
		//Spawn first 10 layers.
		while(amount < 10)
		{
			//Get random range.
			int n = Random.Range(-7,16);
			//If range is positive.
			if(n >= 0)
			{
				//Spawn layer with random integer.
				GameObject layer = Instantiate(Data.layers[n]) as GameObject;
				//Place layer in specific position.
				layer.transform.position = new Vector2(0, amount * 1.5f);
			}
			//If range is negative.
			else
			{
				//Spawn blank layer.
				GameObject layer = Instantiate(Data.layers[0]) as GameObject;
				//Place layer in specific position.
				layer.transform.position = new Vector2(0, amount * 1.5f);				
			}
			//Increase amount.
			amount++;			
		}	
	}
	
    void OnTriggerEnter2D(Collider2D col)
    {
		//If layer entered bottom collider.
        if (col.tag == "Layer")
        {
			if(amount%40==0)
			{
				
				int n = Random.Range(17,21);
				//Spawn layer with random integer.
				GameObject layer = Instantiate(Data.layers[n]) as GameObject;
				//Place layer in specific position.
				layer.transform.position = new Vector2(0, amount * 1.5f);					
			}
			//If 15 layers has been spawned.
			else if(amount%15==0)
			{
				//Spawn random bullet layer.
				int n = Random.Range(8,12);
				GameObject layer = Instantiate(Data.layers[n]) as GameObject;
				//Place layer in specific position.
				layer.transform.position = new Vector2(0, amount * 1.5f);	
			}
			//If 10 layers has been spawned.
			else if(amount%10==0)
			{
				//Get random range.
				n = Random.Range(1,8);
				if(n < 4)
				{
					//Spawn blank layer.
					GameObject layer = Instantiate(Data.layers[0]) as GameObject;
					//Place layer in specific position.
					layer.transform.position = new Vector2(0, amount * 1.5f);						
				}
				else
				{
					//Spawn random coin layer.
					GameObject layer = Instantiate(Data.layers[n]) as GameObject;
					//Place layer in specific position.
					layer.transform.position = new Vector2(0, amount * 1.5f);	
				}			
			}
			else if(amount%5==0)
			{
				//Get random range.
				n = Random.Range(10,16);
				if(n < 12)
				{
					//Spawn blank layer.
					GameObject layer = Instantiate(Data.layers[0]) as GameObject;
					//Place layer in specific position.
					layer.transform.position = new Vector2(0, amount * 1.5f);						
				}
				else
				{
					//Spawn random speed layer.
					GameObject layer = Instantiate(Data.layers[n]) as GameObject;
					//Place layer in specific position.
					layer.transform.position = new Vector2(0, amount * 1.5f);	
				}			
			}			
			else 
			{
				//Get random range.
				n = Random.Range(-3,5);
				if(n >= 0)
				{
					//Spawn random coin layer.
					GameObject layer = Instantiate(Data.layers[n]) as GameObject;
					//Place layer in specific position.
					layer.transform.position = new Vector2(0, amount * 1.5f);
				}
				else
				{
					//Spawn blank layer.
					GameObject layer = Instantiate(Data.layers[0]) as GameObject;
					//Place layer in specific position.
					layer.transform.position = new Vector2(0, amount * 1.5f);				
				}
			}
			//Increase amount.
			amount++;				
		}
	}

	//If player reaches his highscore.
	public static void SpawnHighscoreLine()
	{
		//Spawn highscore line layer.
		GameObject highScore = Instantiate(Data.highscoreLine) as GameObject;
		//Place layer in specific position.
		highScore.transform.position = new Vector2(0, amount * 1.5f + 16);				
	}
}
