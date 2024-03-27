using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Bullet : MonoBehaviour
{
    [SerializeField]private GameObject bulleteffect;
    [Space]
    public float damage;
    private void Start()
    {
        DestroyObject(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.gameObject.GetComponent<Health>())
        {
            if (other.TryGetComponent<PhotonView>(out PhotonView player))
            {
                player.RPC("TakeDamage", RpcTarget.All, damage);
                Debug.Log("Enemy Damage Yedi");
            }
        }
        else if(other.gameObject != null)
        {
            var effect = PhotonNetwork.Instantiate(bulleteffect.name, other.transform.position, Quaternion.identity);
            effect.transform.SetParent(other.transform);
            Debug.Log(other.name + ": buraya çarptý");
        }
    }

    void DestroyObject(GameObject objects)
    {
        Destroy(objects, 2f);
    }
}
