using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileClass : MonoBehaviour
{
    // this is for each tile on the board
    // each tile has a position and holds a piece

    public Vector2 arrayPos; // our position in the array
    public PieceClass heldPiece; // when this is empty it returns null

    private void Start()
    {
        gameObject.name = arrayPos.ToString();
    }
}
