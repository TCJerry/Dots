using UnityEngine;
using System.Collections;

public class ColoredDot : Dot {

	public override bool IsSameDotType(Object obj)
	{
		if (obj == null)
		{
			return false;
		}
		
		return obj.GetType().Equals(typeof(Dot));
	}
}
