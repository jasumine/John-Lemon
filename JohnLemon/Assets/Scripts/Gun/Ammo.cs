using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Ammo : MonoBehaviour
{
    public GameObject ammoPrefab;
    public Transform ammoPos;
    public float ammoPower;

    //private Vector3 firePos= new Vector3(ammoPos.transform.position.x, ammoPos.position.y, ammoPos.position.z);

 

    private void Update()
    {
        FireAmmo();
    }
    void FireAmmo()
    {
        if(Input.GetMouseButton(0))
        {
           
            //GameObject ammo = Instantiate(ammoPrefab,firePos);

            //Rigidbody ammoRigid = ammo.GetComponent<Rigidbody>();

            //ammoRigid.AddForce(ammoRigid.transform.up * ammoPower, ForceMode.Impulse);
            
        }
    }



}
