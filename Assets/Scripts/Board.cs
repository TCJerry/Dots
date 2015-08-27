using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Board : MonoBehaviour {
	[SerializeField, Range(1,100)]
	int rows;
	[SerializeField, Range(1,100)]
	int columns;

	public float spacingX=1f;
	public float spacingY=1f;
	public DotSpawner spawner;

	DotLogic logic;
	Dot[,] dots;

	void Awake()
	{
		dots = new Dot[rows, columns];
		logic = new SimpleDotLogic (rows, columns);
	}

	public void FillBoard()
	{
		for (int i = 0; i< rows; i++) 
		{
			for(int j = 0; j < columns; j++)
			{
				dots[i,j] = spawner.CreateDot();
				dots[i,j].SetPosition(i,j);

				dots[i,j].gameObject.transform.parent = transform;
				Vector3 pos = dots[i,j].gameObject.transform.localPosition;
				pos.x += (i-rows/2f)*spacingX;
				pos.y += (j-columns/2f)*spacingY;
				dots[i,j].gameObject.transform.localPosition = pos;
			}
		}
	}

	public void OnDotSelected(Dot dot)
	{
		logic.OnDotSelected (dot);
	}

	public void ClearDotAtPos(int x, int y)
	{
		if (x < rows && y < columns) 
		{
			if(dots[x,y] != null)
			{
				//remove
				Destroy(dots[x,y].gameObject);
				dots[x,y] = null;
			}
		}
	}

	public void ClearDots(List<Dot> dots)
	{
		for (int i = 0; i < dots.Count; i++) 
		{
			ClearDotAtPos ((int)dots[i].posX, (int)dots[i].posY);
		}
	}

	public void ClearAllDotsOfType(Dot dot)
	{
		for (int i = 0; i< rows; i++) 
		{
			for(int j = 0; j < columns; j++)
			{
				if(dot.TypeEquals(dots[i,j]))
				{
					ClearDotAtPos(i,j);
				}
			}
		}
	}
}
