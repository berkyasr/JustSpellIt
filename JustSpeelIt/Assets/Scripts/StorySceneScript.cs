using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StorySceneScript : MonoBehaviour {
	
	void Update () 
	{
		transform.position = new Vector3 (transform.position.x , transform.position.y+1, transform.position.z);
		if (transform.position.y >= 950)
			ButtonClick ();
	
	}

	public void ButtonClick ()
	{
		SceneManager.LoadScene ("Play2");
	}
}
