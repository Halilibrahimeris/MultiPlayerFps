using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublicVariable : MonoBehaviour
{
    public List<GameObject> bulletImpactEffectPrefab = new List<GameObject>();
    public Chat chat;
    static public PublicVariable instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
