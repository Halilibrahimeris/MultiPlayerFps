using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetUp : MonoBehaviour
{
    public Movement MoveScript;
    public Health HealthScript;
    public GameObject Camera;
    public GameObject Arm;

    public void IsLocalPlayer()
    {
        Arm.SetActive(false);
        MoveScript.enabled = true;
        Camera.SetActive(true);
        HealthScript.isLocalPlayer = true;
    }
}
