using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Compass/Game Data")]
public class GameData : ScriptableObject
{
    public float targetDistance;

    [Header("Tile")]
    public  int numberTiles;
    public float tileSize;
    public GameObject tileLeft;
    public GameObject tileRight;

    [Header("Path Creator")]
    public List<string> moveList;
    public int numberOfMoves;
    public int numberPathAttempts;
    
    [Header("Tile Colors")]
    public Material[] tileColors;
    public Material startTileColor;
    public Material endTileColor;

    public Vector3 TilePosition(int col, int row)
    {
        return(new Vector3(col * tileSize, 0.0f, row * tileSize));
    }

    public GameObject GetCheckeredTile(int col, int row)
    {
        return((((row + col) % 2) == 0) ? tileLeft : tileRight);
    }
}
