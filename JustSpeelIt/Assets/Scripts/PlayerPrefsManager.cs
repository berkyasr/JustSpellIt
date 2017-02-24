using UnityEngine;
using System.Collections;

public class PlayerPrefsManager : MonoBehaviour {
	const string UNLOCKED_LEVEL= "LEVEL_KEY";
	const string HIGHEST_SCORE = "SCORE_KEY";
	// Use this for initialization
	public static void SetUnlockedLevel(int levelNumber){
		PlayerPrefs.SetInt (UNLOCKED_LEVEL, levelNumber);
	}
	public static int GetUnlockedLevel(){
		return PlayerPrefs.GetInt (UNLOCKED_LEVEL);
	}
	public static void SetNewHighScore(int newScore){
		PlayerPrefs.SetInt (HIGHEST_SCORE, newScore);
	}
	public static int GetHighScore(){
		return PlayerPrefs.GetInt (HIGHEST_SCORE);
	}
}
