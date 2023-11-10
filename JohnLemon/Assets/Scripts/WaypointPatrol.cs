using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
{
    public float objBlockTime;
    public NavMeshAgent navMeshAgent;
    public Transform[] waypoints;
    int m_CurrentWaypointIndex;

    [SerializeField] Collider[] m_colliders;

    private bool isMoveBlock = false;

    void Start()
    {
        // waypoint[0]�� �������� ����
        navMeshAgent.SetDestination(waypoints[0].position);
    }

    void Update()
    {
        if(isMoveBlock == false)
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
            StartCoroutine("MoveBlock");
        }
    }


    IEnumerator MoveBlock()
    {
        ColliderActive();
        navMeshAgent.SetDestination(this.transform.position); // ���� �������� �� ��ġ�� �����ؼ�, �������� ���� 
        isMoveBlock = true; // bool�� �̿��� �������� ����

        yield return new WaitForSeconds(objBlockTime);

        isMoveBlock = false;
        ColliderActive();
        navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
    }


    private void ColliderActive()
    {
        for (int i = 0; i < m_colliders.Length; i++)
        {
            m_colliders[i].enabled = !m_colliders[i].enabled;
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
