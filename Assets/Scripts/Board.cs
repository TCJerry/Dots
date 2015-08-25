using UnityEngine;
using System.Collections;

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
			}
		}
	}


}
