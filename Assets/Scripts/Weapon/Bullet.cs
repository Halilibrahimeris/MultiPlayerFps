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
        Destroy(gameObject, 3f);
    }
    /*private void OnTriggerEnter(Collider other)
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
    }*/
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Envoriment"))
        {
            print("hit :" + collision.gameObject.name + "!");
            CreateBulletImpactEffect(collision);
            Destroy(gameObject);
        }
        if (collision.transform.gameObject.GetComponent<Health>())
        {
            if (collision.gameObject.TryGetComponent<PhotonView>(out PhotonView player))
            {
                player.RPC("TakeDamage", RpcTarget.All, damage);
                Debug.Log("Enemy Damage Yedi");
            }
        }
    }

    void DestroyObject(GameObject objects)
    {
        Destroy(objects, 4f);
    }

    private void CreateBulletImpactEffect(Collision ObjectWeHit)//Merminin denk geldiði yere mermi izi efekti ekleniyor
    {
        ContactPoint contact = ObjectWeHit.contacts[0];//temas ettiði nokta alýnýyor

        GameObject hole = PhotonNetwork.Instantiate(bulleteffect.name, contact.point, Quaternion.LookRotation(contact.normal));//temas ettiði noktaya mermi izini spawnlýyor

        Destroy(hole, 2f); //mermi izi 2 saniye sonra siliniyor

        hole.transform.SetParent(ObjectWeHit.gameObject.transform);//mermi izinin parenti temas ettiði nokta yapýlýyor
    }
}
