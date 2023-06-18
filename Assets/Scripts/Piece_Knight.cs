using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece_Knight : PieceClass
{
    public override void ShowPossibleMoves()
    {
        if (transform.childCount == 0)
        {
            // knight can move to the end of an L in 8 directions
            DirectCheck(1, 2);
            DirectCheck(2, 1);
            DirectCheck(-1, 2);
            DirectCheck(-2, 1);
            DirectCheck(1, -2);
            DirectCheck(2, -1);
            DirectCheck(-1, -2);
            DirectCheck(-2, -1);
        }
    }
}
