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

    [Header("Path Creator")]
    public List<string> moveList;
    public int numberOfMoves;
    public int numberPathAttempts;
}
