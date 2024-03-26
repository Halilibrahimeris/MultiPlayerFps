using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
public class Health : MonoBehaviour
{
    public float HP;
    public TextMeshProUGUI HealthText;
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
            //die
            Destroy(gameObject);
            Debug.Log(HP);
        }
        HealthText.text = HP.ToString();
    }
}
