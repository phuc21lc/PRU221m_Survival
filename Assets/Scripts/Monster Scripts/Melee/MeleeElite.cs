using Assets.Scripts.Monster_Scripts;
using Assets.Scripts.Monster_Scripts.Melee;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class MeleeElite : MonsterBase, IMelee
{
    //declaration this
    [SerializeField]
    private MonsterSetting _monsterSetting;
    private Sprite _sprite;
    private string _name;
    private float _hp, _attackDamage, _attackRange, _speed;
    private Action<MeleeElite> _action;
    private Animator animator;

    //declaration others
    private GameObject mainPlayer;
    private Transform player;
    private Character character;

    public void Init(Action<MeleeElite> action)
    {
        _action = action;
    }

    #region Get/set
    public override Sprite Sprite { get => _sprite; set => _sprite = value; }
    public override string Name { get => _name; set => _name = value; }
    public override float Hp { get => _hp; set => _hp = value; }
    public override float AttackDamage { get => _attackDamage; set => _attackDamage = value; }
    public override float AttackRange { get => _attackRange; set => _attackRange = value; }
    public override float Speed { get => _speed; set => _speed = value; }
    #endregion

    private void Awake()
    {
        #region Instantiate object
        _sprite = _monsterSetting.sprite;
        _name = _monsterSetting.name;
        _hp = _monsterSetting.hp;
        _attackDamage = _monsterSetting.attackDamage;
        _attackRange = _monsterSetting.attackRange;
        _speed = _monsterSetting.speed;

        animator = GetComponent<Animator>();
        gameObject.AddComponent<Rigidbody2D>();
        var rb = gameObject.GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.freezeRotation = true;
        gameObject.AddComponent<BoxCollider2D>();
        if (gameObject.GetComponent<SpriteRenderer>() == null)
        {
            gameObject.AddComponent<SpriteRenderer>();
        }
        //var sr = gameObject.GetComponent<SpriteRenderer>();
        //sr.sprite = Sprite;
        //sr.color = Color.red;
        gameObject.name = "Melee Elite";
        gameObject.tag = "Monster";
        #endregion

        mainPlayer = GameObject.FindGameObjectWithTag("Player");
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    //take damage from anything
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag.Equals("Bullet"))
        {
            TakeDamage(FindObjectOfType<Bullet>().dmg);
        }
    }
    //deal damage to player
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag.Equals("Player"))
        {
            Attack();
        }
    }
    private void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.transform.position, transform.position);
        Move(distanceFromPlayer);
    }
    //private void OnDestroy()
    //{
    //    Destroy(gameObject);
    //}
    public void TakeDamage(int damage)
    {
        Hp -= damage;
        if (Hp <= 0)
        {
            _action(this);
        }
    }
    public void Move(float distance)
    {
        //transform.position = Vector2.MoveTowards(this.transform.position, player.position, _speed * Time.deltaTime);
        if (distance >= AttackRange)
        {
            Vector3 des = (mainPlayer.transform.position - transform.position).normalized;
            var rb = gameObject.GetComponent<Rigidbody2D>().velocity = des * Speed;
            Vector3 direction = mainPlayer.transform.position - transform.position;
            if (direction.x < 0)
            {
                animator.SetBool("IsRight", false);
                animator.SetBool("IsLeft", true);
            }
            else
            {
                animator.SetBool("IsRight", true);
                animator.SetBool("IsLeft", false);
            }
        }
    }
    public void Attack()
    {
        character = mainPlayer.GetComponent<Character>();
        character.takeDamage((int)AttackDamage);
    }
}