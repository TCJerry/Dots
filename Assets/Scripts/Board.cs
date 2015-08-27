﻿using UnityEngine;
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
	bool updating;

	void Awake()
	{
		dots = new Dot[columns, rows];
		logic = new SimpleDotLogic ();
		updating = true;
	}

	public void FillBoard()
	{
		for (int i = 0; i< columns; i++) 
		{
			for(int j = 0; j < rows; j++)
			{
				dots[i,j] = spawner.CreateDot();
				MoveDotTo(dots[i,j], i, j);
			}
		}

		while (!logic.HasMoves(dots)) 
		{
			ShuffleBoard();
		}

		updating = false;
	}

	void MoveDotTo(Dot dot, int x, int y, bool dropDown = false)
	{
		dot.SetPosition(x, y);

		dot.gameObject.transform.parent = transform;
		Vector2 pos = Vector2.zero;
		pos.x += (x-(columns-1)/2f)*spacingX;
		pos.y += (y-(rows-1)/2f)*spacingY;
		if (dropDown) 
		{
			dot.gameObject.transform.localPosition = new Vector3 (pos.x, pos.y + rows*spacingY, 0);
		}
		dot.TranslateTo(pos, 2f);
	}

	//todo: fix edge case where board is unsolvable
	void ShuffleBoard()
	{
		Debug.Log ("shuffling board");
		for (int i = 0; i< rows; i++) 
		{
			int x1 = Random.Range (0, columns);
			int y1 = Random.Range (0, rows);

			int x2 = Random.Range (0, columns);
			int y2 = Random.Range (0, rows);

			Dot temp = dots[x1,y1];
			dots[x1,y1] = dots[x2,y2];
			MoveDotTo(dots[x1,y1], x1, y1);
			dots[x2,y2] = temp;
			MoveDotTo(dots[x2,y2], x2, y2);
		}
	}

	void UpdateBoard()
	{
		Debug.Log ("updating board");
		for (int i = 0; i< columns; i++) 
		{
			int emptyDots = 0;
			for(int j = 0; j < rows; j++)
			{
				if(dots[i,j] == null)
				{
					emptyDots++;
				}
				else if(emptyDots > 0)
				{
					dots[i,j-emptyDots] = dots[i,j];
					dots[i,j] = null;

					Debug.LogError("moving (" + i + "," +j + ") to (" + i + "," + (j-emptyDots) + ")");

					MoveDotTo(dots[i,j-emptyDots], i, j-emptyDots);
				}
			}

			for(int j = 1; j <= emptyDots; j++)
			{
				dots[i,rows-j] = spawner.CreateDot();
				MoveDotTo(dots[i,rows-j], i, rows-j, true);
			}
		}
	}

	public void OnDotSelected(Dot dot)
	{
		if (!updating) {
			logic.OnDotSelected (dot);
		}
	}

	public void OnInputComplete()
	{
		logic.OnInputComplete ();
	}

	public void ClearDotAtPos(int x, int y)
	{
		if (x < columns && y < rows) 
		{
			if(dots[x,y] != null)
			{
				//todo add back to object pool
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

		if (dots.Count > 0) 
		{
			UpdateBoard();
		}
	}

	public void ClearAllDotsOfType(Dot dot)
	{
		bool needsUpdate = false;
		for (int i = 0; i< columns; i++) 
		{
			for(int j = 0; j < rows; j++)
			{
				if(dot.IsSameDotType(dots[i,j]))
				{
					needsUpdate = true;
					ClearDotAtPos(i,j);
				}
			}
		}

		if (needsUpdate) 
		{
			UpdateBoard();
		}
	}
}
