using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem.XInput;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public FloatValue winPlayerNum;
    //GameObject Fade;
    Fade script;
    float time;
    private bool hasFadedOut = false;
    [SerializeField] public InputController inputController;


    private void Start()
    {
        AudioManager.instance.PlayBGM("MainBgm");
        //Fade = GameObject.Find("Fade");
        script = GetComponent<Fade>();
        inputController = GetComponent<InputController>();

    }

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
        if (winPlayerNum.runtimeValue != 0 && !hasFadedOut)
        {
            script.isFadeout = true;
            hasFadedOut = true;
            inputController.enabled = false;

        }

        //if (script.isFadeout) { Debug.Log("フェードアウトはtrueです"); }
        if (winPlayerNum.runtimeValue != 0)
        {
            time += Time.deltaTime;

            if (time >= 3.0f)
            {
                SceneManager.LoadScene("result");
                script.isFadeout = false;
                inputController.enabled = true;
                ;
                //pause.EnableInputController();
            }
        }

    }

}
