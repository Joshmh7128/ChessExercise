using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deathzone : MonoBehaviour
{
    // this exists to manage the objects which are killed by players
    public void AddPiece(Sprite spr)
    {
        Instantiate(new GameObject(), transform).AddComponent<Image>().sprite = spr;
    }
}
