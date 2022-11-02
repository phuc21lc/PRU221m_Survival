using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepFactory : AbstractMonsterFtr
{
    public override IMelee CreateMelee()
    {
        return new CreepMelees();
    }

    public override IRanged CreateRanged()
    {
        return new CreepRanged2();
    }
}
