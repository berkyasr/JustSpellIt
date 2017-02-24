using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class LevelPick : MonoBehaviour {
	public  GameObject LevelEnteranceButton;
	private int NumberOfLevels; 
	// Use this for initialization
	void Start () {
		NumberOfLevels = PlayerPrefsManager.GetUnlockedLevel ()+1;
		for (int i = 0; i != ((PlayerPrefsManager.GetUnlockedLevel()+1)/3)+1;i++){
			for(int j = 0; j != 3;j++){
				if(NumberOfLevels != 0) {
					GameObject LevelButton = Instantiate (LevelEnteranceButton, new Vector3 (200f + j * 270f, 500f - i * 120f, 0f), Quaternion.identity) as GameObject;
					LevelButton.transform.parent = GameObject.Find ("Canvas").transform;
					LevelButton.GetComponentInChildren<Text> ().text = "Level " + (PlayerPrefsManager.GetUnlockedLevel () - NumberOfLevels+1).ToString();
					LevelButton.GetComponent<Button> ().onClick.AddListener (delegate{
						Application.LoadLevel (LevelButton.GetComponentInChildren<Text> ().text);
					});
					NumberOfLevels--;
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
