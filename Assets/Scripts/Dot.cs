using UnityEngine;
using System.Collections;

public class Dot : MonoBehaviour {
	public int xPos;
	public int yPos;

	public bool Equals(Object obj)
	{
		if (obj == null)
		{
			return false;
		}

		Dot p = obj as Dot;
		if ((Object)p == null)
		{
			return false;
		}

		return p.xPos == xPos && p.yPos == yPos
	}

	public void SetPosition(int x, int y)
	{
		xPos = x;
		yPos = y;
	}

	void OnMouseOver() {
		if (Input.GetMouseButton (0)) {
			DotGameManager.Instance.OnDotSelected(this);
		}
	}

}
