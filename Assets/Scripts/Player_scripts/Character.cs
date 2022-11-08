using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
    
    public int maxHp = 1000;
    public int currentHp = 1000;
    // Start is called before the first frame update
    [SerializeField]
    StatusBar hpBar;

    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }
    public void takeDamage(int damage)
    {
        currentHp -= damage;
        if (currentHp <= 0)
        {
            SceneManager.LoadScene("GameOverScene");
        }
        hpBar.SetState(currentHp, maxHp);
    }
}
