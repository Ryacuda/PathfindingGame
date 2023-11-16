using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu()]
public class TileData : ScriptableObject
{
    public TileBase[] tiles;
    public float player_cost_multiplier;
    public float animal_cost_multiplier;
}
