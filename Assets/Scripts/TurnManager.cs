using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    public static TurnManager instance;
    private void Awake()
    {
        instance = this;
    }

    public Deathzone whiteZone, blackZone; // stored here for access when a piece dies
    [SerializeField] GameObject whiteKing, blackKing; // our kings

    [SerializeField] List<Text> displayText = new List<Text>();

    public Text log; // our log of play

    public bool isWhiteTurn = true, victory = false; // is it white's turn?

    public void ChangeTurn()
    {


        isWhiteTurn = !isWhiteTurn;

        if (isWhiteTurn)
        {
            foreach (Text text in displayText)
            {
                text.text = "White's Turn";
                text.color = Color.white;
            }
        }

        if (!isWhiteTurn)
        {
            foreach (Text text in displayText)
            {
                text.text = "Black's Turn";
                text.color = Color.black;
            }
        }
    }

    public void Victory()
    {
        StartCoroutine(Win());
    }

    public IEnumerator Win()
    {
        yield return new WaitForFixedUpdate();
        victory = true;

        if (blackKing == null)
        {
            foreach (Text text in displayText)
            {
                text.text = "!!WHITE WINS!!";
                text.color = Color.white;
            }
        }

        if (whiteKing == null)
        {
            foreach (Text text in displayText)
            {
                text.text = "!!BLACK WINS!!";
                text.color = Color.black;
            }
        }
    }
}
