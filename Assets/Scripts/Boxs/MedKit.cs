using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKit : RefreshParent
{
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player") /*&& !isAmmo*/ && isActive)
        {
            var _player = other.GetComponent<Health>();
            _player.HP = 100;
            _player.HealthText.text = _player.HP.ToString();
            gameObject.SetActive(false);
            isActive = false;
        }
    }
}
