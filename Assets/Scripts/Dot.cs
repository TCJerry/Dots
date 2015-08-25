using UnityEngine;
using System.Collections;

public class Dot : MonoBehaviour {
	int xPos;
	int yPos;

	public bool Equals(Object obj)
	{
		if (obj == null)
		{
			return false;
		}

		return obj.GetType().Equals (typeof(Dot));
	}

	public void SetPosition(int x, int y)
	{
		xPos = x;
		yPos = y;
	}

	void OnMouseOver() {
		if (Input.GetMouseButton (0)) {
			Debug.LogError ("test");
		}
	}

}
