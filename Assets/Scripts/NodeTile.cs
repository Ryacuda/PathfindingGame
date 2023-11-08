using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class NodeTile : Tile
{
	public GraphNode<int> node;

	public override void RefreshTile(Vector3Int position, ITilemap tilemap)
	{
		base.RefreshTile(position, tilemap);
	}

	public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
	{
		base.GetTileData(position, tilemap, ref tileData);
	}

	/*
	void ConnectToNeighbours(Vector3Int location, ITilemap tilemap)
	{
		// 8 neighbours
		for(int dx = -1; dx <= 1; dx++)
		{
			for (int dy = -1; dy <= 1; dy++)
			{
				if (dx == 0 && dy == 0) continue;

				TileBase tile = tilemap.GetTile(location + new Vector3Int(dx, dy, 0));

                if (tile is NodeTile)
                {
                    tile.
                }
            }
		}
	}
	*/

#if UNITY_EDITOR
	[MenuItem("Assets/Create/2D/Custom Tiles/Node Tile")]
	public static void CreateNodeTile()
	{
		string path = EditorUtility.SaveFilePanelInProject("Save Node Tile", "New Node Tile", "Asset", "Save Node Tile", "Assets");
		if (path == "") return;

		AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<NodeTile>(), path);
	}
#endif

}
