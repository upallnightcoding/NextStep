using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCntrl : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private GameObject tileRight;
    [SerializeField] private GameObject tileLeft;
    [SerializeField] private UICntrl uiCntrl;

    //private Dictionary<BoardIndex, BoardTile> board;
    //private BoardTile current = null;

    BoardModel boardModel = null;
    
    private string PickAMove() => (GetRandomMove()); 

    // Start is called before the first frame update
    void Start()
    {
        boardModel = new BoardModel();

        InitializeBoard();
        //CreatePath();
    }

    private void InitializeBoard() 
    {
        GameObject boardParent = new GameObject("Board");

        for (int col = 0; col < gameData.numberTiles; col++) 
        {
            for (int row = 0; row < gameData.numberTiles; row++)
            {
                GameObject square = gameData.GetCheckeredTile(col, row);
                Vector3 position = gameData.TilePosition(col, row);

                GameObject tile = Instantiate(square, position, Quaternion.identity);
                tile.transform.parent = boardParent.transform;

                boardModel.Add(col, row, tile);
            }
        }
    }

    // private void CreatePath() 
    // {
    //     bool validPath = false;
    //     int attempts = 0;

    //     while (!validPath && (attempts++ < gameData.numberPathAttempts)) 
    //     {
    //         Stack<BoardTile> stack = new Stack<BoardTile>();
    //         int count = 1;
    //         bool validMove = true;

    //         current = CreateStartingPoint();
    //         stack.Push(current);

    //         while((count <= gameData.numberOfMoves) && validMove && (attempts++ < gameData.numberPathAttempts))
    //         {
    //             string move = PickAMove();
    //             validMove = MakeMove(move, stack);
    //             uiCntrl.SetBtn(count - 1, move);
    //             count++;
    //         }

    //         if (validMove) {
    //             validPath = true;
    //             stack.Peek().SetColor(Color.red);
    //         } else {
    //             while(stack.Count > 0) 
    //             {
    //                 BoardTile tile = stack.Pop();
    //                 tile.RevertColor();
    //                 tile.MarkAsUnVisited();
    //             }
    //         }
    //     }

    //     Debug.Log($"Attempts: {attempts}");
    // }

    // private bool MakeMove(string move, Stack<BoardTile> stack)
    // {
    //     BoardTile startingTile = current;
    //     int movechar = 0;
    //     bool valid = true;

    //     while ((movechar < move.Length) && valid)
    //     {
    //         MoveStep step = GetStep(move.Substring(movechar, 1));
    //         BoardIndex nextStep = current.NextStep(step);
    //         current = GetBoardTile(nextStep);

    //         if ((current != null) && (current.IsMarkAsUnVisited()))
    //         {
    //             current.SetColor(Color.cyan);
    //             current.MarkAsVisited();
    //             stack.Push(current);
    //         } else {
    //             valid = false;
    //             Debug.Log("Failed ..." + current + "/" + current?.IsMarkAsUnVisited());
    //         }

    //         movechar++;
    //     }

    //     return(valid);
    // }


    private BoardTile CreateStartingPoint()
    {
        BoardTile startPoint = GetRandomTile();
        startPoint.SetColor(Color.green);
        startPoint.MarkAsVisited();

        return(startPoint);
    }

    // private void CreateBoard() 
    // {
    //     GameObject boardParent = new GameObject("Board");

    //     board = new Dictionary<BoardIndex, BoardTile>();

    //     for (int col = 0; col < gameData.numberTiles; col++) 
    //     {
    //         for (int row = 0; row < gameData.numberTiles; row++)
    //         {
    //             GameObject square = (((row + col) % 2) == 0) ? tileLeft : tileRight;
    //             Vector3 position = new Vector3(col*gameData.tileSize, 0.0f, row*gameData.tileSize);

    //             GameObject tile = Instantiate(square, position, Quaternion.identity);
    //             tile.transform.parent = boardParent.transform;

    //             board.Add(new BoardIndex(col, row), new BoardTile(col, row, tile));
    //         }
    //     }
    // }

    private string GetRandomMove()
    {
        return(gameData.moveList[Random.Range(0, gameData.moveList.Count)]);
    }

    private BoardTile GetBoardTile(BoardIndex step)
    {
        BoardTile tile = null;

        // if (!board.TryGetValue(step, out tile)) {
        //     tile = null;
        // }

        return(tile);
    }

    private BoardTile GetRandomTile()
    {
        BoardTile tile = null;
        int col = Random.Range(0, gameData.numberTiles);
        int row = Random.Range(0, gameData.numberTiles);

        // if (!board.TryGetValue(new BoardIndex(col, row), out tile)) {
        //     tile = null;
        // }

        return(tile);
    }

    private MoveStep GetStep(string moveChar)
    {
        MoveStep step = null;

        switch(moveChar) {
            case "E": 
                step = new MoveStep(1, 0);
                break;
            case "W":
                step = new MoveStep(-1, 0);
                break;
            case "N": 
                step = new MoveStep(0, 1);
                break;
            case "S":
                step = new MoveStep(0, -1);
                break;
        }

        return(step);
    }
}

public class BoardModel 
{
    private Dictionary<BoardIndex, BoardTile> board = null;

    public BoardModel() 
    {
        board = new Dictionary<BoardIndex, BoardTile>();
    }

    public void Add(int col, int row, GameObject tile) 
    {
        board.Add(new BoardIndex(col, row), new BoardTile(col, row, tile));    
    }
}

public class MoveStep
{
    public int Col { get; set; }
    public int Row { get; set; }

    public MoveStep(int col, int row)
    {
        this.Col = col;
        this.Row = row;
    }

    public override string ToString()
    {
        return($"MoveStep: {Col}/{Row}");
    }
}