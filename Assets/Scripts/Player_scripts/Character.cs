using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
    public static Character Instance { get; private set; }

    public int maxHp = 1000;
    public int currentHp = 1000;
    // Start is called before the first frame update
    [SerializeField]
    StatusBar hpBar;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        if(SaveGame.Instance.data != null)
        {
            currentHp = SaveGame.Instance.data.health;
            hpBar.SetState(currentHp, maxHp);
        }
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
    public void Heal(int heal)
    {
        currentHp += heal;
        if (currentHp > maxHp)
        {
            currentHp = maxHp;
        }
        hpBar.SetState(currentHp, maxHp);
    }
}
