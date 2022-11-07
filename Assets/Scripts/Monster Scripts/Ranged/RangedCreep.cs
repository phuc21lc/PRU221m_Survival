using Assets.Scripts.Monster_Scripts.Melee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace Assets.Scripts.Monster_Scripts.Ranged
{
    public class RangedCreep : MonsterBase, IRanged
    {
        [SerializeField]
        private MonsterSetting _monsterSetting;
        private Animator animator;

        private Transform player;

        private Sprite _sprite;
        private string _name;
        private float _hp, _attackDamage, _attackRange, _speed;
        private GameObject mainPlayer;
        private Character character;
        public RangedCreep()
        {
        }
        private Action<RangedCreep> _action;

        public void Init(Action<RangedCreep> action)
        {
            _action = action;
        }
        public override Sprite Sprite { get => _sprite; set => _sprite = value; }
        public override string Name { get => _name; set => _name = value; }
        public override float Hp { get => _hp; set => _hp = value; }
        public override float AttackDamage { get => _attackDamage; set => _attackDamage = value; }
        public override float AttackRange { get => _attackRange; set => _attackRange = value; }
        public override float Speed { get => _speed; set => _speed = value; }

        private void Awake()
        {
            mainPlayer = GameObject.FindGameObjectWithTag("Player");
            animator = GetComponent<Animator>();
            _sprite = _monsterSetting.sprite;
            _name = _monsterSetting.name;
            _hp = _monsterSetting.hp;
            _attackDamage = _monsterSetting.attackDamage;
            _attackRange = _monsterSetting.attackRange;
            _speed = _monsterSetting.speed;

            gameObject.AddComponent<Rigidbody2D>();
            var rb = gameObject.GetComponent<Rigidbody2D>();
            rb.freezeRotation = true;
            rb.gravityScale = 0;
            gameObject.AddComponent<BoxCollider2D>();
            gameObject.AddComponent<SpriteRenderer>();
            var sr = gameObject.GetComponent<SpriteRenderer>();
            sr.sprite = Sprite;
            //gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            player = GameObject.FindGameObjectWithTag("Player").transform;
            gameObject.name = "Ranged Creep";
            gameObject.tag = "Monster";
        }
        private void Start()
        {
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
            if (distanceFromPlayer >= AttackRange)
            {
                Move();
            }
        }
        private void OnDestroy()
        {
            Destroy(gameObject);
        }
        public void TakeDamage(int damage)
        {
            Hp -= damage;
            if (Hp <= 0)
            {
                _action(this);
            }
        }
        public void Move()
        {
            //transform.position = Vector2.MoveTowards(this.transform.position, player.position, _speed * Time.deltaTime);
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

        public void Attack()
        {
            //mainPlayer = GameObject.FindGameObjectWithTag("Player");
            character = mainPlayer.GetComponent<Character>();
            character.takeDamage((int)AttackDamage);
        }
    }
}