using UnityEngine;
using System.Collections;

public class Dot : MonoBehaviour {
	public GameObject selectedIndictor;

	public int posX;
	public int posY;
	public int id;

	int selected;
	public int Selected
	{
		get
		{
			return selected;
		}
		set
		{
			selected = value;
			if(selectedIndictor != null)
				selectedIndictor.SetActive(selected > 0);
		}
	}

	void Awake()
	{
		Selected = 0;
	}

	public virtual bool IsSameDotType(Object obj)
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
		id = (posX + "," + posY).GetHashCode();
	}

	void OnMouseOver() {
		if (Input.GetMouseButton (0)) {
			DotGameManager.Instance.OnDotSelected(this);
		}
	}

}
