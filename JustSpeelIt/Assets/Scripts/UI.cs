using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class UI : MonoBehaviour {
	public GameObject panel;
	public GameObject Player;
	[SerializeField]private List<GameObject> UsedItems;
	private Texture2D followSprite;
	private bool isCursor = false,CanPut = true;
	private bool draw = false,toolTipMouse = false;
	public GameObject ToolTipPanel;

	private GameObject parent;
	[SerializeField] public List<GameObject> trapList;

	void Start () {
		Player = GameObject.Find ("Player");
		followSprite = null;

		//parent = GameObject.Find ("Traps");

		/*foreach (Transform child in parent.transform)
		{
			trapList.Add (child.gameObject);
		}*/
	}
	
	// Update is called once per frame
	void Update () {
		if(gameObject.tag == "StatBar"){
			transform.FindChild ("Health").GetComponent<Text> ().text = "Health\n" + Player.GetComponent<PlayerInfo> ().health.ToString();
			transform.FindChild ("Mana").GetComponent<Text> ().text = "Mana\n" + Player.GetComponent<PlayerInfo> ().mana.ToString();
		}
		if(gameObject.tag == "SkillBar"){
			int i = 0;
			foreach(Transform child in gameObject.transform){
				child.transform.FindChild ("Image").GetComponent<Image> ().sprite = Player.GetComponent<PlayerInfo> ().itemList [i].GetComponent<SpriteRenderer> ().sprite;
				i++;
			}
		}
		if(isCursor){
			//Cursor.SetCursor(followSprite, Vector2.zero, CursorMode.Auto);
			draw = true;
			if(Input.GetMouseButtonDown(1)){
				//Cursor.SetCursor (null, Vector2.zero, CursorMode.Auto);
				draw = false;
				isCursor = false;
				CanPut = true;
			}if(Input.GetMouseButtonDown(0)&& Player.GetComponent<PlayerInfo>().mana > 0 ){
				for(int i = 0;i!= Player.GetComponent<PlayerInfo>().itemList.Count;i++){
					if(Player.GetComponent<PlayerInfo>().itemList[i].GetComponent<SpriteRenderer>().sprite.texture == followSprite){
						Vector3 spawnPosition = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 1f));
						GameObject spawnedItem = Instantiate (Player.GetComponent<PlayerInfo> ().itemList [i], spawnPosition, Quaternion.identity) as GameObject;
						UsedItems.Add (spawnedItem);
						Player.GetComponent<PlayerInfo>().mana -=Info.ObjectCost (spawnedItem);
						CanPut = true;
						isCursor = false;
						break;
					}
				}
			}
		}else {
			//Cursor.SetCursor (null, Vector2.zero, CursorMode.Auto);
			draw = false;
		}
		if(CanPut){
			if(Input.GetKeyDown(KeyCode.Alpha1)){
				ItemUseFromButton (1);
			}
			if(Input.GetKeyDown(KeyCode.Alpha2)){
				ItemUseFromButton (2);
			}
			if(Input.GetKeyDown(KeyCode.Alpha3)){
				ItemUseFromButton (3);
			}
			if(Input.GetKeyDown(KeyCode.Alpha4)){
				ItemUseFromButton (4);
			}
			if(Input.GetKeyDown(KeyCode.Alpha5)){
				ItemUseFromButton (5);
			}
			if(Input.GetKeyDown(KeyCode.Alpha6)){
				ItemUseFromButton (6);
			}

		}

		if(toolTipMouse){
			ToolTipPanel.transform.position = new Vector3 (Input.mousePosition.x -120f, Input.mousePosition.y + 150f, 0f);
		}
			
	}
	public void ItemUseFromButton(int buttonNumber){
		if(gameObject.tag == "SkillBar") {
			GameObject currentButton = gameObject.transform.FindChild (buttonNumber.ToString ()).gameObject;
			followSprite = currentButton.transform.FindChild ("Image").GetComponent<Image> ().sprite.texture;
			isCursor = true;
		}
	}
	public void removeItem()
	{
		GameObject temp;
		int i = 1;
		if (UsedItems.Count > 0) {
		
			foreach (GameObject test in UsedItems) {
				if (i == UsedItems.Count) {
					temp = test;
					Player.GetComponent<PlayerInfo>().mana +=Info.ObjectCost (temp);
					UsedItems.RemoveAt (UsedItems.Count - 1);
					Destroy(temp);
					break;
				}
				i++;
			}
		}
	}
	void OnGUI ()
	{
		if(draw)
		{
			Cursor.visible = false;

			float mouseX=Mathf.Clamp(Event.current.mousePosition.x,0f,(Screen.width*15)/16);
			float mouseY=Mathf.Clamp(Event.current.mousePosition.y,0f,(Screen.height*5)/6);
			GUI.DrawTexture (new Rect (mouseX-followSprite.width/2, mouseY-followSprite.height/2, followSprite.width, followSprite.height), followSprite);
		}

		else
		{
			Cursor.visible = true;

		}
	}

	public void PlayButton ()
	{
		panel.SetActive (false);
		Player.GetComponent<PlayerMovement> ().direction = Direction.Right;
		Player.GetComponent<Animator> ().SetBool ("isCasting", false);
		Player.GetComponent<Animator> ().SetBool ("isMoving", true);
		Player.GetComponent<TriggerBehaviour> ().isRestart = false;

	}
	public void ReloadLevel ()
	{
		while (UsedItems.Count > 0)
			foreach (GameObject test in UsedItems) {
				UsedItems.Remove (test);
				Destroy (test);
				break;
			}
		UsedItems = new List<GameObject> ();
		Player.GetComponent<TriggerBehaviour> ().isRestart = true;
		Player.GetComponent<Animator> ().SetBool ("isMoving", false);
		Player.GetComponent<Animator> ().SetBool ("isCasting", true);
		Player.GetComponent<Rigidbody2D> ().velocity = new Vector3(0f,0f,0f);
		Player.GetComponent<Rigidbody2D> ().gravityScale = 1;
		Player.transform.localScale = new Vector2 (1, 1);
		Player.transform.position = Player.GetComponent<PlayerMovement> ().startPos;
		Player.GetComponent<PlayerMovement> ().direction = Direction.Cast;
		Player.transform.rotation = new Quaternion (0f, 0f, 0f,0f);
		Player.GetComponent<TriggerBehaviour> ().isReverse = false;
		Player.GetComponent<PlayerInfo> ().mana = 100;
		panel.SetActive (true);

		/*foreach (GameObject trap in trapList)
		{
			if(trap.GetComponent<TrapTrigger> ().isMoved)
			trap.GetComponent<TrapTrigger> ().isReset = true;
		}*/
	}
	public void ToolTipOpen(int number){
		if(gameObject.tag=="SkillBar") {
			ToolTipPanel.SetActive (true);
			if (number == 1) {
				ToolTipPanel.GetComponentInChildren<Text> ().text = "That magical object reverses the gravity.\nMana Cost : " + Info.ObjectCost (Player.GetComponent<PlayerInfo> ().itemList [0]).ToString ();
			} else if (number == 2) {
				ToolTipPanel.GetComponentInChildren<Text> ().text = "That magical object normalizes the gravity.\nMana Cost : " + Info.ObjectCost (Player.GetComponent<PlayerInfo> ().itemList [1]).ToString ();
			} else if (number == 3) {
				ToolTipPanel.GetComponentInChildren<Text> ().text = "That magical object makes you jump.\nMana Cost : " + Info.ObjectCost (Player.GetComponent<PlayerInfo> ().itemList [2]).ToString ();
			} else if (number == 4) {
				ToolTipPanel.GetComponentInChildren<Text> ().text = "That magical object enables you to change your direction toleft direction.\nMana Cost : " + Info.ObjectCost (Player.GetComponent<PlayerInfo> ().itemList [3]).ToString ();
			} else if (number == 5) {
				ToolTipPanel.GetComponentInChildren<Text> ().text = "That magical object enables you to change your direction right direction.\nMana Cost : " + Info.ObjectCost (Player.GetComponent<PlayerInfo> ().itemList [4]).ToString ();
			} else if (number == 6) {
				ToolTipPanel.GetComponentInChildren<Text> ().text = "That magical object enables you to place a platform.\nMana Cost : " + Info.ObjectCost (Player.GetComponent<PlayerInfo> ().itemList [5]).ToString ();
			}
			toolTipMouse = true;
		}
	}
	public void ToolTipClose(){
		if(gameObject.tag=="SkillBar") {
			ToolTipPanel.SetActive (false);
			toolTipMouse = false;
		}
	}
}