using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	
	public Direction direction;
	public float xSpeed;
	private Rigidbody2D rb2d;
	private Animator anim;
	public Vector3 startPos;
	public bool isDead;

	void Start () 
	{
		isDead = false;
		rb2d = GetComponent<Rigidbody2D> ();	
		direction = gameObject.GetComponent<TriggerBehaviour>().direction;
		anim = GetComponent<Animator> ();
		startPos = transform.position;
	}
	
	void Update () 
	{
		Move ();
		IsDead ();
	}

	void Move ()
	{
		if(direction == Direction.Right)
		{
			rb2d.velocity = new Vector2 (xSpeed, rb2d.velocity.y);
		}

		if(direction == Direction.Left)
		{
			rb2d.velocity = new Vector2 (-xSpeed, rb2d.velocity.y);
		}

		if(direction == Direction.Cast)
		{
			anim.SetBool ("isCasting", true);
		}
	}
	public void TakeDamage(int i)
	{
		if(!GetComponent<TriggerBehaviour> ().shieldActive)
		{
			GetComponent<PlayerInfo> ().health -= i;
		}
	}

	void IsDead ()
	{
		if (GetComponent<PlayerInfo> ().health <= 0)
			isDead = true;
	}
}

public enum Direction {Left,Right,Cast,Stand};
