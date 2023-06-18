using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossibleMoveClass : MonoBehaviour
{
    // this class exists to display a possible move for a piece
    public bool isAttack; // is this an attack?
    public int arrayX, arrayY; // our positions in the array
    [SerializeField] Sprite possibleMove, possibleAttack; // pieces for when it is a possible move or attack
    [SerializeField] PieceClass pieceClass; // our parent piece class, always our parent object

    bool mouseOver; // is the mouse over?

    private void Start()
    {
        // set our parent class
        pieceClass = transform.parent.GetComponent<PieceClass>();

        // set our sprite
        gameObject.GetComponent<SpriteRenderer>().sprite = isAttack ? possibleAttack : possibleMove;
    }

    private void Update()
    {
        // if we are clicked on...
        if (mouseOver && Input.GetMouseButtonDown(0))
        {
            // make sure we remove our current piece from the current tile it is on
            BoardManager.instance.boardTiles[(int)pieceClass.arrayPos.x, (int)pieceClass.arrayPos.y].heldPiece = null;
            // move the piece
            pieceClass.GoToPossibleMove(new Vector2(transform.position.x, transform.position.y), new Vector2(arrayX, arrayY));
            // check if this is an attack BEFORE setting the new piece
            if (isAttack)
            {
                // destroy the piece still there
                BoardManager.instance.boardTiles[arrayX, arrayY].heldPiece.ManualDestroy();
            }
            // set the new held piece
            BoardManager.instance.boardTiles[arrayX, arrayY].heldPiece = pieceClass;
            // call destruction
            pieceClass.ChildRemoval();
            // destroy this object
            Destroy(gameObject);
        }
    }

    private void OnMouseOver()
    {
        mouseOver = true;
    }

    private void OnMouseExit()
    {
        mouseOver = false;
    }
}
