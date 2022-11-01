using Assets.Scripts.Monster_Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class MonsterSpawn : MonoBehaviour
{
    [Header("Creep:")]
    [SerializeField]
    private CreepMelees MeleeCreep;
    [SerializeField]
    private int numberOfMeleeCreep;
    [SerializeField]
    private CreepRanged2 RangedCreep;
    [SerializeField]
    private int numberOfRangedCreep;
    [Header("Elite:")]
    [SerializeField]
    private EMelee MeleeCreepElite;
    [SerializeField]
    private int numberOfMeleeCreepElite;
    [SerializeField]
    private ERanged RangedCreepElite;
    [SerializeField]
    private int numberOfRangedCreepElite;

    [Header("Camera:")]
    [SerializeField]
    private Camera cameraPosition;
    private IObjectPool<CreepMelees> meleeCreepPools;
    private IObjectPool<CreepRanged2> rangedCreepPools;
    private IObjectPool<EMelee> meleeElitePools;
    private IObjectPool<ERanged> rangedElitePools;
    private AbstractMonsterFtr _monsterFactory;

    private void Awake()
    {
        //create pool for creeps only
        _monsterFactory = new CreepFactory();
        var melee = _monsterFactory.CreateMelee();
        meleeCreepPools = new ObjectPool<CreepMelees>(
            () => { return Instantiate(MeleeCreep); },
            creep => { creep.gameObject.SetActive(true); },
            creep => { creep.gameObject.SetActive(false); },
            creep => { Destroy(creep.gameObject); },
            false,
            numberOfMeleeCreep,
            100
            );
        rangedCreepPools = new ObjectPool<CreepRanged2>(
            () => { return Instantiate(RangedCreep); },
            creep => { creep.gameObject.SetActive(true); },
            creep => { creep.gameObject.SetActive(false); },
            creep => { Destroy(creep.gameObject); },
            false,
            numberOfRangedCreep,
            100
            );


        _monsterFactory = new EliteFactory();
        meleeElitePools = new ObjectPool<EMelee>(
            () => { return Instantiate(MeleeCreepElite); },
            creep => { creep.gameObject.SetActive(true); },
            creep => { creep.gameObject.SetActive(false); },
            creep => { Destroy(creep.gameObject); },
            false,
            numberOfMeleeCreepElite,
            100
            );
        rangedElitePools = new ObjectPool<ERanged>(
            () => { return Instantiate(RangedCreepElite); },
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
    private void KillMeleeCreep(CreepMelees creep)
    {
        meleeCreepPools.Release(creep);
    }
    private void KillRangedCreep(CreepRanged2 creep)
    {
        rangedCreepPools.Release(creep);
    }
    private void KillMeleeElite(EMelee creep)
    {
        meleeElitePools.Release(creep);
    }
    private void KillRangedElite(ERanged creep)
    {
        rangedElitePools.Release(creep);
    }
}
