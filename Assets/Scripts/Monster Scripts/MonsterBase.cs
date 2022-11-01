using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Monster_Scripts
{    
    public abstract class MonsterBase : MonoBehaviour
    {
        private float hp;
        private float attackDamage;
        private float attackRange;

        public MonsterBase()
        {
        }

        public MonsterBase(float hp, float attackDamage, float attackRange)
        {
            this.hp = hp;
            this.attackDamage = attackDamage;
            this.attackRange = attackRange;
        }

        public float Hp { get => hp; set => hp = value; }
        public float AttackDamage { get => attackDamage; set => attackDamage = value; }
        public float AttackRange { get => attackRange; set => attackRange = value; }

    }
}
