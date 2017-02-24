using UnityEngine;
using System.Collections;

public static class Info {

	public static int ObjectCost (GameObject other)
	{
		if (other.CompareTag ("Jumper"))
			return 10;
		if (other.CompareTag ("Platform"))
			return 10;
		if (other.CompareTag ("GravityUp"))
			return 5;
		if (other.CompareTag ("GravityDown"))
			return 5;
		if (other.CompareTag ("TurnLeft"))
			return 5;
		if (other.CompareTag ("TurnRight"))
			return 10;
		if (other.CompareTag ("Teleport"))
			return 10;
		return 0;
	}


}
