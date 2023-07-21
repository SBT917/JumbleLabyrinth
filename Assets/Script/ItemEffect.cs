using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffect : MonoBehaviour
{
    public enum Effect
    {
        speedDown,
        sendEnemy,
        squid
    }
    public Effect effect;
    public int effectValue;
    public void applyEffect(PlayerController player)
    {
        //switch(effect) 
        //{
        //    case Effect.speedDown:
        //        player.speedDown(effectValue);
        //        break;
        //        case Effect.sendEnemy:

        //}
    }
}
