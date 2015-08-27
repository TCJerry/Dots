using UnityEngine;
using System.Collections;

public class Dot : MonoBehaviour {
	public int posX;
	public int posY;
	public int selected;

	void Awake()
	{
		selected = 0;
	}

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

		return p.posX == posX && p.posY == posY;
	}

	public bool TypeEquals(Object obj)
	{
		if (obj == null)
		{
			return false;
		}
		
		return obj.GetType().Equals(typeof(Dot));
	}

	public void SetPosition(int x, int y)
	{
		posX = x;
		posY = y;
	}

	void OnMouseOver() {
		if (Input.GetMouseButton (0)) {
			DotGameManager.Instance.OnDotSelected(this);
		}
	}

}
