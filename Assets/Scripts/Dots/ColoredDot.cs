using UnityEngine;
using System.Collections;

[RequireComponent (typeof (SpriteRenderer))]
public class ColoredDot : Dot {

	public override string GetDotType()
	{	
		return GetType().ToString() + GetComponent<SpriteRenderer>().color.ToString();
	}
}
