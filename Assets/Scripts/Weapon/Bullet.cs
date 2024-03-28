using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Bullet : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 4f);
    }
    public GameObject[] bulletImpactEffectPrefab = new GameObject[3];
    public float damage;
    private void OnCollisionEnter(Collision collision)//çarptýþtýðý durumu kontrol eden fonksiyon
    {
        IDamageable damageObject = collision.gameObject.GetComponent<IDamageable>();
        if(damageObject != null)
        {
            IDamageable.Type typ = damageObject.SetType();
            CreateBulletImpactEffect(collision, typ);
        }
        if (collision.transform.gameObject.GetComponent<Health>())
        {
            if (collision.gameObject.TryGetComponent<PhotonView>(out PhotonView player))
            {
                player.RPC("TakeDamage", RpcTarget.All, damage);
                Debug.Log("Enemy Damage Yedi");
            }
        }
        Destroy(gameObject);
    }

    private void CreateBulletImpactEffect(Collision ObjectWeHit, IDamageable.Type type)//Merminin denk geldiði yere mermi izi efekti ekleniyor
    {
        ContactPoint contact = ObjectWeHit.contacts[0];//temas ettiði nokta alýnýyor
        if(type == IDamageable.Type.Steal)
        {
            CreateHole(contact, 0);
        }
        else if(type == IDamageable.Type.Wood)
        {
            CreateHole(contact, 1);
        }
        else
        {
            CreateHole(contact, 2);
        }
    }

    private void CreateHole(ContactPoint contact,int id)
    {
        GameObject hole = PhotonNetwork.Instantiate(bulletImpactEffectPrefab[id].name, contact.point, Quaternion.LookRotation(contact.normal));//temas ettiði noktaya mermi izini spawnlýyor

        Destroy(hole, 2f); //mermi izi 2 saniye sonra siliniyor
    }
}
