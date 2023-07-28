using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    /**  
    <summary>
    ダメージを受ける処理
    </summary>
    */
    void TakeDamage(float damage);

    /**  
    <summary>
    死亡処理
    </summary>
    */
    void Death();
}
