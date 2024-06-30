using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;
public class Chat : MonoBehaviour
{
    public bool inChat = false;

    public TMP_InputField inputField;
    public GameObject massage;
    public GameObject content;
    private void Update()
    {

        if (Input.GetKeyUp(KeyCode.KeypadEnter) && inChat != true)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            inputField.ActivateInputField();
            inChat = true;
        }
        else if(Input.GetKeyUp(KeyCode.KeypadEnter) && inChat)
        {
            SendMassage();
            inputField.text = "";
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            inChat = false;
        }   
    }
    public void SendMassage()
    {
        if (inputField.text.Length == 0)
            return;
        GetComponent<PhotonView>().RPC("GetMassage", RpcTarget.All, inputField.text);
        inputField.DeactivateInputField();
    }

    [PunRPC]
    public void GetMassage(string ReciveMassage)
    {
        GameObject Massage = Instantiate(massage, Vector3.zero, Quaternion.identity, content.transform);
        Massage.GetComponent<Massage>().MyMassage.text = ReciveMassage;
    }

}
