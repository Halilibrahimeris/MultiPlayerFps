using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Bullet : MonoBehaviour
{
    public float damage;
    private float destroyTime = 2f;
    private float timer;
    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= destroyTime)
        {
            Destroy(gameObject);
        }
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
    }
}
