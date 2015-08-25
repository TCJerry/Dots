using UnityEngine;
using System.Collections;

public class DotGameManager : MonoBehaviour {
	public Board board;
	public DotLogic logic;

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
		logic.OnDotSelected (dot);

	}
}
