using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Board : MonoBehaviour {
	public int rows;
	public int columns;
	public DotSpawner spawner;

	Dot[,] dots;

	public void FillBoard()
	{
		dots = new Dot[rows, columns];
		for (int i = 0; i< rows; i++) 
		{
			for(int j = 0; j < columns; j++)
			{
				dots[i,j] = spawner.CreateDot();
				dots[i,j].SetPosition(i,j);
				dots[i,j].gameObject.transform.parent = transform;

			}
		}
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
			ClearDotAtPos (dots[i].xPos, dots[i].yPos);
		}
	}
}
