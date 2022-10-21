using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject GameObjectToSpawn;
    private GameObject Clone;
    public float timeToSpawn = 1f;
    public float FirstSpawn = 3f;
    void Start()
    {

    }
    void Spawn()
    {
        Vector2 randomPositionOnScreen = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y);
        FirstSpawn -= Time.deltaTime;
        if (FirstSpawn <= 0f)
        {
            Clone = Instantiate(GameObjectToSpawn, randomPositionOnScreen, Quaternion.identity);
            Clone.transform.position = randomPositionOnScreen;
            FirstSpawn = timeToSpawn;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Spawn();
    }
}
