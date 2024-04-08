using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoRefresh : MonoBehaviour
{
    public Weapon weapon;

    public void Refresh(int refresh)
    {
        if(weapon!= null)
        {
            weapon.MagazineSize += refresh;
        }
    }
}
