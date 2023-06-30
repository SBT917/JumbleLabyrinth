using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatus", menuName = "ScriptableObjects/PlayerStatus")]
public class PlayerStatus : ScriptableObject
{
    public float defaultMoveSpeed; //デフォルトの移動速度
    public float defaultpower; //デフォルトの攻撃力
}
