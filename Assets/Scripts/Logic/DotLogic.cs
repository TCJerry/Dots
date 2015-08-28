using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Solvability
{
	HAS_MOVES,
	NO_MOVES,
	UNSOLVABLE
}

public abstract class DotLogic {
	protected List<Dot> selectedDots;
	public List<Dot> SelectedDots
	{
		get
		{
			return selectedDots;
		}
	}

	public abstract Solvability CheckSolvability(Dot[,] board);

	public abstract bool HasMadeSquare();

	public abstract void OnDotSelected(Dot dot);

	public abstract void OnInputComplete();
}
