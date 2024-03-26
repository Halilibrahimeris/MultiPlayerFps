using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetUp : MonoBehaviour
{
    public Movement MoveScript;

    public GameObject Camera;

    public void IsLocalPlayer()
    {
        MoveScript.enabled = true;
        Camera.SetActive(true);
    }
}
