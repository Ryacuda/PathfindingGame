using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Pathfinding<T>
{
    class DistanceData
    {
        public float distance;
        public GraphNode<T> previous_node;

        public DistanceData(float distance, ref GraphNode<T> p_node)
        {
            this.distance = distance;
            previous_node = p_node;
        }

        public DistanceData(float distance)
        {
            this.distance = distance;
            previous_node = null;
        }
    }


    public static List<GraphNode<T>> Dijkstra(in Graph<T> graph, int id_from, int id_to)
    {
        // validate inputs
        if (graph == null || (id_from == id_to))
        {
            return null;
        }

        bool from_inside = false;
        bool to_inside = false;
        foreach( GraphNode<T> node in graph.nodes )
        {
            if(node.id == id_from)
            {
                from_inside = true;
            }
            else if(node.id == id_to)
            {
                to_inside = true;
            }
        }

        if(!from_inside || !to_inside)
        {
            return null;
        }

        // dijkstra's algorithm
        Graph<DistanceData> distance_graph = new Graph<DistanceData>();

        distance_graph.AddNode(id_from, new DistanceData(0));


    }
}
