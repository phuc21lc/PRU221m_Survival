using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractMonsterFactory
{
    public abstract ICreep CreateCreep();
    public abstract IElite CreateElite();
}
