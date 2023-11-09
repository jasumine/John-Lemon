using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBundle : MonoBehaviour
{
    public float ammoCount;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Gun playerGun = other.GetComponentInChildren<Gun>();

            playerGun.hasAmmo += ammoCount;

            Destroy(gameObject);
        }
    }
}
