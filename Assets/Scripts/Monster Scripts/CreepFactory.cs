using Assets.Scripts.Monster_Scripts.Melee;
using Assets.Scripts.Monster_Scripts.Ranged;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepFactory : AbstractMonsterFtr
{
    public override IMelee CreateMelee()
    {
        return new MeleeCreep();
    }

    public override IRanged CreateRanged()
    {
        return new RangedCreep();
    }
}
