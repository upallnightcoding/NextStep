using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCntrl : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private GameObject tileRight;
    [SerializeField] private GameObject tileLeft;

    private Dictionary<BoardIndex, BoardTile> board;
    private BoardTile current = null;

    // Start is called before the first frame update
    void Start()
    {
        CreateBoard();
        CreatePath();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreatePath() 
    {
        bool validPath = false;
        int attempts = 0;

        while (!validPath && (attempts++ < gameData.numberPathAttempts)) 
        {
            Stack<BoardTile> stack = new Stack<BoardTile>();
            int count = 1;
            int turn = 0;
            bool validMove = true;

            current = CreateStartingPoint();
            stack.Push(current);

            while((count <= gameData.numberOfMoves) && validMove && (attempts++ < gameData.numberPathAttempts))
            {
                validMove = MakeMove(turn++, PickAMove(), stack);
                count++;
            }

            if (validMove) {
                validPath = true;
                current.SetColor(Color.red);
            } else {
                while(stack.Count > 0) 
                {
                    Debug.Log("Pop: " + stack.Peek().ToString());
                    BoardTile tile = stack.Pop();
                    tile.RevertColor();
                    tile.MarkAsUnVisited();
                }
            }
        }

        Debug.Log($"Attempts: {attempts}");
    }

    private bool MakeMove(int turn, string move, Stack<BoardTile> stack)
    {
        BoardTile startingTile = current;
        int movechar = 0;
        bool valid = true;

        Debug.Log($"Move: {move}/{startingTile.ToString()}");

        Color turnColor = (turn % 2) == 0 ? Color.cyan : Color.yellow;

        while ((movechar < move.Length) && valid)
        {
            MoveStep step = GetStep(move.Substring(movechar, 1));
            BoardIndex nextStep = current.NextStep(step);
            current = GetBoardTile(nextStep);

            if ((current != null) && (current.IsMarkAsUnVisited()))
            {
                current.SetColor(turnColor);
                current.MarkAsVisited();
                stack.Push(current);
            } else {
                valid = false;
                Debug.Log("Failed ..." + current + "/" + current?.IsMarkAsUnVisited());
            }

            movechar++;
        }

        return(valid);
    }

    private string PickAMove()
    {
        string move = GetRandomMove();

        return(move);
    }

    private BoardTile CreateStartingPoint()
    {
        BoardTile startPoint = GetRandomTile();
        startPoint.SetColor(Color.green);
        startPoint.MarkAsVisited();

        return(startPoint);
    }

    private void CreateBoard() 
    {
        GameObject boardParent = new GameObject("Board");

        board = new Dictionary<BoardIndex, BoardTile>();

        for (int col = 0; col < gameData.numberTiles; col++) 
        {
            for (int row = 0; row < gameData.numberTiles; row++)
            {
                GameObject square = (((row + col) % 2) == 0) ? tileLeft : tileRight;
                Vector3 position = new Vector3(col*gameData.tileSize, 0.0f, row*gameData.tileSize);

                GameObject tile = Instantiate(square, position, Quaternion.identity);
                tile.transform.parent = boardParent.transform;

                board.Add(new BoardIndex(col, row), new BoardTile(col, row, tile));
            }
        }
    }

    private string GetRandomMove()
    {
        return(gameData.moveList[Random.Range(0, gameData.moveList.Count)]);
    }

    private BoardTile GetBoardTile(BoardIndex step)
    {
        BoardTile tile = null;

        if (!board.TryGetValue(step, out tile)) {
            tile = null;
        }

        return(tile);
    }

    private BoardTile GetRandomTile()
    {
        BoardTile tile = null;
        int col = Random.Range(0, gameData.numberTiles);
        int row = Random.Range(0, gameData.numberTiles);

        if (!board.TryGetValue(new BoardIndex(col, row), out tile)) {
            tile = null;
        }

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