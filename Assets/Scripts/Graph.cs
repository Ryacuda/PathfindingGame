using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph
{
	// Attributes
	public List<int> nodes;                    // node identifiers
	public List<List<float>> connections;       // connection matrix

	// Constructors
	private Graph(in List<int> nodes, in List<List<float>> connections)
	{
		this.nodes = nodes;
		this.connections = connections;
	}

	// Methods
	static public Graph CreateTestGraph()
	{
		List<int> ints = new List<int> {0,1,3,4};
		float inf = float.PositiveInfinity;

		List<List<float>> mat = new List<List<float>>
		{
			new List<float>{inf, 10, 10, inf},
			new List<float>{inf, inf, 10, inf},
			new List<float>{inf, inf, inf, 10},
			new List<float>{inf, inf, inf,inf}
		};

		return new Graph(ints, mat);
	}
	
	public void AddNode(List<float> newnode_connections, List<float> connections_to_newnode)
	{
		nodes.Add(nodes.Count);
		
		for(int i = 0; i < connections.Count; i++)
		{
			for(int j = 0; j < connections_to_newnode.Count; j++)
			{
				connections[i].Add(connections_to_newnode[i]);
			}
		}

		connections.Add(newnode_connections);
	}
}
