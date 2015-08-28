using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid2DDotLogic : DotLogic {
	List<string> selectedEdges;
	string lastCheckedId;

	public Grid2DDotLogic()
	{
		selectedDots = new List<Dot> ();
		selectedEdges = new List<string>();
	}

	public override Solvability CheckSolvability(Dot[,] board)
	{
		int columns = board.GetLength (0);
		int rows = board.GetLength (1);
		HashSet<string> dotTypeSet = new HashSet<string>();

		for (int i = 0; i<columns; i++) 
		{
			for(int j = 0; j < rows; j++)
			{
				Dot dot = board[i,j];
				dotTypeSet.Add(dot.GetDotType());

				if(i+1 < columns && board[i+1,j].GetDotType() == dot.GetDotType())
				{
					return Solvability.HAS_MOVES;
				}

				if(j+1 < rows && board[i,j+1].GetDotType() == dot.GetDotType())
				{
					return Solvability.HAS_MOVES;
				}
			}
		}

		if (columns * rows <= dotTypeSet.Count) 
		{
			return Solvability.UNSOLVABLE;
		} 
		else 
		{
			return Solvability.NO_MOVES;
		}
	}

	public override void OnDotSelected(Dot dot)
	{
		if (!string.IsNullOrEmpty(lastCheckedId) && lastCheckedId == dot.Index.ToString()) 
		{
			return;
		}

		lastCheckedId = dot.Index.ToString();

		if (selectedDots.Count == 0) {
			AddDot (dot);
		} 
		else if(selectedDots[0].GetDotType() == dot.GetDotType() && isNeighbor (dot))
		{
			Dot lastDot = selectedDots[selectedDots.Count-1];
			string edge = dot.Index < lastDot.Index ? dot.Index + ":" + lastDot.Index : lastDot.Index + ":" + dot.Index;
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
		Debug.Log ("Undo");
		Dot lastDot = selectedDots[selectedDots.Count-1];
		lastDot.Selected--;
		selectedDots.RemoveAt(selectedDots.Count-1);
		selectedEdges.RemoveAt (selectedEdges.Count-1);
	}

	bool isNeighbor(Dot dot)
	{
		if (selectedDots.Count > 0) {
			Dot prevDot = selectedDots [selectedDots.Count-1];
			int diffX = dot.PosX - prevDot.PosX;
			int diffY = dot.PosY - prevDot.PosY;
			
			if(diffX*diffX + diffY*diffY == 1)
			{
				return true;
			}
		}
		
		return false;
	}

	public override bool HasMadeSquare()
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
		if(selectedDots.Count == 1) 
		{
			selectedDots[0].Selected = 0;
		}

		lastCheckedId = "";
		selectedDots.Clear ();
		selectedEdges.Clear ();
	}
}
