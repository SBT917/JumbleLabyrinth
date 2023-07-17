using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatus", menuName = "ScriptableObjects/PlayerStatus")]
public class PlayerStatus : ScriptableObject
{
    public float defaultMoveSpeed; //�f�t�H���g�̈ړ����x
    public float defaultAttackSpeed; //�f�t�H���g�̍U�����x
    public float defaultpower; //�f�t�H���g�̍U����
    public float defaultKnockForce; //�f�t�H���g�̃m�b�N�o�b�N��
}
