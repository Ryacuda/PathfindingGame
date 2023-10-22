using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;


// directed graph
public class Graph<T>
{
	// Attributes
	public List<GraphNode<T>> nodes;                    // node identifiers

	// Constructors
	private Graph()
	{
		nodes = new List<GraphNode<T>>();
	}

	// Methods
	static public Graph<string> CreateTestGraph2()
	{
		Graph<string> g = new Graph<string>();

		// add the nodes
		g.AddNode(0, "A");
		g.AddNode(1, "B");
		g.AddNode(2, "C");
		g.AddNode(3, "D");
		g.AddNode(4, "E");
		g.AddNode(5, "F");
		g.AddNode(6, "G");
		g.AddNode(7, "H");
		g.AddNode(8, "I");
		g.AddNode(9, "J");

		// add the edges
		g.AddDirectedEdge(0, 1, 85);        // A -> B
        g.AddDirectedEdge(0, 2, 217);		// A -> C
		g.AddDirectedEdge(0, 4, 173);		// A -> E

		g.AddDirectedEdge(1, 5, 80);        // B -> F

        g.AddDirectedEdge(2, 6, 186);		// C -> G
        g.AddDirectedEdge(2, 7, 103);		// C -> H

        g.AddDirectedEdge(7, 3, 183);       // H -> D

        g.AddDirectedEdge(7, 3, 250);       // F -> I

        g.AddDirectedEdge(8, 9, 84);		// I -> J

        g.AddDirectedEdge(7, 9, 167);       // H -> J

        g.AddDirectedEdge(4, 9, 502);       // E -> J
        

        return g;
	}

	public void AddNode(int node_id, in T node_data)
	{
		nodes.Add(new GraphNode<T>(node_id, node_data));
	}

	public void AddDirectedEdge(int id_from,  int id_to, float cost)
	{
		if(id_from != id_to) 
		{
			GraphNode<T> from = null, to = null;

			foreach (GraphNode<T> node in nodes)
			{
				if (node.id == id_from)
				{
					from = node;
				}
				else if (node.id == id_to)
				{
					to = node;
				}
			}

            if (from != null && to != null)
			{
                AddDirectedEdge(ref from, ref to, cost);
			}
		}
	}

	private void AddDirectedEdge(ref GraphNode<T> from, ref GraphNode<T> to, float cost)
	{
		from.AddConnection(ref to, cost);
	}
}
