using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;

public class pause : MonoBehaviour
{
    public float disableDuration = 2.0f; // ���������鎞�ԁi�b�j
    public InputController inputController; // InputController�X�N���v�g���i�[����ϐ�
    private bool isDisabled = false; // �X�N���v�g������������Ă��邩�ǂ���

    private void Start()
    {
        // InputController�X�N���v�g���A�^�b�`���ꂽ�Q�[���I�u�W�F�N�g��������
        inputController = GetComponent<InputController>();

        if (inputController == null)
        {
            //Debug.LogError("InputController���A�^�b�`���ꂽ�Q�[���I�u�W�F�N�g��������܂���B");
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
            // ���������̃J�E���g��i�߂�
            disableDuration -= Time.deltaTime;

            if (disableDuration <= 0)
            {
                // �w�肵�����Ԃ��o�߂�����InputController��L��������
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