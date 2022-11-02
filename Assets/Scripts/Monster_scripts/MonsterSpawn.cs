using Assets.Scripts.Monster_Scripts;
using Assets.Scripts.Monster_Scripts.Melee;
using Assets.Scripts.Monster_Scripts.Ranged;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class MonsterSpawn : MonoBehaviour
{
    [Header("Creep:")]
    [SerializeField]
    private MeleeCreep meleeCreep;
    [SerializeField]
    private int numberOfMeleeCreep;
    [SerializeField]
    private RangedCreep rangedCreep;
    [SerializeField]
    private int numberOfRangedCreep;
    [Header("Elite:")]
    [SerializeField]
    private MeleeElite meleeElite;
    [SerializeField]
    private int numberOfMeleeCreepElite;
    [SerializeField]
    private RangedElite rangeElite;
    [SerializeField]
    private int numberOfRangedCreepElite;

    [Header("Camera:")]
    [SerializeField]
    private Camera cameraPosition;
    private IObjectPool<MeleeCreep> meleeCreepPools;
    private IObjectPool<RangedCreep> rangedCreepPools;
    private IObjectPool<MeleeElite> meleeElitePools;
    private IObjectPool<RangedElite> rangedElitePools;
    private AbstractMonsterFtr _monsterFactory;

    private void Awake()
    {
        //create pool for creeps only
        _monsterFactory = new CreepFactory();
        var melee = _monsterFactory.CreateMelee();
        meleeCreepPools = new ObjectPool<MeleeCreep>(
            () => { return Instantiate(meleeCreep); },
            creep => { creep.gameObject.SetActive(true); },
            creep => { creep.gameObject.SetActive(false); },
            creep => { Destroy(creep.gameObject); },
            false,
            numberOfMeleeCreep,
            100
            );
        rangedCreepPools = new ObjectPool<RangedCreep>(
            () => { return Instantiate(rangedCreep); },
            creep => { creep.gameObject.SetActive(true); },
            creep => { creep.gameObject.SetActive(false); },
            creep => { Destroy(creep.gameObject); },
            false,
            numberOfRangedCreep,
            100
            );


        _monsterFactory = new EliteFactory();
        meleeElitePools = new ObjectPool<MeleeElite>(
            () => { return Instantiate(meleeElite); },
            creep => { creep.gameObject.SetActive(true); },
            creep => { creep.gameObject.SetActive(false); },
            creep => { Destroy(creep.gameObject); },
            false,
            numberOfMeleeCreepElite,
            100
            );
        rangedElitePools = new ObjectPool<RangedElite>(
            () => { return Instantiate(rangeElite); },
            creep => { creep.gameObject.SetActive(true); },
            creep => { creep.gameObject.SetActive(false); },
            creep => { Destroy(creep.gameObject); },
            false,
            numberOfRangedCreepElite,
            100
            );
        Spawn();

        //InvokeRepeating("Spawn",0.5f,0.5f);
        //var melee = _monsterFactory.CreateMelee();
        //var ranged = _monsterFactory.CreateRanged();
    }
    private void Spawn()
    {
        for (int i = 0; i < numberOfMeleeCreep; i++)
        {
            var creep = meleeCreepPools.Get();
            creep.transform.position = transform.position + Random.insideUnitSphere * 10;
            creep.Init(KillMeleeCreep);
        }
        for (int i = 0; i < numberOfRangedCreep; i++)
        {
            var creep = rangedCreepPools.Get();
            creep.transform.position = transform.position + Random.insideUnitSphere * 10;
            creep.Init(KillRangedCreep);
        }
        for (int i = 0; i < numberOfMeleeCreepElite; i++)
        {
            var creep = meleeElitePools.Get();
            creep.transform.position = transform.position + Random.insideUnitSphere * 10;
            creep.Init(KillMeleeElite);
        }
        for (int i = 0; i < numberOfRangedCreepElite; i++)
        {
            var creep = rangedElitePools.Get();
            creep.transform.position = transform.position + Random.insideUnitSphere * 10;
            creep.Init(KillRangedElite);
        }
    }
    private void KillMeleeCreep(MeleeCreep creep)
    {
        meleeCreepPools.Release(creep);
    }
    private void KillRangedCreep(RangedCreep creep)
    {
        rangedCreepPools.Release(creep);
    }
    private void KillMeleeElite(MeleeElite creep)
    {
        meleeElitePools.Release(creep);
    }
    private void KillRangedElite(RangedElite creep)
    {
        rangedElitePools.Release(creep);
    }
}
