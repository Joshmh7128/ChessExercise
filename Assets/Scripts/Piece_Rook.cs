using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece_Rook : PieceClass
{
    public override void ShowPossibleMoves()
    {
        if (transform.childCount == 0)
        {
            // the rook can move vertically or horizontally. cycle through all of the possible upwards, downwards, leftwards, and rightwards positions
            IterateCheck(1, 0);
            IterateCheck(-1, 0);
            IterateCheck(0, 1);
            IterateCheck(0, -1);
        }
    }

}
