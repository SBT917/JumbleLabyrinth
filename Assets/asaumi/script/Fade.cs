using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public bool isFadeout = false;

    [SerializeField]
    Image fade;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Color_FadeIn");
    }
    IEnumerator Color_FadeIn()
    {
       // Image fade=GetComponent<Image>();
        fade.color = new Color((0.0f / 255.0f), (0.0f / 255.0f), (0.0f / 0.0f), (255.0f / 255.0f));

        // �t�F�[�h�C���ɂ����鎞��
        const float fade_time = 2.0f;

        // ���[�v��
        const int loop_count = 10;

        // �E�F�C�g���ԎZ�o
        float wait_time = fade_time / loop_count;

        // �F�̊Ԋu���Z�o 
        float alpha_interval = 255.0f / loop_count;

        // �F�����X�ɕς��郋�[�v
        for (float alpha = 255.0f; alpha >= 0.0f; alpha -= alpha_interval)
        {
            // �҂�����
            yield return new WaitForSeconds(wait_time);

            // Alpha�l��������������
            Color new_color = fade.color;
            new_color.a = alpha / 255.0f;
            fade.color = new_color;
        }
    }
    IEnumerator Color_FadeOut()
    {
        //Image fade=GetComponent<Image>();   
        // �t�F�[�h���̐F��ݒ�
        fade.color = new Color((0.0f / 255.0f), (0.0f / 255.0f), (0.0f / 0.0f), (0.0f / 255.0f));

        // �t�F�[�h�A�E�g�ɂ����鎞��
        const float fade_time = 2.0f;

        // ���[�v��
        const int loop_count = 10;

        // �E�F�C�g���ԎZ�o
        float wait_time = fade_time / loop_count;

        // �F�̊Ԋu���Z�o
        float alpha_interval = 1.0f / loop_count;

        // �F�����X�ɕς��郋�[�v
        for (float alpha = 0.0f; alpha <= 2.0f; alpha += alpha_interval)
        {
            // �҂�����
            yield return new WaitForSeconds(wait_time);

            // Alpha�l���������グ��
            Color new_color = fade.color;
            new_color.a = alpha;
            fade.color = new_color;
        }
    }
    private void Update()
    {
        if(isFadeout)
        {
            isFadeout = false;
            StartCoroutine("Color_FadeOut");
        }
    }
}

