using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AnimalController : MonoBehaviour
{
	private MapManager mapmanager_instance;
	[SerializeField] private float move_time;
	[SerializeField] private GameObject fish_prefab;
	public float time_available;
	public int score;
	private Vector3 fish_position;

	// Start is called before the first frame update
	void Start()
	{
		mapmanager_instance = GameObject.Find("MapManager").GetComponent<MapManager>();
		score = 0;
		SpawnSalmon();
	}

	// Update is called once per frame
	void Update()
	{
		
	}
	
	// Dijkstra
	public void DijkstraMoveToward(Vector3 goal_position)
	{
		GraphNode start = mapmanager_instance.GetNode(transform.position);
		GraphNode target = mapmanager_instance.GetNode(goal_position);

		Dictionary<GraphNode, float> costs = new Dictionary<GraphNode, float>();
		Dictionary<GraphNode, GraphNode> previous = new Dictionary<GraphNode, GraphNode>();

		List<GraphNode> unvisited = new List<GraphNode>();

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
				float d = costs[node] + (node.animal_multiplier + n.animal_multiplier) / 2;

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

	// A*
	public void AStarMoveToward()
	{
		GraphNode start = mapmanager_instance.GetNode(transform.position);
		GraphNode target = mapmanager_instance.GetNode(fish_position);

		List<GraphNode> open_set = new List<GraphNode>();
		HashSet<GraphNode> closed_set = new HashSet<GraphNode>();

		Dictionary<GraphNode, GraphNode> previous = new Dictionary<GraphNode, GraphNode>();
		Dictionary<GraphNode, float> gCosts = new Dictionary<GraphNode, float>();

		open_set.Add(start);
		previous[start] = null;
		gCosts[start] = 0;

		foreach (KeyValuePair<Vector2Int, GraphNode> entry in mapmanager_instance.graph)
		{
			if (entry.Value != start)
			{
				gCosts[entry.Value] = Mathf.Infinity;
				previous[entry.Value] = null;
			}
		}

		while (open_set.Count > 0)
		{
			// search for the lest costly node
			GraphNode node = open_set[0];
			
			for(int i = 1; i < open_set.Count; i++)
			{
				if (gCosts[open_set[i]] + open_set[i].hCost(fish_position) < gCosts[node] + node.hCost(fish_position)
					|| (gCosts[open_set[i]] + open_set[i].hCost(fish_position) == gCosts[node] + node.hCost(fish_position) && open_set[i].hCost(fish_position) < node.hCost(fish_position)) )
				{
					node = open_set[i];
				}
			}

			open_set.Remove(node);
			closed_set.Add(node);

			// exit if target is found
			if(node == target)
			{
				break;
			}

			foreach(GraphNode n in node.neighbours)
			{
				if(closed_set.Contains(n))
				{
					continue;
				}

				float movement_cost_to_neighbour = gCosts[node] + (n.animal_multiplier + node.animal_multiplier)/2f;

				if(movement_cost_to_neighbour < gCosts[n] || !open_set.Contains(n))
				{
					gCosts[n] = movement_cost_to_neighbour;
					previous[n] = node;

					if(!open_set.Contains(n))
					{
						open_set.Add(n);
					}
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

			while (current_path != null)
			{
				if (gCosts[current_path] <= time_available)
				{
					time_available -= gCosts[current_path];
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
	}

	public void SpawnSalmon()
	{
		Vector3 p = mapmanager_instance.GetRandomTerrainLocation();

		GameObject go = Instantiate(fish_prefab, p + new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
		go.GetComponent<SpriteRenderer>().sortingOrder = 3;
		go.GetComponent<SalmonManager>().animal_instance = gameObject;

		fish_position = go.transform.position;
	}
}
