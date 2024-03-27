using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefreshAmmo : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if(other.CompareTag("Player"))
        {
            var camera = other.GetComponentInChildren<Camera>();
            var _player = camera.GetComponentInChildren<Weapon>();
            _player.MagazineSize += 120;
            Destroy(gameObject);
        }
    }

    private void Update()
    {

    }
}
