using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AnimalController : MonoBehaviour
{
	[SerializeField] private MapManager mapmanager_instance;
	private bool is_moving;
	[SerializeField] private float move_time;
	public float time_available;

	// Start is called before the first frame update
	void Start()
	{
		mapmanager_instance = GameObject.Find("MapManager").GetComponent<MapManager>();
	}

	// Update is called once per frame
	void Update()
	{
		
	}
   
	public void MoveToward(Vector3 goal_position)
	{
		Dictionary<GraphNode, float> costs = new Dictionary<GraphNode, float>();
		Dictionary<GraphNode, GraphNode> previous = new Dictionary<GraphNode, GraphNode>();

		List<GraphNode> unvisited = new List<GraphNode>();

		GraphNode start = mapmanager_instance.GetNode(transform.position);
		GraphNode target = mapmanager_instance.GetNode(goal_position);

		costs[start] = 0;
		previous[start] = null;

		foreach(KeyValuePair<Vector2Int, GraphNode> entry in mapmanager_instance.graph)
		{
			if(entry.Value != start)
			{
				costs[entry.Value] = Mathf.Infinity;
				previous[entry.Value] = null;
			}

			unvisited.Add(entry.Value);
		}

		while(unvisited.Count > 0)
		{
			// get the closest unvisited node
			GraphNode node = null;
			foreach(GraphNode n in unvisited)
			{
				if(node == null || costs[n] < costs[node])
				{
					node = n;
				}
			}

			// if the closest unvisited node is the target, loop ends
			if(node == target)
			{
				break; 
			}

			unvisited.Remove(node);

			foreach(GraphNode n in node.neighbours)
			{
				float d = costs[node] + (node.speed_multiplier + n.speed_multiplier) / 2;

				if(d < costs[n])
				{
					costs[n] = d;
					previous[n] = node;
				}
			}
		}

		// Here, the shortest path has been found (or there is none)
		if (previous[target] == null)
		{
			// no path between start and target
		}
		else
		{
			// Find the longest move depending on the time available
			GraphNode current_path = target;

			while(current_path != null)
			{
				if (costs[current_path] <= time_available)
				{
					time_available -= costs[current_path];
					break;
				}
				current_path = previous[current_path];
			}

			// move animal to the current farthest node it can reach
			Vector3 movement = current_path.position;
			movement -= mapmanager_instance.GetNode(transform.position).position;

			StartCoroutine(MoveAnimal(movement));
		}
	}

	IEnumerator MoveAnimal(Vector3 movement)
	{
		is_moving = true;

		float next_move = 0;
		Vector3 start_position = transform.position;
		Vector3 end_position = start_position + movement;

		while (next_move < move_time)
		{
			transform.position = Vector3.Lerp(start_position, end_position, next_move / move_time);
			next_move += Time.deltaTime;
			yield return null;
		}

		transform.position = end_position;

		is_moving = false;
	}
}
