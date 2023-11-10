using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Ammo : MonoBehaviour
{
    public float ammoDamage;


    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this);
    }
}
