/*
* Copyright (c) DeckOfDev
* Email deckofdev@gmail.com
*/

using UnityEngine;
using UnityEngine.UI;

public class AchievementsMenu : MonoBehaviour {

	[Tooltip("Fade(Animation)")]
    public Animation fade;
	[Tooltip("Award s(Not Reached)(Rect Transform)")]
	public Transform awards;
	[Tooltip("Award List(Rect Transform)")]
	public Transform awardList;

	void Start()
	{
		//Load all awards.
		EnableAward(1,Score.highScore, 100);
		EnableAward(2,Score.highScore, 250);
		EnableAward(3,Score.highScore, 500);
		EnableAward(4,PlayerPrefs.GetInt("ShootTimes"), 500);
		EnableAward(5,PlayerPrefs.GetInt("ShootTimes"), 2000);
		EnableAward(6,PlayerPrefs.GetInt("ShootTimes"), 5000);
		EnableAward(7,PlayerPrefs.GetInt("GamesPlayed"), 10);
		EnableAward(8,PlayerPrefs.GetInt("GamesPlayed"), 50);
		EnableAward(9,PlayerPrefs.GetInt("GamesPlayed"), 150);
		EnableAward(10,PlayerPrefs.GetInt("VerticalShots"), 10);
		EnableAward(11,PlayerPrefs.GetInt("VerticalShots"), 100);
		EnableAward(12,PlayerPrefs.GetInt("VerticalShots"), 500);
		EnableAward(13,PlayerPrefs.GetInt("RunOutAmmo"), 10);
		EnableAward(14,PlayerPrefs.GetInt("RunOutAmmo"), 25);
		EnableAward(15,PlayerPrefs.GetInt("RunOutAmmo"), 100);
		EnableAward(16,PlayerPrefs.GetInt("CollectCoins"), 1000);
		EnableAward(17,PlayerPrefs.GetInt("CollectCoins"), 2500);
		EnableAward(18,PlayerPrefs.GetInt("CollectCoins"), 5000);
		EnableAward(19,PlayerPrefs.GetInt("OneSessionCoins"), 10);
		EnableAward(20,PlayerPrefs.GetInt("OneSessionCoins"), 25);
		EnableAward(21,PlayerPrefs.GetInt("OneSessionCoins"), 50);
		EnableAward(22,PlayerPrefs.GetInt("CollectGifts"), 10);
		EnableAward(23,PlayerPrefs.GetInt("CollectGifts"), 25);
		EnableAward(24,PlayerPrefs.GetInt("CollectGifts"), 50);
		EnableAward(25,PlayerPrefs.GetInt("CollectSpeed"), 50);
		EnableAward(26,PlayerPrefs.GetInt("CollectSpeed"), 250);
		EnableAward(27,PlayerPrefs.GetInt("CollectSpeed"), 500);
		EnableAward(28,PlayerPrefs.GetInt("CollectBullets"), 50);
		EnableAward(29,PlayerPrefs.GetInt("CollectBullets"), 250);
		EnableAward(30,PlayerPrefs.GetInt("CollectBullets"), 500);
	}

	//Play fade in animation.
	public void FadeIn()
	{
		fade.Play("FadeIn");
	}

	//Play fade out animation.
	public void FadeOut()
	{
		fade.Play("FadeOut");
	}

	public void Award01()
	{
		Award(0, Score.highScore, 100);
	}

	public void Award02()
	{
		Award(1, Score.highScore, 250);
	}

	public void Award03()
	{
		Award(2, Score.highScore, 500);
	}

	public void Award04()
	{
		Award(3, PlayerPrefs.GetInt("ShootTimes"), 500);
	}

	public void Award05()
	{
		Award(4, PlayerPrefs.GetInt("ShootTimes"), 2000);
	}

	public void Award06()
	{
		Award(5, PlayerPrefs.GetInt("ShootTimes"), 5000);
	}

	public void Award07()
	{
		Award(6, PlayerPrefs.GetInt("GamesPlayed"), 10);
	}

	public void Award08()
	{
		Award(7, PlayerPrefs.GetInt("GamesPlayed"), 50);
	}

	public void Award09()
	{
		Award(8, PlayerPrefs.GetInt("GamesPlayed"), 150);
	}

	public void Award10()
	{
		Award(9, PlayerPrefs.GetInt("VerticalShots"), 10);
	}

	public void Award11()
	{
		Award(10, PlayerPrefs.GetInt("VerticalShots"), 100);
	}

	public void Award12()
	{
		Award(11, PlayerPrefs.GetInt("VerticalShots"), 500);
	}

	public void Award13()
	{
		Award(12, PlayerPrefs.GetInt("RunOutAmmo"), 10);
	}

	public void Award14()
	{
		Award(13, PlayerPrefs.GetInt("RunOutAmmo"), 25);
	}

	public void Award15()
	{
		Award(14, PlayerPrefs.GetInt("RunOutAmmo"), 100);
	}

	public void Award16()
	{
		Award(15, PlayerPrefs.GetInt("CollectCoins"), 1000);
	}

	public void Award17()
	{
		Award(16, PlayerPrefs.GetInt("CollectCoins"), 2500);
	}

	public void Award18()
	{
		Award(17, PlayerPrefs.GetInt("CollectCoins"), 5000);
	}

	public void Award19()
	{
		Award(18, PlayerPrefs.GetInt("OneSessionCoins"), 10);
	}

	public void Award20()
	{
		Award(19, PlayerPrefs.GetInt("OneSessionCoins"), 25);
	}

	public void Award21()
	{
		Award(20, PlayerPrefs.GetInt("OneSessionCoins"), 50);
	}

	public void Award22()
	{
		Award(21, PlayerPrefs.GetInt("CollectGifts"), 10);
	}

	public void Award23()
	{
		Award(22, PlayerPrefs.GetInt("CollectGifts"), 25);
	}

	public void Award24()
	{
		Award(23, PlayerPrefs.GetInt("CollectGifts"), 50);
	}

	public void Award25()
	{
		Award(24, PlayerPrefs.GetInt("CollectSpeed"), 50);
	}

	public void Award26()
	{
		Award(25, PlayerPrefs.GetInt("CollectSpeed"), 250);
	}

	public void Award27()
	{
		Award(26, PlayerPrefs.GetInt("CollectSpeed"), 500);
	}

	public void Award28()
	{
		Award(27, PlayerPrefs.GetInt("CollectBullets"), 50);
	}

	public void Award29()
	{
		Award(28, PlayerPrefs.GetInt("CollectBullets"), 250);
	}

	public void Award30()
	{
		Award(29, PlayerPrefs.GetInt("CollectBullets"), 500);
	}

	void Award(int award, int score, int highscore)
	{
		//Display score text.
		Text scoreTxt = awards.GetChild(award).GetChild(2).GetComponent<Text>();

			scoreTxt.text = score.ToString() + "/" + highscore.ToString();
	}

	void EnableAward(int award, int score, int highscore)
	{
		//If score is higher than highscore then enable reached award.
		if(score >= highscore)
		{
			awardList.GetChild(award*2).gameObject.SetActive(true);
		}
		//If score is lower than highscore then enable not reached award.
		else
		{
			awardList.GetChild((award*2)-1).gameObject.SetActive(true);
		}
	}
}
