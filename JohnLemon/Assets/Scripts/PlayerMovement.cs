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

        // 2개의 값이 유사하면 true , 아니면 false
        // 움직인다면 0이 아니라서 fale지만 !를 썻기때문에 true가 된다.
        // input은 움직일경우 true가 된다.
        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0);
    }
}
