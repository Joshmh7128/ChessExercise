using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece_King : PieceClass
{
    public override void ShowPossibleMoves()
    {
        if (transform.childCount == 0)
        {
            // king moves one in all directions
            DirectCheck(1, 0);
            DirectCheck(-1, 0);
            DirectCheck(0, 1);
            DirectCheck(0, -1);
            DirectCheck(1, 1);
            DirectCheck(-1, -1);
            DirectCheck(-1, 1);
            DirectCheck(1, -1);
        }
    }

    // called when we want to destroy this object
    public override void ManualDestroy()
    {
        // put ourselves into the correct deathzone
        if (isWhite) TurnManager.instance.whiteZone.AddPiece(whiteSprite);
        if (!isWhite) TurnManager.instance.blackZone.AddPiece(blackSprite);

        TurnManager.instance.Victory();

        // then destroy this game object
        Destroy(gameObject);
    }
}
