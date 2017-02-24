using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {
	public int AnimationType;
	public float distance;
	private float speed = 20f;
	private bool checkRotation = false;
	private Vector3 temp;
	// Use this for initialization
	void Start () {
		temp = transform.position;
	}
	void Update(){
		if(AnimationType == 1 ){//Vertical
			if(transform.position.y < temp.y+distance && checkRotation == false ){
				transform.position += Vector3.up * speed * Time.deltaTime;
			}else{
				checkRotation = true;
				transform.position += Vector3.down * speed * Time.deltaTime;
				if (transform.position.y <= temp.y)
					checkRotation = false;
			}

		}else if(AnimationType == 2){
			if(transform.position.x >temp.x-distance && checkRotation == false ){
				transform.position += Vector3.left * speed * Time.deltaTime;
			}else{
				checkRotation = true;
				transform.position += Vector3.right * speed * Time.deltaTime;
				if (transform.position.x >= temp.x)
					checkRotation = false;
			}
		}
	}
}
