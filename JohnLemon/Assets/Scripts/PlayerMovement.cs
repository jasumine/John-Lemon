using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Vector3 m_Movement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        m_Movement.Set(horizontal,0f,vertical);
        m_Movement.Normalize();

        // 2���� ���� �����ϸ� true , �ƴϸ� false
        // �����δٸ� 0�� �ƴ϶� fale���� !�� ���⶧���� true�� �ȴ�.
        // input�� �����ϰ�� true�� �ȴ�.
        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0);
    }
}
