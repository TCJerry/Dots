using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleDotLogic : DotLogic {
	List<Dot> selectedDots;

	public SimpleDotLogic()
	{
		selectedDots = new List<Dot> ();
	}

	public override void OnDotSelected(Dot dot)
	{
		if (selectedDots.Count == 0) 
		{
			selectedDots.Add (dot);
		}


	}

	public override void OnInputRelease()
	{
		if (selectedDots.Count > 1) 
		{
			DotGameManager.Instance.board.ClearDots(selectedDots);
		}

		selectedDots.Clear ();
	}
}
