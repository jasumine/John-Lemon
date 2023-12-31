using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
{
    public float moveBlockTime; // block 디버프 시간
    public float curTime =-1f; // 타이머작동까지 시간을 누적하는 변수
    public NavMeshAgent navMeshAgent;
    public Transform[] waypoints;
    int m_CurrentWaypointIndex;

    [SerializeField] private Observer observer;

    private bool isMoveBlock = false;

    void Start()
    {
        // waypoint[0]을 목적지로 설정
        navMeshAgent.SetDestination(waypoints[0].position);
    }

    void Update()
    {
        UpdateTimer();
        if (isMoveBlock == false)
        {
            // 목적지까지 남은 거리(remain)가 목적지(stopDistance)보다 작을 경우
            if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
            {
                // 현재 인덱스에서 +1이 다음 목적지 이므로 +1을 해주고, 개수만큼의 나머지 값으로 index를 설정해준다.
                // 이렇게 나온 계산 값을 다음 목적지로 정해주면서, 유령이 움직이도록 한다.
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
        // 스턴에 걸리면, 움직이지 못하도록 설정한다.
        observer.IsAtkBlockTrue();
        ActiveMoveBlock();
    }

    private void CancleStun()
    {
        observer.IsAtkBlockFalse();
    }

    private void ActiveMoveBlock()
    {
        // 1. 코루틴 사용하기
        //StartCoroutine("MoveBlock");

        // 2. timer 사용하기
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
        // 다음 목적지를 내 위치로 설정하고, bool을 이용해 움직임 제한
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
        //// remainingDistance를 시각적으로 표현되도록 만들어보기
        ////Gizmos.DrawLine(waypoints[m_CurrentWaypointIndex].position, this.transform.position); -> remainDistance의 실제 거리
        //Gizmos.DrawWireSphere(this.transform.position, navMeshAgent.remainingDistance); // -> 위치로부터 남은 거리를 표시
        //Gizmos.color = Color.red;


        //// wayPoint(목적지)가 어디인지 가시적으로 알려줌.
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
