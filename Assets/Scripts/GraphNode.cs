using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphNode
{
	public List<GraphNode> neighbours;
	public float player_multiplier;
	public float animal_multiplier;
	public Vector3Int position;

	// A* attributes 

	public GraphNode()
	{
		neighbours = new List<GraphNode>();
		player_multiplier = 0;
		animal_multiplier = 0;
	}

	// Methods

	// heursitic cost is the world distance
	public float hCost(Vector3 target)
	{
		return Vector3.Dot(target - position, target - position);
	}
}
