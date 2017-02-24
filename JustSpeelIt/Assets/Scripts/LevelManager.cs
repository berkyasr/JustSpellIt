using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {
	private float timer =0f; 
	public void LoadNextLevel(){
		Application.LoadLevel (Application.loadedLevel + 1);
	}
	public void Exit(){
		Application.Quit ();
	}
	public void LoadLevelAfter(float seconds){
		timer = seconds +0.5f;
	}
	public void LoadGivenLevel(string level){
		Application.LoadLevel (level);
	}

	void Update(){
		if(timer > 0f ){
			timer -= Time.deltaTime;
		}
		if(timer > 0f && timer< 0.5f){
			timer = 0f;
			Application.LoadLevel(Application.loadedLevel+1);
		}
	}
	public void OpenGameMenu( GameObject gameMenu)
	{
		if (gameMenu.gameObject.activeSelf == false) {
			gameMenu.gameObject.SetActive (true);
		} else
			gameMenu.gameObject.SetActive (false);
		
	}
}
