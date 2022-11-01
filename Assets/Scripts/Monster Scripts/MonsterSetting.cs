using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Monster Setting")]
public class MonsterSetting : ScriptableObject
{
    public string name;
    public Sprite sprite;
    public float hp;
    public float attackDamage;
    public float attackRange;
    public float speed;
}
