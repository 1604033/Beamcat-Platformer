using MoreMountains.CorgiEngine;
using MoreMountains.Tools;
using UnityEngine;

public class CustomDamageOnTouch : DamageOnTouch
{
    // Create a serializable class to store enemy types and their corresponding damage
    [System.Serializable]
    public class EnemyDamage
    {
        public string enemyTag;
        public int damage;
    }

    // Create an array of EnemyDamage to set up in the inspector
    public EnemyDamage[] enemyDamages;

}