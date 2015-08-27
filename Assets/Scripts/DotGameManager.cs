using UnityEngine;
using System.Collections;

public class DotGameManager : MonoBehaviour {
	public Board board;

	private static DotGameManager instance;
	public static DotGameManager Instance
	{
		get
		{
			return instance;
		}
	}

	void Awake()
	{
		if(instance == null)
			instance = this;

	}

	void Start()
	{
		board.FillBoard();
	}

	public void OnDotSelected(Dot dot)
	{
		board.OnDotSelected (dot);
	}

	void Update() {
		if (Input.GetMouseButtonUp (0)) 
		{
			board.OnInputComplete ();
		}
	}
}
