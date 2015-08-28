using UnityEngine;
using System.Collections;

public abstract class Board : MonoBehaviour {

	public abstract void InitBoard();

	public abstract void OnDotSelected(Dot dot);

	public abstract void OnInputComplete();
}
