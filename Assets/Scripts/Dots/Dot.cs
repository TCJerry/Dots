using UnityEngine;
using System.Collections;

public class Dot : MonoBehaviour {
	public GameObject selectedIndictor;

	int posX;
	public int PosX
	{
		get
		{
			return posX;
		}
	}

	int posY;
	public int PosY
	{
		get
		{
			return posY;
		}
	}

	int index;
	public int Index
	{
		get
		{
			return index;
		}
	}

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

	Vector3 targetPosition;
	Vector3 changeStep;

	void Awake()
	{
		Selected = 0;
		targetPosition = transform.localPosition;
	}

	public virtual string GetDotType()
	{	
		return GetType().ToString();
	}

	public void SetPosition(int x, int y)
	{
		posX = x;
		posY = y;
		index = (posX + "," + posY).GetHashCode();
	}

	public void TranslateTo(Vector3 position, float speed)
	{
		if (transform.position != position) 
		{
			if (speed <= 0) 
			{
				transform.localPosition = position;
			} 
			else 
			{
				targetPosition = position;
				changeStep = (position - transform.localPosition).normalized * speed * Time.fixedDeltaTime;
			}
		}
	}

	void OnMouseOver() {
		if (Input.GetMouseButton (0)) {
			DotGameManager.Instance.OnDotSelected(this);
		}
	}

	void FixedUpdate()
	{
		if (targetPosition != transform.localPosition) 
		{
			if((targetPosition-transform.localPosition).magnitude < changeStep.magnitude)
			{
				transform.localPosition = targetPosition;
			}
			else
			{
				transform.localPosition += changeStep;
			}
		}
	}
}
