using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum EdgeStatus
{
	NONE,
	UNUSED,
	USED
}

public class SimpleDotLogic : DotLogic {
	List<Dot> selectedDots;
	EdgeStatus[,] adjMatrix;
	int rows;
	int columns;
	Dot lastChecked;

	public SimpleDotLogic(int rows, int columns)
	{
		selectedDots = new List<Dot> ();
		adjMatrix = new EdgeStatus[rows,columns];
		this.rows = rows;
		this.columns = columns;
	}

	public void OnBoardUpdated(Dot[,] board)
	{
		for (int i = 0; i<rows; i++) 
		{
			for(int j = 0; j < columns; j++)
			{
				Dot dot = board[i,j];

				if(i-1 >= 0 && board[i-1,j].TypeEquals(dot))
				{

				}

				if(i+1 < rows && board[i+1,j].TypeEquals(dot))
				{
					
				}

				if(j-1 >= 0 && board[i,j-1].TypeEquals(dot))
				{
					
				}

				if(j+1 < columns && board[i,j+1].TypeEquals(dot))
				{
					
				}
			}
		}
	}

	public override void OnDotSelected(Dot dot)
	{
		if (lastChecked != null && lastChecked.Equals (dot)) 
		{
			return;
		}
		Debug.LogError ("dot selected " + dot.posX + " " + dot.posY + " " + dot.selected);
		lastChecked = dot;

		if (selectedDots.Count == 0) {
			AddDot (dot);
		} 
		else if(selectedDots[0].TypeEquals(dot) && isNeighbor (dot))
		{
			if(dot.selected > 0 && selectedDots.Count > 1 && selectedDots[selectedDots.Count-2].Equals(dot))
			{
				//undo
				Dot lastDot = selectedDots[selectedDots.Count-1];
				lastDot.selected--;
				selectedDots.RemoveAt(selectedDots.Count-1);
			}
			else
			{
				AddDot (dot);
			}
		}
	}

	bool isNeighbor(Dot dot)
	{
		if (selectedDots.Count > 0) {
			Dot prevDot = selectedDots [selectedDots.Count-1];
			int diffX = dot.posX - prevDot.posX;
			int diffY = dot.posY - prevDot.posY;

			if(diffX*diffX + diffY*diffY == 1)
			{
				return true;
			}
		}

		return false;
	}

	void AddDot(Dot dot)
	{
		selectedDots.Add (dot);
		dot.selected++;
	}

	bool hasMadeSquare()
	{
		for(int i = 0; i < selectedDots.Count; i++)
		{
			if(selectedDots[i].selected > 1)
			{
				return true;
			}
		}

		return false;
	}

	public override void OnInputComplete()
	{
		if (selectedDots.Count > 1) {
			if (hasMadeSquare ())
			{
				DotGameManager.Instance.board.ClearAllDotsOfType (selectedDots[0]);
			}
			else
			{
				DotGameManager.Instance.board.ClearDots (selectedDots);
			}
		} 
		else if(selectedDots.Count == 1) 
		{
			selectedDots[0].selected = 0;
		}

		selectedDots.Clear ();
	}
}
