using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBoxParent : MonoBehaviour
{
    RefreshAmmo child;
    private float refreshTime = 10f;
    private float Timer;
    void Start()
    {
        child = GetComponentInChildren<RefreshAmmo>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!child.isActive)
        {
            Timer += Time.deltaTime;
            if(Timer >= refreshTime)
            {
                child.isActive = true;
                child.gameObject.SetActive(true);
                Timer = 0f;
            }
        }
    }
}
