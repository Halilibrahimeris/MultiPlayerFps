using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro; 
public class PlayerSetUp : MonoBehaviour
{
    public CharacterController CharacterController;
    public CharacterMovement MoveScript;
    public GameObject Camera;
    public GameObject Arm;

    public string NickName;
    public TextMeshPro TextNickName;
    private void Start()
    {

    }
    public void IsLocalPlayer()
    {
        CharacterController.enabled = true;
        Arm.SetActive(false);
        MoveScript.enabled = true;
        Camera.SetActive(true);
    }

    [PunRPC]
    public void SetNickName(string _name)
    {
        NickName = _name;
        TextNickName.text = _name;
    }
}
