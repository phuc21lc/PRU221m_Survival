using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteFactory : AbstractMonsterFtr
{
    public override IMelee CreateMelee()
    {
        return new EMelee();
    }

    public override IRanged CreateRanged()
    {
        return new ERanged();
    }
}
