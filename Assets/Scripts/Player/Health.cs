using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
public class Health : MonoBehaviour
{
    public float HP;
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
            if (isLocalPlayer)
                RoomManager.instance.SpawnPlayer();

            Destroy(gameObject);
        }
        HealthText.text = HP.ToString();
    }
}
