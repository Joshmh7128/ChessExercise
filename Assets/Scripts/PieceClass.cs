using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public abstract class PieceClass : MonoBehaviour
{
    // this class is the abstract parent class for all of our game pieces
    // every piece has a color
    public bool isWhite; // is this piece white? or black?
    // our white and black sprites
    public Sprite whiteSprite, blackSprite; 
    // every piece has gameobject prefabs to show possible moves
    public GameObject possibleMove;
    // every piece has a position in the array
    public Vector2 arrayPos;
    // is this our first move?
    public bool firstMove = true;

    // letters
    string letters = "abcdefgh";

    // every piece can be selected
    public void OnSelect()
    {
        // make sure it is our turn
        if (TurnManager.instance.isWhiteTurn == isWhite && !TurnManager.instance.victory)
        {
            // make sure we are not showing any moves already
            if (transform.childCount == 0)
                // then show the moves
                ShowPossibleMoves();
        }
    }
    // every piece can be deselected
    public virtual void OnDeselect()
    {
        Invoke("ChildRemoval", 0.01f);
    }
    // every piece has to show its own moves in different ways
    public abstract void ShowPossibleMoves();
    // every piece can be moved to a possible position
    public virtual void GoToPossibleMove(Vector2 worldPosition, Vector2 arrayPosition)
    {
        // build our string
        string logline = gameObject.name + " " + letters[(int)arrayPos.x] + "" + arrayPos.y + " to " + letters[(int)arrayPosition.x] + "" + arrayPosition.y;
        // send our string
        TurnManager.instance.log.text += "\n" + logline;

        // we used our first move
        firstMove = false;
        // move to the new position
        transform.position = worldPosition;
        // update the possible position
        arrayPos = arrayPosition;
        // update the turn
        TurnManager.instance.ChangeTurn();
    }
    // set the sprite of our piece
    public void SetSprite() => gameObject.GetComponent<SpriteRenderer>().sprite = isWhite ? whiteSprite : blackSprite;

    public BoardManager boardManager; // our board manager

    // on start we always set our sprite
    public virtual void Start()
    {
        SetSprite();

        // set our board manager instance
        boardManager = BoardManager.instance;

        // place ourselves on the board
        Invoke("EstablishPlacement", 0.1f);
        EstablishPlacement();
    }

    public virtual void Update()
    {
        if (mouseHovering && Input.GetMouseButtonDown(0))
            OnSelect();

        if (!mouseHovering && Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Escape))
        {
            OnDeselect();
        }
    }

    bool mouseHovering; // is the mouse hovering?

    private void OnMouseOver() => mouseHovering = true;

    private void OnMouseExit() => mouseHovering = false;

    // place ourselves on the board
    void EstablishPlacement()
    {
        if (boardManager != null)
            try
            {
                boardManager.boardTiles[(int)arrayPos.x, (int)arrayPos.y].heldPiece = gameObject.GetComponent<PieceClass>();
            } catch { }
        if (boardManager == null)
            Invoke("EstablishPlacement", 0.1f);
    }

    // function for placing a single possible movement tile
    public void PlacePossibleMoveTile(Vector2 arrayPos)
    {
        // check that position, if it is empty place a green tile, if filled place a red tile
        if (boardManager.boardTiles[(int)arrayPos.x, (int)arrayPos.y].heldPiece == null)
        {
            PossibleMoveClass move = Instantiate(possibleMove, new Vector2(arrayPos.x, arrayPos.y), Quaternion.identity, transform).GetComponent<PossibleMoveClass>();
            move.isAttack = false;
            move.arrayX = (int)arrayPos.x;
            move.arrayY = (int)arrayPos.y;  
        }

        // if this is not null...
        if (boardManager.boardTiles[(int)arrayPos.x, (int)arrayPos.y].heldPiece != null && boardManager.boardTiles[(int)arrayPos.x, (int)arrayPos.y].heldPiece != this)
        {
            // make sure this is the opposite color
            if (BoardManager.instance.boardTiles[(int)arrayPos.x, (int)arrayPos.y].heldPiece.isWhite != isWhite)
            {
                PossibleMoveClass move = Instantiate(possibleMove, new Vector2(arrayPos.x, arrayPos.y), Quaternion.identity, transform).GetComponent<PossibleMoveClass>();
                move.isAttack = true;
                move.arrayX = (int)arrayPos.x;
                move.arrayY = (int)arrayPos.y;
            }
        }
    }

    // use to check in directions for each piece that has linear movement functions
    public void IterateCheck(int xp, int yp)
    {
        // we are checking on the x and y axis
        if (xp != 0 && yp != 0)
        {
            int x = (int)arrayPos.x, y = (int)arrayPos.y;

            for (x = x + xp; x < 8 && x > -1; x += xp)
            {
                // check our y position...
                for (y = y + yp; y < 8 && y > -1; y += yp)
                {
                    // if there is nothing there...
                    if (boardManager.boardTiles[x, y].heldPiece == null)
                    {
                        Debug.Log("nothing at " + x + ", " + y);
                        // place a tile
                        PlacePossibleMoveTile(new Vector2(x, y));
                        break;
                    }

                    // if there is something there...
                    if (boardManager.boardTiles[x, y].heldPiece != null && boardManager.boardTiles[x, y].heldPiece != this)
                    {
                        Debug.Log("something at " + x + ", " + y);
                        // place a tile
                        PlacePossibleMoveTile(new Vector2(x, y));
                        // full return
                        return;
                    }
                }
            }
            return;
        }    

        // we are checking only the x axis
        if (xp != 0 && yp == 0)
        {
            int x = (int)arrayPos.x, y = (int)arrayPos.y;

            for (x = x + xp; x < 8 && x > -1; x += xp)
            {
                // check our y position...
                // if there is nothing there...
                if (boardManager.boardTiles[x, y].heldPiece == null)
                {
                    Debug.Log("nothing at " + x + ", " + y);
                    // place a tile
                    PlacePossibleMoveTile(new Vector2(x, y));
                }

                // if there is something there...
                if (boardManager.boardTiles[x, y].heldPiece != null && boardManager.boardTiles[x, y].heldPiece != this)
                {
                    Debug.Log("something at " + x + ", " + y);
                    // place a tile
                    PlacePossibleMoveTile(new Vector2(x, y));
                    // break
                    break;
                }
                
            }
            return;
        }

        // we are checking only the y axis
        if (xp == 0 && yp != 0)
        {
            int x = (int)arrayPos.x, y = (int)arrayPos.y;

            // check our y position...
            for (y = y + yp; y < 8 && y > -1; y += yp)
            {
                // if there is nothing there...
                if (boardManager.boardTiles[x, y].heldPiece == null)
                {
                    Debug.Log("nothing at " + x + ", " + y);
                    // place a tile
                    PlacePossibleMoveTile(new Vector2(x, y));
                }

                // if there is something there...
                if (boardManager.boardTiles[x, y].heldPiece != null && boardManager.boardTiles[x, y].heldPiece != this)
                {
                    Debug.Log("something at " + x + ", " + y);
                    // place a tile
                    PlacePossibleMoveTile(new Vector2(x, y));
                    // break
                    break;
                }
            }
            
            return;
        }
        
    }

    // invoked a moment after we click on something else, so that we can run a movement check
    public void ChildRemoval()
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);
    }

    // use this to perform a relative check to where the piece is
    public void DirectCheck(int xp, int yp)
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

    // called when we want to destroy this object
    public virtual void ManualDestroy()
    {
        // put ourselves into the correct deathzone
        if (isWhite) TurnManager.instance.whiteZone.AddPiece(whiteSprite);
        if (!isWhite) TurnManager.instance.blackZone.AddPiece(blackSprite);

        // then destroy this game object
        Destroy(gameObject);
    }
}
