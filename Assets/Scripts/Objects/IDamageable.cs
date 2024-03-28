using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public enum Type
    {
        Wood,
        Stone,
        Steal
    }
    public Type SetType();
}
