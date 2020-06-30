using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    public PoolObjectType PoolObjectType;
    public void TurnOff()
    {
        PoolManager.Instance.AddObject(this);
    }
}
