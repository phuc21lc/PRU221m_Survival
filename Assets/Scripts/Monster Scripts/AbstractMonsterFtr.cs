using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractMonsterFtr
{
    public abstract IMelee CreateMelee();
    public abstract IRanged CreateRanged();
}
