using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Bullet : MonoBehaviour
{
    [HideInInspector]public float damage;

    private void Start()
    {
        Destroy(gameObject, 2f);
    }
    private void OnCollisionEnter(Collision collision)//�arpt��t��� durumu kontrol eden fonksiyon
    {
        IDamageable damageObject = collision.gameObject.GetComponent<IDamageable>();
        if(damageObject != null)
        {
            IDamageable.Type typ = damageObject.SetType();
            CreateBulletImpactEffect(collision, typ);
            Destroy(gameObject);
        }
        if (collision.transform.gameObject.GetComponent<Health>())
        {

            if (collision.gameObject.TryGetComponent<PhotonView>(out PhotonView player))
            {
                player.RPC("TakeDamage", RpcTarget.Others, damage);
                Debug.Log("Enemy Damage Yedi");
                Destroy(gameObject);
            }
        }
    }

    private void CreateBulletImpactEffect(Collision ObjectWeHit, IDamageable.Type type)//Merminin denk geldi�i yere mermi izi efekti ekleniyor
    {
        ContactPoint contact = ObjectWeHit.contacts[0];//temas etti�i nokta al�n�yor
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
        GameObject hole = PhotonNetwork.Instantiate(PublicVariable.instance.bulletImpactEffectPrefab[id].name, contact.point, Quaternion.LookRotation(contact.normal));//temas etti�i noktaya mermi izini spawnl�yor

        Destroy(hole, 2f); //mermi izi 2 saniye sonra siliniyor
    }
}
