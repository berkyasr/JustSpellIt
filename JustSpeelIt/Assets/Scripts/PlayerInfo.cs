using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PlayerInfo : MonoBehaviour {

	public int health;
	public int mana;
	public List<GameObject> itemList;


	void AddItem (GameObject item)
	{
		itemList.Add (item);
	}

	void DeleteItem (GameObject item)
	{
		itemList.Remove(item);
	}
}
