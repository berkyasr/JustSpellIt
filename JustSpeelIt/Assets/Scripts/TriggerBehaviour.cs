using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TriggerBehaviour : MonoBehaviour {
	public bool isTutorial;
	public bool isTrap;
	public float jumpSpeed;
	public float xSpeed;
	public bool isRestart = false;
	public bool isReverse;
	private Rigidbody2D rb2d;
	public Direction direction;
	private Animator anim;
	public bool shieldActive = false;

	void Start () 
	{
		isTrap = false;
		rb2d = GetComponent<Rigidbody2D> ();	
		anim = GetComponent<Animator> ();
		if (!isTutorial) {
			GameObject.Find("InfoPanel").SetActive(false);
			Destroy (GameObject.Find ("InfoPanel"));
			Destroy (GameObject.Find ("Info"));
			Destroy (GameObject.Find ("ButtonInfoStart"));
			Destroy (GameObject.Find ("ButtonInfoUndo"));
			Destroy (GameObject.Find ("ButtonInfoRestart"));
		}
		GameObject.Find("Info").GetComponent<Text>().text = "Welcome to Tutorial Level. You can choose items form skill bar and put it with mouse.\n" +
			"To see how items work please press Start Button!!";
	}
	void OnCollisionEnter2D (Collision2D other)
	{
		if (other.gameObject.tag == "Platform") {
			if (isTutorial) {
				GameObject.Find ("Info").GetComponent<Text> ().text = "This is a platform\n You can put these to create new route ";
			}
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if(other.CompareTag("Teleport"))
		{
			if (isTutorial) {
				GameObject.Find ("Info").GetComponent<Text> ().text = "This is a platform\n You can put these to create new route ";
			}
			isTutorial = false;
//			GameObject.Find("InfoPanel").SetActive(false);
			Destroy (GameObject.Find ("InfoPanel"));
			Destroy (GameObject.Find ("Info"));
			Destroy (GameObject.Find ("ButtonInfoStart"));
			Destroy (GameObject.Find ("ButtonInfoUndo"));
			Destroy (GameObject.Find ("ButtonInfoRestart"));
			GetComponent<PlayerMovement> ().startPos = other.gameObject.transform.FindChild("Dest").transform.position;
			GetComponent<PlayerMovement> ().direction = Direction.Stand;
			rb2d.velocity = new Vector2 (0, 0);
			anim.SetBool ("isMoving", false);
			anim.SetBool ("isCasting", false);
			anim.SetTrigger ("isFading");
			rb2d.isKinematic = true;
			GetComponent<PlayerInfo> ().mana = 100;
			StartCoroutine (MoveCamera ());
			StartCoroutine (MovePlayer (other));
			StartCoroutine (ResetPlayer ());
			GameObject.Find ("Canvas/UI/Skill Bar").transform.GetChild (0).gameObject.SetActive (true);
			DestroyObject (other.gameObject);

		}
		/*if(other.CompareTag("Trap"))
		{
			other.gameObject.GetComponent<TrapTrigger> ().isActive = true;
		}*/

		if(other.CompareTag("TurnRight"))
		{
			if (isTutorial) {
				GameObject.Find("Info").GetComponent<Text>().text = "This is an item which makes wizard to turn right";

			}
			GetComponent<PlayerMovement> ().direction = Direction.Right;
			if (!isReverse)
				transform.rotation = Quaternion.Euler (0, 0, 0);
			else
				transform.rotation = Quaternion.Euler (0, 0, 180);
			other.gameObject.SetActive (false);
			rb2d.velocity = new Vector2 (xSpeed, rb2d.velocity.y);

		}

		if(other.CompareTag("TurnLeft"))
		{

			if (isTutorial) {
				GameObject.Find("Info").GetComponent<Text>().text = "This is an item which makes wizard to turn left";

			}
			GetComponent<PlayerMovement> ().direction = Direction.Left;
			if (!isReverse)
				transform.rotation = Quaternion.Euler (0, 180, 0);
			else
				transform.rotation = Quaternion.Euler (0, 180, 180);

			rb2d.velocity = new Vector2 (-xSpeed, rb2d.velocity.y);
			other.gameObject.SetActive (false);
		}

		if(other.CompareTag("Jumper"))
		{
			if (isTutorial) {
				GameObject.Find("Info").GetComponent<Text>().text = "This is an item which makes wizard to jump";

			}
			rb2d.velocity = new Vector2 (rb2d.velocity.x, jumpSpeed);
			anim.SetBool ("isJumping", true);
			other.gameObject.SetActive (false);
		}

		if(other.CompareTag("GravityUp"))
		{
			if (isTutorial) {
				GameObject.Find("Info").GetComponent<Text>().text = "This is an item which changes gravity direction down to up";

			}
			if(!isReverse)
			{
				rb2d.gravityScale = -1;
				transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);

				StartCoroutine (Flip (1));
				isReverse = true;
			}
			other.gameObject.SetActive (false);

		}
		if(other.CompareTag("ArtifactSpeed"))
		{
			StartCoroutine (ArtifactSpeed ());
			DestroyObject (other.gameObject);
		}

		if(other.CompareTag("ArtifactShield"))
		{
			StartCoroutine (ShieldActivator ());
			DestroyObject (other.gameObject);
		}

		if(other.CompareTag("GravityDown"))
		{
			if (isTutorial) {
				GameObject.Find("Info").GetComponent<Text>().text = "This is an item which changes gravity direction up to down";

			}
			if(isReverse)
			{
				rb2d.gravityScale = 1;
				transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
				StartCoroutine (Flip (-1));

				isReverse = false;				
			}
			other.gameObject.SetActive (false);

		}
	}

	IEnumerator Flip (int x)
	{
		float y = 180;
		if (x == 1) 
		{
			for (int i = 1; i < 26; i++)
			{
				if (!isRestart) {
					yield return new WaitForSeconds (0.0005f);
					transform.rotation = Quaternion.Euler (0, transform.rotation.y, 7.2f * i);
				}
			}
		}

		if (x == -1) 
		{
			for (int i = 1; i < 26; i++)
			{
				if (!isRestart) {
					y -= 7.2f;
					yield return new WaitForSeconds (0.0005f);
					transform.rotation = Quaternion.Euler (0, transform.rotation.y, y);
				}
			}
		}
		if (isRestart)
			transform.rotation = new Quaternion (0f, 0f, 0f, 0f);
		isRestart = false;
	}
	IEnumerator ArtifactSpeed ()
	{
		xSpeed *= 2;
		GetComponent<PlayerMovement> ().xSpeed *= 2;
		yield return new WaitForSeconds (3);
		GetComponent<PlayerMovement> ().xSpeed /= 2;
		xSpeed /= 2;
	}

	IEnumerator ShieldActivator ()
	{
		shieldActive = true;
		yield return new WaitForSeconds (3);
		shieldActive = false;
	}

	IEnumerator MoveCamera ()
	{
		float i = 0.2f;
		float x = Camera.main.gameObject.transform.position.x + 10f;
		yield return new WaitForSeconds (1.5f);
		while(Camera.main.gameObject.transform.position.x <= x)
		{
			yield return new WaitForSeconds (0.000005f);
			Camera.main.gameObject.transform.position = new Vector3 (Camera.main.gameObject.transform.position.x+i,Camera.main.gameObject.transform.position.y,Camera.main.gameObject.transform.position.z);
		}
	}

	IEnumerator MovePlayer (Collider2D other)
	{
		yield return new WaitForSeconds (1.5f);
		transform.position = GetComponent<PlayerMovement> ().startPos;
		GetComponent<PlayerMovement> ().direction = Direction.Cast;	
	}

	IEnumerator ResetPlayer ()
	{
		yield return new WaitForSeconds (5);
		GameObject.Find ("Canvas/UI/Skill Bar/Panel").GetComponent<UI> ().ReloadLevel ();
		rb2d.isKinematic = false;
	}
}
