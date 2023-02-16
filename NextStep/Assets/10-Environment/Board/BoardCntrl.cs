using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCntrl : MonoBehaviour
{
    [SerializeField] private GameObject tileRight;
    [SerializeField] private GameObject tileLeft;

    private Dictionary<BoardIndex, BoardTile> board;

    private int size = 7;

    // Start is called before the first frame update
    void Start()
    {
        CreateBoard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateBoard() 
    {
        GameObject boardParent = new GameObject("Board");

        board = new Dictionary<BoardIndex, BoardTile>();

        for (int col = 0; col < size; col++) 
        {
            for (int row = 0; row < size; row++)
            {
                GameObject square = (((row + col) % 2) == 0) ? tileLeft : tileRight;
                Vector3 position = new Vector3(col*5.0f, 0.0f, row*5.0f);

                GameObject tile = Instantiate(square, position, Quaternion.identity);
                tile.transform.parent = boardParent.transform;

                board.Add(new BoardIndex(col, row), new BoardTile(tile));
            }
        }

        BoardTile start = GetRandomTile();
        start.SetColor(Color.red);
    }

    private BoardTile GetRandomTile()
    {
        BoardTile tile = null;
        int col = Random.Range(0, size);
        int row = Random.Range(0, size);

        if (!board.TryGetValue(new BoardIndex(col, row), out tile)) {
            tile = null;
        }

        return(tile);
    }
}
