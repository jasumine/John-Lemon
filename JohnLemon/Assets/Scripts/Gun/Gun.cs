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
   
        // firePos에 ammo를 생성
        GameObject ammo = Instantiate(ammoPrefab, firePos, ammoPrefab.transform.rotation);

        // ammo가 앞으로 나아가도록 힘을 준다.
        Rigidbody ammoRigid = ammo.GetComponent<Rigidbody>();
        ammoRigid.AddRelativeForce(ammoPos.transform.forward * ammoPower, ForceMode.Impulse); // 캐릭터가 앞을 보는 방향으로 가도록

        // 발사 쿨타임을 설정
        yield return new WaitForSeconds(fireDelay);
        isFire = true;

        // 3초뒤에 발사된 ammo가 삭제된다.
        yield return new WaitForSeconds(3f);
        Destroy(ammo);
    }


    private void CalculateAmmo()
    {
        isReload = true;

        // max와 currnent의 차이(temp)를 구해서
        // has에서 그 차이(temp)만큼을 빼준다.
        // 장전 되었기 때문에, '현재 장전된 개수'를 '최대 장전 개수'와 맞추어준다.
        // has가 temp보다 작을 경우, has는 0이 되어 더이상 장전을 할 수 없게 되며, 
        // current에는 has만큼 더해준다.
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

        // 장전에 걸리는 시간을 설정해준다.
        Invoke("ReloadFalse", reloadDelay);
    }

    private void ReloadFalse()
    {
        isReload = false;
    }

}
