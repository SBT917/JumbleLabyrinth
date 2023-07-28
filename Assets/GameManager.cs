using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

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

    private void Update()
    {
        if(winPlayerNum.runtimeValue != 0)
        {
            SceneManager.LoadScene("result");
        }
    }
}
