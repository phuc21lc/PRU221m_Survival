using Assets.Scripts.Monster_Scripts.Melee;
using Assets.Scripts.Monster_Scripts.Ranged;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [Header("Prefaps")]
    [SerializeField]
    private MeleeCreep meleeCreep;
    [SerializeField]
    private RangedCreep rangedCreep;
    [SerializeField]
    private MeleeElite meleeElite;
    [SerializeField]
    private RangedElite rangedElite;
    //[Header("Camera")]
    private GameObject camGO;
    private Camera cam;
    private Bounds bounds, outOfBounds;
    private Vector3 bottomLeft, bottomRight, topLeft, topRight;

    [Header("Number of monsters")]
    [SerializeField]
    private int numberOfMeleeCreep;
    [SerializeField]
    private int numberOfRangedCreep;
    [SerializeField]
    private int numberOfMeleeElite;
    [SerializeField]
    private int numberOfRangedElite;
    private int countMc, countRc, countMe, coundRe;
    [Header("Time spawn")]
    [SerializeField]
    private float spawnCreepTime;
    [SerializeField]
    private float spawnEliteTime;
    private float spawnCreepTimer;
    private float spawnEliteTimer;
    //private List<MeleeCreep> meleeCreeps;
    //private List<RangedCreep> rangedCreeps;
    //private List<MeleeElite> meleeElites;
    //private List<RangedElite> rangedElites;

    private IObjectPool<MeleeCreep> meleeCreepPools;
    private IObjectPool<RangedCreep> rangedCreepPools;
    private IObjectPool<MeleeElite> meleeElitePools;
    private IObjectPool<RangedElite> rangedElitePools;

    private AbstractMonsterFtr factory;
    private void Awake()
    {
        camGO = GameObject.FindGameObjectWithTag("MainCamera");
        cam = camGO.GetComponent<Camera>();
        factory = MonsterFactory.getMonsterType(MonsterType.Creep);
        IMelee melee = factory.CreateMelee();

        //meleeCreep = GameObject.Find("Melee Creep");
        //rangedCreep = GameObject.Find("Ranged Creep");
        //meleeElite = GameObject.Find("Melee Elite");
        //rangedElite = GameObject.Find("Ranged Elite");

        bounds = OrthographicBounds(cam);
        outOfBounds = OutOfOrthographicBounds(cam);
        //bottomLeft = new Vector3(cam.transform.position.x, 0, cam.transform.position.z);
        //bottomRight = new Vector3(0, cam.transform.position.y, cam.transform.position.z);


        //meleeCreeps = new List<MeleeCreep>();
        //rangedCreeps = new List<RangedCreep>();
        //meleeElites = new List<MeleeElite>();
        //rangedElites = new List<RangedElite>();

        meleeCreepPools = new ObjectPool<MeleeCreep>(() =>
        {
            return Instantiate(meleeCreep);
        },
        creep =>
        {
            creep.gameObject.SetActive(true);
        },
        creep =>
        {
            creep.gameObject.SetActive(false);
        },
        creep =>
        {
            Destroy(creep);
        },
        false,
        numberOfMeleeCreep,
        numberOfMeleeCreep * 5
        );

        rangedCreepPools = new ObjectPool<RangedCreep>(() =>
        {
            return Instantiate(rangedCreep);
        },
        creep =>
        {
            creep.gameObject.SetActive(true);
        },
        creep =>
        {
            creep.gameObject.SetActive(false);
        },
        creep =>
        {
            Destroy(creep);
        },
        false,
        numberOfRangedCreep,
        numberOfRangedCreep * 5
        );

        meleeElitePools = new ObjectPool<MeleeElite>(() =>
        {
            return Instantiate(meleeElite);
        },
        creep =>
        {
            creep.gameObject.SetActive(true);
        },
        creep =>
        {
            creep.gameObject.SetActive(false);
        },
        creep =>
        {
            Destroy(creep);
        },
        false,
        numberOfMeleeElite,
        numberOfMeleeElite * 5
        );

        rangedElitePools = new ObjectPool<RangedElite>(() =>
        {
            return Instantiate(rangedElite);
        },
        creep =>
        {
            creep.gameObject.SetActive(true);
        },
        creep =>
        {
            creep.gameObject.SetActive(false);
        },
        creep =>
        {
            Destroy(creep);
        },
        false,
        numberOfRangedElite,
        numberOfRangedElite * 5
        );
    }
    private void Start()
    {
        spawnCreepTimer = 0;
        spawnEliteTimer = 0;
        //TEST
        //meleeCreep.GetComponent<MeleeCreep>().Speed = 0;
        //Debug.Log($"Bottom Left:{bottomLeft}");
        //Debug.Log($"Bottom Right:{bottomRight}");
        //Debug.Log($"{bounds.extents}");
        //Debug.Log($"{outOfBounds.extents}");
        SpawnCreeps(bounds,outOfBounds);
        SpawnElite(bounds, outOfBounds);
        //END TEST
    }
    private void Update()
    {
        cam = camGO.GetComponent<Camera>();
        bounds = OrthographicBounds(cam);
        outOfBounds = OutOfOrthographicBounds(cam);
        spawnCreepTimer += Time.deltaTime;
        spawnEliteTimer += Time.deltaTime;
        //Spawn every 3 seconds
        if (spawnCreepTimer >= spawnCreepTime)
        {
            SpawnCreeps(bounds, outOfBounds);
            spawnCreepTimer = 0;
        }
        if (spawnEliteTimer >= spawnEliteTime)
        {
            SpawnElite(bounds, outOfBounds);
            spawnEliteTimer = 0;
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
    private void SpawnElite(Bounds bounds, Bounds outOfBounds)
    {
        for (int i = 0; i < numberOfMeleeElite / 2; i++)
        {
            var me = meleeElitePools.Get();
            me.transform.position = randomSpot(bounds, outOfBounds);
            me.Init(KillMeleeElite);
        }
        for (int i = 0; i < numberOfRangedElite / 2; i++)
        {
            var re = rangedElitePools.Get();
            re.transform.position = randomSpot(bounds, outOfBounds);
            re.Init(KillRangedElite);
        }
    }
    private void SpawnCreeps(Bounds bounds, Bounds outOfBounds)
    {
        for (int i = 0; i < numberOfMeleeCreep/2; i++)
        {
            var mc = meleeCreepPools.Get();
            mc.transform.position = randomSpot(bounds, outOfBounds);
            mc.Init(KillMeleeCreep);
        }
        for (int i = 0; i < numberOfRangedCreep / 2; i++)
        {
            var rc = rangedCreepPools.Get();
            rc.transform.position = randomSpot(bounds,outOfBounds);
            rc.Init(KillRangedCreep);
        }

        //float height = cam.orthographicSize*2 + 1;
        //float width = cam.orthographicSize*2 * cam.aspect + 1;

        //for (int i = 0; i < numberOfMeleeCreep; i++)
        //{
        //    var go = Instantiate(meleeCreep, randomSpot(), Quaternion.identity);
        //    meleeCreeps.Add(go);
        //}
        //for (int i = 0; i < numberOfRangedCreep; i++)
        //{
        //    var go = Instantiate(rangedCreep, randomSpot(), Quaternion.identity);
        //    rangedCreeps.Add(go);
        //}
        //for (int i = 0; i < numberOfMeleeElite; i++)
        //{
        //    var go = Instantiate(meleeElite, randomSpot(), Quaternion.identity);
        //    meleeElites.Add(go);
        //}
        //for (int i = 0; i < numberOfRangedElite; i++)
        //{
        //    var go = Instantiate(rangedElite, randomSpot(), Quaternion.identity);
        //    rangedElites.Add(go);
        //}
    }
    private Vector3 randomSpot(Bounds bounds, Bounds outOfBounds)
    {
        int random = Random.Range(0, 4);
        float spawnSpotX = 0, spawnSpotY = 0;
        switch (random)
        {
            case 0:
                spawnSpotX = Random.Range(bounds.extents.x, outOfBounds.extents.x);
                spawnSpotY = Random.Range(bounds.extents.y, outOfBounds.extents.y);
                break;
            case 1:
                spawnSpotX = Random.Range(-outOfBounds.extents.x, -bounds.extents.x);
                spawnSpotY = Random.Range(bounds.extents.y, outOfBounds.extents.y);
                break;
            case 2:
                spawnSpotX = Random.Range(bounds.extents.x, outOfBounds.extents.x);
                spawnSpotY = Random.Range(-outOfBounds.extents.y, -bounds.extents.y);
                break;
            case 3:
                spawnSpotX = Random.Range(-outOfBounds.extents.x, -bounds.extents.x);
                spawnSpotY = Random.Range(-outOfBounds.extents.y, -bounds.extents.y);
                break;
            default:
                break;
        }
        Vector3 spawnSpot = new Vector3(spawnSpotX, spawnSpotY, 0);
        //spawnSpot = Camera.main.ViewportToWorldPoint(spawnSpot);
        return spawnSpot;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(outOfBounds.center, outOfBounds.size);
    }
    private Bounds OrthographicBounds(Camera camera)
    {
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = camera.orthographicSize * 2;
        Bounds bounds = new Bounds(
        camera.transform.position,
        new Vector3(cameraHeight * screenAspect, cameraHeight, 0));
        return bounds;
    }
    private Bounds OutOfOrthographicBounds(Camera camera)
    {
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = camera.orthographicSize * 2;
        Bounds bounds = new Bounds(
        camera.transform.position,
        new Vector3(cameraHeight * screenAspect + 4, cameraHeight + 2, 0));
        return bounds;
    }
}
