using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardTile : MonoBehaviour
{
    private GameObject tile;

    public Vector3 Position => tile.transform.position;

    public BoardTile(GameObject tile)
    {
        this.tile = tile;
    }

    public void SetColor(Color color)
    {
        tile.GetComponent<MeshRenderer>().material.color = color;
    }

}
