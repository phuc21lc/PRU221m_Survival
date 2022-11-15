using Assets.Scripts.Monster_Scripts.Melee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.TextCore.Text;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Monster_Scripts.Ranged
{
    public class RangedCreep : MonsterBase, IRanged
    {
        //declaration for this object
        [SerializeField]
        private MonsterSetting _monsterSetting;
        private Sprite _sprite;
        private string _name;
        private float _hp, _attackDamage, _attackRange, _speed;
        private Action<RangedCreep> _action;
        private Animator animator;

        [SerializeField]
        GameObject healPickUp;
        [SerializeField]
        [Range(0f, 1f)]
        float chance = 1f;
        //declaration others
        private GameObject mainPlayer;
        private Transform player;
        private Character character;
        private float attackTime;
        [SerializeField]
        private RangedBullet1 rangedBullet;
        [SerializeField]
        private GameObject rangedBulletParent;
        [SerializeField]
        private int numberOfRangedBullet1;
        private IObjectPool<RangedBullet1> rangedBulletPools;
        float bulletDuration;

        public void Init(Action<RangedCreep> action)
        {
            _action = action;
        }
        private void KillBullet(RangedBullet1 bullet1)
        {
            rangedBulletPools.Release(bullet1);
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
            rb.freezeRotation = true;
            rb.gravityScale = 0;
            gameObject.AddComponent<BoxCollider2D>();
            if (gameObject.GetComponent<SpriteRenderer>() == null)
            {
                gameObject.AddComponent<SpriteRenderer>();
            }
            //var sr = gameObject.GetComponent<SpriteRenderer>();
            //sr.sprite = Sprite;
            //gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            gameObject.name = "Ranged Creep";
            gameObject.tag = "Monster";
            #endregion
            #region Bullet Object Pooling
            rangedBulletPools = new ObjectPool<RangedBullet1>(() =>
            {
                return Instantiate(rangedBullet);
            },
            bullet =>
            {
                bullet.gameObject.SetActive(true);
            },
            bullet =>
            {
                bullet.gameObject.SetActive(false);
            },
            bullet =>
            {
                Destroy(bullet);
            },
            false,
            numberOfRangedBullet1,
            numberOfRangedBullet1 * 5
            );
            #endregion
            attackTime = 2;
            bulletDuration = 2;
            mainPlayer = GameObject.FindGameObjectWithTag("Player");
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void Start()
        {
        }

        private void Update()
        {
            float distanceFromPlayer = Vector3.Distance(player.position, gameObject.transform.position);

            if (distanceFromPlayer >= AttackRange)
            {
                //Vector3 des = (mainPlayer.transform.position - transform.position).normalized;
                //var rb = gameObject.GetComponent<Rigidbody2D>().velocity = des * Speed;
                Move(distanceFromPlayer);
            }
            else
            {
                attackTime -= Time.deltaTime;
                if (attackTime <= 0)
                {
                    Attack();
                    attackTime = 2;
                }
            }
        }

        //take damage from anything
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.tag.Equals("Bullet"))
            {
                TakeDamage(FindObjectOfType<Bullet>().dmg);
            }
        }

        //attack/deal damage to player
        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.transform.tag.Equals("Player"))
            {
                //_action(this);
            }
        }

        public void TakeDamage(int damage)
        {
            Hp -= damage;
            if (Hp <= 0)
            {
                _action(this);
                if (Random.value < chance)
                {
                    GameObject t = Instantiate(healPickUp, transform.position, Quaternion.identity);
                }
            }
        }
        public void Move(float distance)
        {
            //transform.position = Vector2.MoveTowards(this.transform.position, player.position, _speed * Time.deltaTime);
            //Vector3 des = (mainPlayer.transform.position - transform.position).normalized;
            //var rb = gameObject.GetComponent<Rigidbody2D>().velocity = des * Speed;
            transform.position = Vector2.MoveTowards(transform.position, player.position, _speed * Time.deltaTime);
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
            var rangedBullet = rangedBulletPools.Get();
            rangedBullet.transform.position = rangedBulletParent.transform.position;            
            rangedBullet.Init(KillBullet);
            character.takeDamage((int)AttackDamage);
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, AttackRange);
        }
    }
}