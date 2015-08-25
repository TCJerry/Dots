using UnityEngine;
using System.Collections;

public class DotInfo
{
	public Dot dot;

}

public class DotGameManager : MonoBehaviour {
	Board board;

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


}
