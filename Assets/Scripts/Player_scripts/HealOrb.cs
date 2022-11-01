using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealOrb : MonoBehaviour
{
    [SerializeField]
    int heal = 100;
    Character targetCharacter;
    GameObject targetGameObj;
    // Start is called before the first frame update
    void Start()
    {
        targetGameObj = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag.Equals("Player"))
        {
            Heal();
            Destroy(this.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void Heal()
    {
        if (targetCharacter == null)
        {
            targetCharacter = targetGameObj.GetComponent<Character>();
        }
        targetCharacter.heal(heal);
    }
}
