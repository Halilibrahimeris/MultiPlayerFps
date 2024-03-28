using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objects : MonoBehaviour, IDamageable
{
    public IDamageable.Type type;
    public IDamageable.Type SetType()
    {
        if(type == IDamageable.Type.Steal)
        {
            return IDamageable.Type.Steal;
        }
        else if(type == IDamageable.Type.Wood)
        {
            return IDamageable.Type.Wood;
        }
        else
        {
            return IDamageable.Type.Stone;
        }
    }
}
