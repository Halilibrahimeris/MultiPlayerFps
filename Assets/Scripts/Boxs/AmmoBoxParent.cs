using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBoxParent : MonoBehaviour
{
    RefreshParent child;
    private float refreshTime = 10f;
    private float Timer;
    void Start()
    {
        child = GetComponentInChildren<RefreshParent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!child.isActive)
        {
            Timer += Time.deltaTime;
            if(Timer >= refreshTime)
            {
                child.SetActiveChild();
                Timer = 0f;
            }
        }
    }
}
