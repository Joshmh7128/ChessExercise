using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece_Queen : PieceClass
{
    public override void ShowPossibleMoves()
    {
        if (transform.childCount == 0)
        {
            // queen moves in all directions
            IterateCheck(1, 0);
            IterateCheck(-1, 0);
            IterateCheck(0, 1);
            IterateCheck(0, -1);
            IterateCheck(1, 1);
            IterateCheck(-1, -1);
            IterateCheck(-1, 1);
            IterateCheck(1, -1);
        }
    }

}
