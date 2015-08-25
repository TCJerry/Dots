using UnityEngine;
using System.Collections;

[System.Serializable]
public class WeightedDot
{
	public Dot prefab;
	[Range(0,1)]
	public float weight;
}

public class WeightedDotSpawner : DotSpawner 
{
	[SerializeField]
	WeightedDot[] weightedDots;
	float totalweight = 0;

	void Awake()
	{
		for (int i = 0; i<weightedDots.Length; i++) 
		{
			totalweight += weightedDots[i].weight;
		}
	}

	public override Dot CreateDot()
	{
		float r = Random.Range (0, totalweight);
		float currentWeight = 0;

		for (int i = 0; i<weightedDots.Length; i++) 
		{
			currentWeight += weightedDots[i].weight;

			if(currentWeight >= r)
			{
				//todo: use object pool
				Dot dot = Instantiate(weightedDots[i].prefab.gameObject).GetComponent<Dot>();
				return dot;
			}
		}

		return null;
	}
}
