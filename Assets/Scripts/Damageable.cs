using System;
using UnityEngine;

//СКРИПТ - Обьекты которые могут получать урон
public class Damageable : MonoBehaviour
{
    public Action<float> OnResiveDamage = delegate { };
    public Action<float> OnResiveBossDamage = delegate { };

    public void DoDamage(float damage)
    {
        OnResiveDamage(damage);
    }

    public void DoBossDamage(float damageBoss)
    {
        OnResiveBossDamage(damageBoss);
    }
}