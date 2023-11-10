using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.UI.Image;

public class Gun : MonoBehaviour
{
    public float hasAmmo;
    public float maxAmmo;
    public float currentAmmo;

    public GameObject ammoPrefab;
    public Transform ammoPos;
    public float ammoPower;
    public float fireDelay;
    public float reloadDelay;

    private bool isFire = true;
    private bool isReload = false;

    private void Start()
    {
        currentAmmo = maxAmmo;
    }


    private void Update()
    {

        FireAmmo();
    }
    


    void FireAmmo()
    {
        if(Input.GetKeyUp(KeyCode.R) && isFire && !isReload)
        {
            CalculateAmmo();
        }

        if (Input.GetMouseButtonDown(0) && isFire && !isReload && currentAmmo>0)
        {
            StartCoroutine(InstantiateAmmo()); 
        }
    }

    IEnumerator InstantiateAmmo()
    {
        isFire = false;
        currentAmmo--;

        Vector3 firePos = new Vector3(ammoPos.position.x, ammoPos.position.y, ammoPos.position.z);
   
        // firePos�� ammo�� ����
        GameObject ammo = Instantiate(ammoPrefab, firePos, ammoPrefab.transform.rotation);

        // ammo�� ������ ���ư����� ���� �ش�.
        Rigidbody ammoRigid = ammo.GetComponent<Rigidbody>();
        ammoRigid.AddRelativeForce(ammoPos.transform.forward * ammoPower, ForceMode.Impulse); // ĳ���Ͱ� ���� ���� �������� ������

        // �߻� ��Ÿ���� ����
        yield return new WaitForSeconds(fireDelay);
        isFire = true;

        // 3�ʵڿ� �߻�� ammo�� �����ȴ�.
        yield return new WaitForSeconds(3f);
        Destroy(ammo);
    }


    private void CalculateAmmo()
    {
        isReload = true;

        // max�� currnent�� ����(temp)�� ���ؼ�
        // has���� �� ����(temp)��ŭ�� ���ش�.
        // ���� �Ǿ��� ������, '���� ������ ����'�� '�ִ� ���� ����'�� ���߾��ش�.
        // has�� temp���� ���� ���, has�� 0�� �Ǿ� ���̻� ������ �� �� ���� �Ǹ�, 
        // current���� has��ŭ �����ش�.
        float temp = maxAmmo - currentAmmo;
        if(hasAmmo < temp)
        {
            currentAmmo += hasAmmo;
            hasAmmo = 0;
        }
        else
        {
            hasAmmo -= temp;
            currentAmmo = maxAmmo;
        }

        // ������ �ɸ��� �ð��� �������ش�.
        Invoke("ReloadFalse", reloadDelay);
    }

    private void ReloadFalse()
    {
        isReload = false;
    }

}
