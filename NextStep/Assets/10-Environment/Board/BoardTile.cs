using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardTile 
{
    public GameObject Tile  { get; private set; }
    public int Col          { get; private set; }
    public int Row          { get; private set; }

    public Vector3 Position() => Tile.transform.position;

    public void MarkAsVisited() => state = BoardTileState.VISITED;
    public void MarkAsUnVisited() => state = BoardTileState.UNVISTIED;

    public bool IsMarkAsUnVisited() => (state == BoardTileState.UNVISTIED);

    private BoardTileState state = BoardTileState.UNVISTIED;

    private Color originalColor = Color.black;

    public BoardTile(int col, int row, GameObject tile)
    {
        this.Col = col;
        this.Row = row;
        this.Tile = tile;
    }

    public BoardIndex NextStep(MoveStep step)
    {
        return(new BoardIndex(Col + step.Col, Row + step.Row));
    }

    public void SetColor(Color color)
    {
        Material material = Tile
            .transform
            .GetChild(0)
            .GetComponent<MeshRenderer>()
            .material;

        originalColor = material.color;

        material.color = color;
    }

    public void RevertColor() 
    {
        Material material = Tile
            .transform
            .GetChild(0)
            .GetComponent<MeshRenderer>()
            .material;

        material.color = originalColor;
    }

    public override string ToString()
    {
        return($"Col/Row: {Col}/{Row}");
    }

}

public enum BoardTileState
{
    VISITED, 
    UNVISTIED
}
