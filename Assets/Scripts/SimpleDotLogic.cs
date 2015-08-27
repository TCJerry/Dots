using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleDotLogic : DotLogic {
	List<Dot> selectedDots;
	List<string> selectedEdges;
	string lastCheckedId;

	public SimpleDotLogic()
	{
		selectedDots = new List<Dot> ();
		selectedEdges = new List<string>();
	}

	public override bool HasMoves(Dot[,] board)
	{
		int columns = board.GetLength (0);
		int rows = board.GetLength (1);

		Debug.LogError ("has moves " + rows + " " + columns);

		for (int i = 0; i<columns; i++) 
		{
			for(int j = 0; j < rows; j++)
			{
				Dot dot = board[i,j];

				if(i+1 < columns && board[i+1,j].IsSameDotType(dot))
				{
					return true;
				}

				if(j+1 < rows && board[i,j+1].IsSameDotType(dot))
				{
					return true;
				}
			}
		}

		return false;
	}

	public override void OnDotSelected(Dot dot)
	{
		if (!string.IsNullOrEmpty(lastCheckedId) && lastCheckedId == dot.id.ToString()) 
		{
			return;
		}

		lastCheckedId = dot.id.ToString();

		if (selectedDots.Count == 0) {
			AddDot (dot);
		} 
		else if(selectedDots[0].IsSameDotType(dot) && isNeighbor (dot))
		{
			Dot lastDot = selectedDots[selectedDots.Count-1];
			string edge = dot.id < lastDot.id ? dot.id + ":" + lastDot.id : lastDot.id + ":" + dot.id;
			int edgeIndex = selectedEdges.IndexOf(edge);

			if(edgeIndex >= 0)
			{
				if(edgeIndex == selectedEdges.Count-1)
				{
					UndoLastMove();
				}
				return;
			}
				
			AddDot (dot, edge);
		}
	}

	void AddDot(Dot dot)
	{
		Debug.LogError ("AddDot");
		selectedDots.Add (dot);
		dot.Selected++;
	}

	void AddDot(Dot dot, string edge)
	{
		AddDot (dot);
		selectedEdges.Add (edge);
	}

	void UndoLastMove()
	{
		Debug.LogError ("UNDO");
		Dot lastDot = selectedDots[selectedDots.Count-1];
		lastDot.Selected--;
		selectedDots.RemoveAt(selectedDots.Count-1);
		selectedEdges.RemoveAt (selectedEdges.Count-1);
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

	bool hasMadeSquare()
	{
		for(int i = 0; i < selectedDots.Count; i++)
		{
			if(selectedDots[i].Selected > 1)
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
			selectedDots[0].Selected = 0;
		}

		lastCheckedId = "";
		selectedDots.Clear ();
		selectedEdges.Clear ();
	}
}
