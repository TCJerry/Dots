using UnityEngine;
using System.Collections;

[RequireComponent (typeof (SpriteRenderer))]
public class ColoredDot : Dot {

	public override bool IsSameDotType(Object obj)
	{
		if (obj == null)
		{
			return false;
		}

		ColoredDot dot = obj as ColoredDot;
		if ((Object)dot == null)
		{
			return false;
		}

		return dot.GetComponent<SpriteRenderer>().color == GetComponent<SpriteRenderer>().color;
	}
}
