using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCntrl : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private GameObject tileRight;
    [SerializeField] private GameObject tileLeft;
    [SerializeField] private UICntrl uiCntrl;

    private BoardModel boardModel = null;

    private GamePath gamePath;
    
    // Start is called before the first frame update
    void Start()
    {
        boardModel = new BoardModel();

        InitializeBoard();
        gamePath = CreateGamePath();
    }

    public void ShowMove(string move, Material color) 
    {
        gamePath.ShowMove(move, color);
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

    private GamePath CreateGamePath()
    {
        bool pathComplete = false;
        int attempts = 0;
        GamePath gamePath = null;

        while (!pathComplete && (attempts++ < gameData.numberPathAttempts))
        {
            int numberOfMoves = 1;
            bool validMove = true;
            
            gamePath = new GamePath(gameData, boardModel);

            while(validMove && (numberOfMoves <= gameData.numberOfMoves))
            {
                string move = GetRandomMove();
                validMove = gamePath.append(move);
                uiCntrl.SetBtn(numberOfMoves - 1, move);
                numberOfMoves++;
            }

            if (validMove) {
                gamePath.Complete();
                pathComplete = true;
            } else {
                gamePath.BackTrack();
            }
        }

        if (!pathComplete) {
            gamePath = null;
        }

        return(gamePath);
    }

    private string GetRandomMove()
    {
        return(gameData.moveList[Random.Range(0, gameData.moveList.Count)]);
    }

    // private bool MakeMove(string move, Stack<BoardTile> stack) 
    // {
    //     bool valid = true;
    //     int moveChar = 0;

    //     while (valid && (moveChar < move.Length))
    //     {
    //         //MoveStep step = GetStep(move.Substring(moveChar, 1));
    //         //BoardIndex nextStep = current.NextStep(step);
    //     }

    //     return(valid);
    // }

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

    // private BoardTile GetBoardTile(BoardIndex step)
    // {
    //     BoardTile tile = null;

    //     // if (!board.TryGetValue(step, out tile)) {
    //     //     tile = null;
    //     // }

    //     return(tile);
    // }

    
}

public class BoardModel 
{
    private Dictionary<BoardIndex, BoardTile> board = null;

    public BoardModel() 
    {
        board = new Dictionary<BoardIndex, BoardTile>();
    } 

    public BoardTile GetTile(int col, int row) 
    {
        BoardTile tile = null;

        if (!board.TryGetValue(new BoardIndex(col, row), out tile)) {
            tile = null;
        }

        return(tile);
    }

    public BoardTile GetTile(BoardIndex boardIndex) 
    {
        BoardTile tile = null;

        if (!board.TryGetValue(boardIndex, out tile)) {
            tile = null;
        }

        return(tile);
    }

    public void Add(int col, int row, GameObject tile) 
    {
        board.Add(new BoardIndex(col, row), new BoardTile(col, row, tile));    
    }

    public BoardTile GetRandomTile(GameData gameData)
    {
        int col = Random.Range(0, gameData.numberTiles);
        int row = Random.Range(0, gameData.numberTiles);

        return(GetTile(col, row));
    }
}

public class GamePath
{
    private Stack<BoardTile> backTrack = new Stack<BoardTile>();
    private BoardTile pathStartPoint;
    private BoardTile drawPoint;
    private GameData gameData;
    private BoardModel boardModel;
    private BoardTile currentTile;

    public GamePath(GameData gameData, BoardModel boardModel)
    {
        this.gameData = gameData;
        this.boardModel = boardModel;

        CreateStartingPoint();
    }

    public void ShowMove(string move, Material material)  
    {
        for (int moveChar = 0; moveChar < move.Length; moveChar++)
        {
            MoveStep step = GetStep(move, moveChar);
            BoardIndex nextStep = drawPoint.Next(step);
            drawPoint = boardModel.GetTile(nextStep);
            drawPoint.Set(material.color);
        }
    }

    public bool append(string move) 
    {
        int movePos = 0;
        bool valid = true;

        Debug.Log($"Move: {move}");

        while (valid && (movePos < move.Length))
        {
            MoveStep step = GetStep(move, movePos);
            BoardIndex nextStep = currentTile.Next(step);
            currentTile = boardModel.GetTile(nextStep);

            if ((currentTile != null) && (currentTile.IsMarkAsUnVisited()))
            {
                //currentTile.MarkAsVisited(Color.cyan);
                currentTile.MarkAsVisited();
                backTrack.Push(currentTile);
                movePos++;
            } else {
                valid = false;
                Debug.Log($"Path Failed - Current Tile: {currentTile} {currentTile?.IsMarkAsUnVisited()}");
            }
        }

        return(valid);
    }

    public void Complete()
    {
        currentTile.MarkAsVisited(Color.red);
    }

    public void BackTrack()
    {
        while(backTrack.Count > 0) 
        {
            BoardTile tile = backTrack.Pop();
            Debug.Log($"Back Track: {tile}");
            tile.RevertColor();
            tile.MarkAsUnVisited();
        }
    }

    private MoveStep GetStep(string move, int movePos)
    {
        MoveStep step = null;

        switch(move.Substring(movePos, 1)) {
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

    private void CreateStartingPoint()
    {
        pathStartPoint = boardModel.GetRandomTile(gameData);
        pathStartPoint.MarkAsVisited(Color.green);

        currentTile = pathStartPoint;
        drawPoint = pathStartPoint;

        backTrack.Push(pathStartPoint);

        Debug.Log($"Start Position - Current Tile: {pathStartPoint}");
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