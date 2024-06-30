using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Pun.UtilityScripts;
public class Health : MonoBehaviour
{
    public float HP;
    public float hpScore;
    public TextMeshProUGUI HealthText;
    public bool isLocalPlayer;
    private void Start()
    {
        HealthText.text = HP.ToString();
    }

    [PunRPC]
    public void TakeDamage(float Damage)
    {
        HP -= Damage;
        if(HP<= 0)
        {
            PhotonNetwork.Destroy(gameObject);

            if (isLocalPlayer)
            {
                RoomManager.instance.SpawnPlayer();
                PhotonNetwork.LocalPlayer.AddDeathScore(1);
            }

        }
        HealthText.text = HP.ToString();
    }
    public void TakeDamageForScore(float Damage)
    {
        hpScore -= Damage;
    }
}
