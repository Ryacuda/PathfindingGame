using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph<T>
{
	// Attributes
	public List<Node<T>> nodes;                    // node identifiers
	public List<List<float>> connections;       // connection matrix

	// Constructors
	private Graph(in List<Node<T>> nodes, in List<List<float>> connections)
	{
		this.nodes = nodes;
		this.connections = connections;
	}

	// Methods
	static public Graph<string> CreateTestGraph()
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

		List<Node<string>> node_list = new List<Node<string>>();
		
		for(int i = 0; i < ints.Count; i++)
		{
			node_list.Add(new Node<string>(ints[i], ints[i].ToString()));
		}

		return new Graph<string>(node_list, mat);
	}
	
	public void AddNode(List<float> newnode_connections, List<float> connections_to_newnode, T node_data)
	{
		nodes.Add(new Node<T>(nodes.Count, node_data));
		
		for(int i = 0; i < connections.Count; i++)
		{
			connections[i].Add(connections_to_newnode[i]);
		}

		connections.Add(newnode_connections);
	}
}
