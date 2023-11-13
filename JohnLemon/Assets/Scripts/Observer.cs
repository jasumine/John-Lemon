using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public Transform player;
    public GameEnding gameEnding;
    bool m_IsPlayerInRange;
    bool isAtkBlock;

    private void Update()
    {
        if(m_IsPlayerInRange)
        {
            if(isAtkBlock == false)
            {
                Vector3 direction = player.position - transform.position + Vector3.up;
                Ray ray = new Ray(transform.position, direction);
                RaycastHit raycastHit;
                if (Physics.Raycast(ray, out raycastHit))
                {
                    if (raycastHit.collider.transform == player)
                    {
                        gameEnding.CaughtPlayer();
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform ==player)
        {
            m_IsPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.transform ==player)
        {
            m_IsPlayerInRange = false;
        }
    }

    public void IsAtkBlockTrue()
    {
        isAtkBlock = true;
    }

    public void IsAtkBlockFalse()
    {
        isAtkBlock = false;
    }
}
