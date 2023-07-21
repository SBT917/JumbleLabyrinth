using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public FloatValue winPlayerNum;

    public void WinPlayer1()
    {
        winPlayerNum.runtimeValue = 1;
    }

    public void WinPlayer2()
    {
        winPlayerNum.runtimeValue = 2;
    }
}
