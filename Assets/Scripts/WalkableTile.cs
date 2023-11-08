using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class WalkableTile : Tile
{
	[SerializeField] GameObject node;

	public override void RefreshTile(Vector3Int location, ITilemap tilemap)
	{
		base.RefreshTile(location, tilemap);
	}

	public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
	{
		base.GetTileData(position, tilemap, ref tileData);

		tileData.gameObject = Instantiate(node);
		tileData.gameObject.GetComponent<GraphNode>().position = this.transform.GetPosition();
	}

#if UNITY_EDITOR
	[MenuItem("Assets/Create/2D/Tiles/Custom Tiles/Walkable Tile")]
	public static void CreateWalkableTile()
	{
		string path = EditorUtility.SaveFilePanelInProject("Save Walkable Tile", "New Walkable Tile", "Asset", "Save Walkable Tile", "Assets");
		if (path == "") return;

		AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<WalkableTile>(), path);
	}
#endif
}
