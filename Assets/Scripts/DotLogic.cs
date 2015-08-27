using UnityEngine;
using System.Collections;

public abstract class DotLogic {
	public abstract bool HasMoves(Dot[,] board);

	public abstract void OnDotSelected(Dot dot);

	public abstract void OnInputComplete();
}
