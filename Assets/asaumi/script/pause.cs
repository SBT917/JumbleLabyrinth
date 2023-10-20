using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;

public class pause : MonoBehaviour
{
    public float disableDuration = 2.0f; // 無効化する時間（秒）
    public InputController inputController; // InputControllerスクリプトを格納する変数
    private bool isDisabled = false; // スクリプトが無効化されているかどうか

    private void Start()
    {
        // InputControllerスクリプトがアタッチされたゲームオブジェクトを見つける
        inputController = GetComponent<InputController>();

        if (inputController == null)
        {
            //Debug.LogError("InputControllerがアタッチされたゲームオブジェクトが見つかりません。");
        }
        else
        {
            DisableInputController();
        }
    }

    private void Update()
    {
        if (isDisabled)
        {
            // 無効化中のカウントを進める
            disableDuration -= Time.deltaTime;

            if (disableDuration <= 0)
            {
                // 指定した時間が経過したらInputControllerを有効化する
                EnableInputController();
            }
        }
    }

   public void DisableInputController()
    {
        inputController.enabled = false;
        isDisabled = true;
    }

    public void EnableInputController()
    {
        inputController.enabled = true;
        isDisabled = false;
    }
}