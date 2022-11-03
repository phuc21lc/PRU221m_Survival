using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Monster_Scripts.Melee
{
    public class MeleeCreep : MonsterBase, IMelee
    {
        [SerializeField]
        private MonsterSetting _monsterSetting;

        private Transform player;

        private Sprite _sprite;
        private string _name;
        private float _hp, _attackDamage, _attackRange, _speed;

        public MeleeCreep()
        {
        }
        private Action<MeleeCreep> _action;

        public void Init(Action<MeleeCreep> action)
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
            _sprite = _monsterSetting.sprite;
            _name = _monsterSetting.name;
            _hp = _monsterSetting.hp;
            _attackDamage = _monsterSetting.attackDamage;
            _attackRange = _monsterSetting.attackRange;
            _speed = _monsterSetting.speed;

            gameObject.AddComponent<Rigidbody2D>();
            var rb = gameObject.GetComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            gameObject.AddComponent<BoxCollider2D>();
            gameObject.AddComponent<SpriteRenderer>();
            var sr = gameObject.GetComponent<SpriteRenderer>();
            sr.sprite = Sprite;
            sr.color = Color.white;
            gameObject.name = "Melee Creep";
            gameObject.tag = "Monster";
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        private void Start()
        {

            Debug.Log($"Monster name: {_name}, Hp: {_hp}, Attack damage: {_attackDamage}, Attack range: {_attackRange}, Speed: {_speed}");
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (Hp <= 0)
            {
                _action(this);
            }
            if (collision.transform.tag.Equals("Player"))
            {
                //DEAL DAMAGE TO PLAYER
                //Destroy(gameObject);
                _action(this);
            }
        }
        private void Update()
        {
            //float distanceFromPlayer = Vector2.Distance(player.transform.position, transform.position);
            //if (distanceFromPlayer < AttackRange)
            //{
            Move();
            //}
        }
        private void OnDestroy()
        {
            Destroy(gameObject);
        }
        public void Move()
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, _speed * Time.deltaTime);
        }

        public void Attack()
        {

        }
    }
}