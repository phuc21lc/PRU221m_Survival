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
        public abstract Sprite Sprite { get; set; }
        public abstract string Name { get; set; }
        public abstract float Hp { get; set; }
        public abstract float AttackDamage { get; set; }
        public abstract float AttackRange { get; set; }
        public abstract float Speed { get; set; }
    }
}
