using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphNode
{
	public List<GraphNode> neighbours;
	public float speed_multiplier;
	public Vector3Int position;

	public GraphNode()
	{
		neighbours = new List<GraphNode>();
		speed_multiplier = 0;
	}
}
