using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece_Pawn : PieceClass
{

    public override void ShowPossibleMoves()
    {
        // knight can move to the end of an L in 8 directions
        if (isWhite)
        {
            PawnCheck(0, 1);

            if (firstMove)
                PawnCheck(0, 2);


            // do our attack checks
            PawnAttackCheck(1, 1);
            PawnAttackCheck(-1, 1);
        }

        if (!isWhite)
        {
            PawnCheck(0, -1);

            if (firstMove)
                PawnCheck(0, -2);


            // do our attack checks
            PawnAttackCheck(1, -1);
            PawnAttackCheck(-1, -1);
        }
    }

    // use this to perform a relative check to where the piece is
    public void PawnCheck(int xp, int yp)
    {
        if ((int)arrayPos.x + xp < 8 && (int)arrayPos.x + xp > -1 && (int)arrayPos.y + yp < 8 && (int)arrayPos.y + yp > -1)
        {
            // if there is nothing there...
            if (boardManager.boardTiles[(int)arrayPos.x + xp, (int)arrayPos.y + yp].heldPiece == null)
            {
                PossibleMoveClass move = Instantiate(possibleMove, new Vector2((int)arrayPos.x + xp, (int)arrayPos.y + yp), Quaternion.identity, transform).GetComponent<PossibleMoveClass>();
                move.isAttack = false;
                move.arrayX = (int)arrayPos.x + xp;
                move.arrayY = (int)arrayPos.y + yp;
            }

            // if there is something there...
                // do nothing!
        }
    }

    // for our attacks, check to see if we can attack
    void PawnAttackCheck(int xp, int yp)
    {
        if ((int)arrayPos.x + xp < 8 && (int)arrayPos.x + xp > -1 && (int)arrayPos.y + yp < 8 && (int)arrayPos.y + yp > -1)
            if (boardManager.boardTiles[(int)arrayPos.x + xp, (int)arrayPos.y + yp].heldPiece != null)
            {
                // make sure this is the opposite color
                if (BoardManager.instance.boardTiles[(int)arrayPos.x + xp, (int)arrayPos.y + yp].heldPiece.isWhite != isWhite)
                {
                    PossibleMoveClass move = Instantiate(possibleMove, new Vector2((int)arrayPos.x + xp, (int)arrayPos.y + yp), Quaternion.identity, transform).GetComponent<PossibleMoveClass>();
                    move.isAttack = true;
                    move.arrayX = (int)arrayPos.x + xp;
                    move.arrayY = (int)arrayPos.y + yp;
                }
            }
    }

}
