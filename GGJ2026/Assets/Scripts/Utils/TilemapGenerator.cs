using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapGenerator : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;

    [SerializeField] private int mapRadius;

    [SerializeField] private List<Tile> tiles;

    private void Start()
    {
        GenerateChunk();
    }
    
    public void GenerateChunk()
    {
        for (int i = -mapRadius; i < mapRadius; i++)
        {
            for (int j = -mapRadius; j < mapRadius; j++)
            {
                tilemap.SetTile(new Vector3Int(i, j, 0), tiles[Random.Range(0, tiles.Count)]);
            }
        }
    }
}
