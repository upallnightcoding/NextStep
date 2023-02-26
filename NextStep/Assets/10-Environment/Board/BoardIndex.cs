using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardIndex 
{
    public int Col { get; private set; }
    public int Row { get; private set; }

    public BoardIndex(int col, int row) 
    {
        this.Col = col;
        this.Row = row;
    }

    public BoardIndex(MoveStep step)
    {
        this.Col = step.Col;
        this.Row = step.Row;
    }

    public override bool Equals(object obj)
    {
        return obj is BoardIndex index &&
               Col == index.Col &&
               Row == index.Row;
    }

    public override int GetHashCode()
    {
        int hashCode = -1831622508;
        hashCode = hashCode * -1521134295 + Col.GetHashCode();
        hashCode = hashCode * -1521134295 + Row.GetHashCode();
        return hashCode;
    }
}
