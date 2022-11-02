using Assets.Scripts.Monster_Scripts.Melee;
using Assets.Scripts.Monster_Scripts.Ranged;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject meleeCreep, rangedCreep, meleeElite, rangedElite;
    private Camera cam;
    private Bounds bounds, outOfBounds;
    private Vector3 bottomLeft, bottomRight, topLeft, topRight;
    [SerializeField]
    private int numberOfMeleeCreep;
    [SerializeField]
    private int numberOfRangedCreep;
    [SerializeField]
    private int numberOfMeleeElite;
    [SerializeField]
    private int numberOfRangedElite;
    private int countMc, countRc, countMe, coundRe;

    private List<GameObject> meleeCreeps, rangedCreeps, meleeElites, rangedElites;

    private void Awake()
    {
        //meleeCreep = GameObject.Find("Melee Creep");
        //rangedCreep = GameObject.Find("Ranged Creep");
        //meleeElite = GameObject.Find("Melee Elite");
        //rangedElite = GameObject.Find("Ranged Elite");

        cam = Camera.main;
        bounds = OrthographicBounds(cam);
        outOfBounds = OutOfOrthographicBounds(cam);
        bottomLeft = new Vector3(cam.transform.position.x, 0, cam.transform.position.z);
        bottomRight = new Vector3(0, cam.transform.position.y, cam.transform.position.z);


        meleeCreeps = new List<GameObject>();
        rangedCreeps = new List<GameObject>();
        meleeElites = new List<GameObject>();
        rangedElites = new List<GameObject>();
    }
    private void Start()
    {
        meleeCreep.GetComponent<MeleeCreep>().Speed = 0;
        Debug.Log($"Bottom Left:{bottomLeft}");
        Debug.Log($"Bottom Right:{bottomRight}");
        Debug.Log($"{bounds.extents}");
        Debug.Log($"{outOfBounds.extents}");
        Spawn();
    }
    private void Update()
    {
    }
    private void Spawn()
    {
        //float height = cam.orthographicSize*2 + 1;
        //float width = cam.orthographicSize*2 * cam.aspect + 1;

        for (int i = 0; i < numberOfMeleeCreep; i++)
        {
            var go = Instantiate(meleeCreep, randomSpot(), Quaternion.identity);
            meleeCreeps.Add(go);
        }
        for (int i = 0; i < numberOfRangedCreep; i++)
        {
            var go = Instantiate(rangedCreep, randomSpot(), Quaternion.identity);
            meleeCreeps.Add(go);
        }
        for (int i = 0; i < numberOfMeleeElite; i++)
        {
            var go = Instantiate(meleeElite, randomSpot(), Quaternion.identity);
            meleeCreeps.Add(go);
        }
        for (int i = 0; i < numberOfRangedElite; i++)
        {
            var go = Instantiate(rangedElite, randomSpot(), Quaternion.identity);
            meleeCreeps.Add(go);
        }
        Debug.Log($"{randomSpot()}");
    }
    private Vector3 randomSpot()
    {
        float spawnSpotX = Random.Range(bounds.extents.x, outOfBounds.extents.x);
        float spawnSpotY = Random.Range(bounds.extents.y, outOfBounds.extents.y);
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
        new Vector3(cameraHeight * screenAspect + 2, cameraHeight + 1, 0));
        return bounds;
    }
}
