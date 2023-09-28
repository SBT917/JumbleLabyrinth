using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class AudioData
{
    public string name;
    public AudioClip clip;
    [Range(0, 1)] public float volume = 1;
}

public class AudioManager : Singleton<AudioManager> //�V���O���g���p��
{
    private AudioSource audioSource; //AudioSource
    [SerializeField] private List<AudioData> seDatas; //SE�̃f�[�^���X�g

    protected override void Awake()
    {
        TryGetComponent(out audioSource);
        base.Awake();
    }

    //�w�肵�����O��SE���Đ�
    public void PlaySE(string name)
    {
        if (audioSource == null) return;
        AudioData data = seDatas.Find(se => se.name == name);
        audioSource.volume = data.volume;
        audioSource.PlayOneShot(data.clip);
    }
}