using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
{
    public float moveBlockTime; // block ����� �ð�
    public float curTime =-1f; // Ÿ�̸��۵����� �ð��� �����ϴ� ����
    public NavMeshAgent navMeshAgent;
    public Transform[] waypoints;
    int m_CurrentWaypointIndex;

    [SerializeField] private Observer observer;

    private bool isMoveBlock = false;

    void Start()
    {
        // waypoint[0]�� �������� ����
        navMeshAgent.SetDestination(waypoints[0].position);
    }

    void Update()
    {
        UpdateTimer();
        if (isMoveBlock == false)
        {
            // ���������� ���� �Ÿ�(remain)�� ������(stopDistance)���� ���� ���
            if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
            {
                // ���� �ε������� +1�� ���� ������ �̹Ƿ� +1�� ���ְ�, ������ŭ�� ������ ������ index�� �������ش�.
                // �̷��� ���� ��� ���� ���� �������� �����ָ鼭, ������ �����̵��� �Ѵ�.
                m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
                navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Ammo")
        {
            if (isMoveBlock == false)
            {
                Debug.Log("ismoveblock==false");
                ActiveStun();
            }
        }
    }
    private void ActiveStun()
    {
        // ���Ͽ� �ɸ���, �������� ���ϵ��� �����Ѵ�.
        observer.IsAtkBlockTrue();
        ActiveMoveBlock();
    }

    private void CancleStun()
    {
        observer.IsAtkBlockFalse();
    }

    private void ActiveMoveBlock()
    {
        // 1. �ڷ�ƾ ����ϱ�
        //StartCoroutine("MoveBlock");

        // 2. timer ����ϱ�
        navMeshAgent.SetDestination(this.transform.position);
        isMoveBlock = true;
        curTime = 0;
    }

    private void CancleMoveBlock()
    {
        curTime = moveBlockTime;
 
    }


    IEnumerator MoveBlock()
    {
        // ���� �������� �� ��ġ�� �����ϰ�, bool�� �̿��� ������ ����
        navMeshAgent.SetDestination(this.transform.position);
        isMoveBlock = true;

        yield return new WaitForSeconds(moveBlockTime);

        isMoveBlock = false;
    }

    private void UpdateTimer()
    {
        if (curTime >= 0)
        {
            if (curTime >= moveBlockTime)
            {
                isMoveBlock = false;
                curTime = -1;
                CancleStun();
            }
            else
            {
                curTime += Time.deltaTime;
            }
        }
    }

    private void OnDrawGizmos()
    {
        //// remainingDistance�� �ð������� ǥ���ǵ��� ������
        ////Gizmos.DrawLine(waypoints[m_CurrentWaypointIndex].position, this.transform.position); -> remainDistance�� ���� �Ÿ�
        //Gizmos.DrawWireSphere(this.transform.position, navMeshAgent.remainingDistance); // -> ��ġ�κ��� ���� �Ÿ��� ǥ��
        //Gizmos.color = Color.red;


        //// wayPoint(������)�� ������� ���������� �˷���.
        //Vector3 vWayPointPos = waypoints[m_CurrentWaypointIndex].position;
        //Gizmos.color = Color.red;

        //Gizmos.DrawWireSphere(vWayPointPos, navMeshAgent.stoppingDistance);

        //for (int i = 0; i < waypoints.Length; i++)
        //{
        //    Gizmos.color = Color.white;
        //    Gizmos.DrawWireSphere(waypoints[i].position, navMeshAgent.stoppingDistance);
        //}

    }
}
