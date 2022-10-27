using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class MonsterSpawn : MonoBehaviour
{
    [Header("Monster Prefaps:")]
    [SerializeField]
    private GameObject MeeleeCreep;
    [SerializeField]
    private int numberOfMeeleeCreep;
    [SerializeField]
    private GameObject MeeleeCreepElite;
    [SerializeField]
    private int numberOfMeeleeCreepElite;
    [SerializeField]
    private GameObject RangedCreep;
    [SerializeField]
    private int numberOfRangedCreep;
    [SerializeField]
    private GameObject RangedCreepElite;
    [SerializeField]
    private int numberOfRangedCreepElite;

    [Header("Camera:")]
    [SerializeField]
    private Camera cameraPosition;
    private ObjectPool<CreepFactory> meleePools, rangedPools;
    private ObjectPool<EliteFactory> meleeElitePools, rangedElitePoo;

    private AbstractMonsterFtr _monsterFactory;
    private void Awake()
    {
        int random = Random.Range(0,2);
        if (random <=1)
        {
            _monsterFactory = new CreepFactory();
        }
        else
        {
            _monsterFactory = new EliteFactory();
        }
        meleePools = new ObjectPool<CreepFactory>();
        //var melee = _monsterFactory.CreateMelee();
        //var ranged = _monsterFactory.CreateRanged();
    }
}
