using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    // our instance
    public static BoardManager instance;
    private void Awake() => instance = this;

    // our array to to build the board
    public TileClass[,] boardTiles = new TileClass[8,8];

    // our list of pieces, set in inspector
    [SerializeField] List<PieceClass> pieceClasses = new List<PieceClass>();
    // using an enum to help us keep track of the pieces in the list
    enum pieces
    {
        pawn, king, queen, bishop, knight, rook
    }

    // on our start method, build the board
    private void Start()
    {
        // place the tiles in the world
        PlaceTileClasses();
        // place the pieces on the board
    }

    // place our tiles in the world
    void PlaceTileClasses()
    {
        // change our tileclass positions based on their positions in the array, and set their array positions
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                // add a new tile to the array
                GameObject tile = new GameObject();
                boardTiles[x,y] = tile.AddComponent<TileClass>();
                boardTiles[x, y].arrayPos = new Vector2(x, y);
            }
        }
    }

    // place our pieces on the board manually
    void PlacePieces()
    {

    }

    void PlacePiece(Vector2 arrayPos, pieces piece, bool white)
    {

    }

}
