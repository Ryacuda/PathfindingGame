using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
	[SerializeField] private Tilemap map;
	[SerializeField] private List<TileData> tileDatas;
	[SerializeField] private TileBase spawn_tile;

	public Dictionary<Vector2Int, GraphNode> graph;

	private List<TileData> tiles;
	private Dictionary<TileBase, TileData> dataFromTiles;

	private void Awake()
	{
		dataFromTiles = new Dictionary<TileBase, TileData>();

		foreach(var data in tileDatas)
		{
			foreach(var tile in data.tiles) 
			{
				dataFromTiles.Add(tile, data);
			}
		}
	}

	// Start is called before the first frame update
	void Start()
	{
		GenerateGraph();
	}

	// Update is called once per frame
	void Update()
	{

	}

	private void GenerateGraph()
	{
		graph = new Dictionary<Vector2Int, GraphNode>();

		// populate graph
		foreach (Vector3Int localtilepos in map.cellBounds.allPositionsWithin)
		{
			if(map.HasTile(localtilepos))
			{
				GraphNode node = new GraphNode();

				node.player_multiplier = GetPlayerCostMultiplier(new Vector2(localtilepos.x, localtilepos.y));
				node.animal_multiplier = GetAnimalCostMultiplier(new Vector2(localtilepos.x, localtilepos.y));
				node.position = localtilepos;

				graph[new Vector2Int(localtilepos.x, localtilepos.y)] = node;
			}
		}

		// connect nodes
		foreach (KeyValuePair<Vector2Int, GraphNode> entry in graph)
		{
			// connect the 4 neighbours (top-bottom-left-right) if they exist in the graph
			if (graph.ContainsKey(entry.Key + Vector2Int.left))
			{
				entry.Value.neighbours.Add(graph[entry.Key + Vector2Int.left]);
			}

			if (graph.ContainsKey(entry.Key + Vector2Int.right))
			{
				entry.Value.neighbours.Add(graph[entry.Key + Vector2Int.right]);
			}

			if (graph.ContainsKey(entry.Key + Vector2Int.up))
			{
				entry.Value.neighbours.Add(graph[entry.Key + Vector2Int.up]);
			}

			if (graph.ContainsKey(entry.Key + Vector2Int.down))
			{
				entry.Value.neighbours.Add(graph[entry.Key + Vector2Int.down]);
			}
		}
	}

	public float GetPlayerCostMultiplier(Vector3 world_position)
	{
		TileBase tile = map.GetTile(map.WorldToCell(world_position));

		if (tile == null)
			return 0f;

		return dataFromTiles[tile].player_cost_multiplier;
	}

	public float GetAnimalCostMultiplier(Vector3 world_position)
	{
		TileBase tile = map.GetTile(map.WorldToCell(world_position));

		if (tile == null)
			return 0f;

		return dataFromTiles[tile].animal_cost_multiplier;
	}

	public GraphNode GetNode(Vector3 worldpos)
	{
		Vector3Int p = map.WorldToCell(worldpos);
		return graph[new Vector2Int(p.x, p.y)];
	}

	// increments animal time, and provide them the opportunity to move if they can
	public void IncrementAnimalTime(float time)
	{
		Vector3 playerpos = GameObject.Find("amogus").transform.position;

		foreach (GameObject go in GameObject.FindGameObjectsWithTag("Animal"))
		{
			// give time to animals
			go.GetComponent<AnimalController>().time_available += time;

			// decide if the animal goes for the fish or the player (only decide to chase the player if it's close enough)
			if(Vector3.Distance(playerpos, go.transform.position) < 7f)
			{	// go toward player
				go.GetComponent<AnimalController>().DijkstraMoveToward(playerpos);
			}
			else
			{	// go toward fish
				go.GetComponent<AnimalController>().AStarMoveToward(); 
			}
		}
	}

	public Vector3 GetRandomTerrainLocation()
	{
		List<Vector3> possible_coords = new List<Vector3>();

		foreach (Vector3Int localtilepos in map.cellBounds.allPositionsWithin)
		{
			if (map.HasTile(localtilepos) && map.GetTile(localtilepos) == spawn_tile)
			{
				possible_coords.Add(map.LocalToWorld(localtilepos));
			}
		}

		return possible_coords[Random.Range(0, possible_coords.Count-1)];
	}
}
