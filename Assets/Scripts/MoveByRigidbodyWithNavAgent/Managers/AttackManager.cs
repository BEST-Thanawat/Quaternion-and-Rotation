using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : Singleton<AttackManager>
{
    //Current attack
    public List<AttackInfo> AttackInfos = new List<AttackInfo>();

    public List<AttackInfo> CurrentAttacks = new List<AttackInfo>();
}
