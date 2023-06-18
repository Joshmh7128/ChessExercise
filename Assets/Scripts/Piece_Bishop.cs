using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece_Bishop : PieceClass
{
    public override void ShowPossibleMoves()
    {
        if (transform.childCount == 0)
        {
            // bishop moves diagonally
            IterateCheck(1, 1);
            IterateCheck(-1, -1);
            IterateCheck(-1, 1);
            IterateCheck(1, -1);
        }
    }
}
