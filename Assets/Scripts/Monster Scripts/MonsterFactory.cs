using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFactory
{
    private MonsterFactory()
    {

    }
    public static AbstractMonsterFtr getMonsterType(MonsterType type)
    {
        switch (type)
        {
            case MonsterType.Creep:
                return new CreepFactory();
            case MonsterType.Elite:
                return new EliteFactory();
            default:
                throw new System.Exception("Dont found that type nor available!");
        }
    }
}
public enum MonsterType
{
    Creep,
    Elite
}
