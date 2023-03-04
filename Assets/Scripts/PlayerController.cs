using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /** 입력받은 위치 값을 적용 할 m_InputVec이름의 변수 선언 */
    public Vector2 m_InputVec;
    /** VariableJoystick 스크립트를 Joystick라고 선언해준다. */
    // public VariableJoystick Joystick;
    /** 캐릭터 속도 값 */
    public float Speed;
    /** SpriteRenderer기능을 사용할 수 있도록 sprite이름의 변수 선언 */
    SpriteRenderer m_sprite;
    /** Rigidbody2D기능을 사용할 수 있도록 rigid이름의 변수 선언 */
    Rigidbody2D m_rigid;
    /** Animator기능을 사용할 수 있도록 anim이름의 변수 선언 */
    Animator m_anim;

    // Start is called before the first frame update
    void Start()
    {
        /** rigid가 Rigidbody2D컴포넌트의 기능에 접근할 수 있도록 한다. */
        m_rigid = GetComponent<Rigidbody2D>();
        /** sprite가 SpriteRenderer컴포넌트의 기능에 접근할 수 있도록 한다. */
        m_sprite = GetComponent<SpriteRenderer>();
        /** anim이 Animator컴포넌트의 기능에 접근할 수 있도록 한다. */
        m_anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        /** m_InputVec.x은 joystick이 입력받은 Horizontal값을 받을 */
        m_InputVec.x = GameManager.GMInstance.Joystick.Horizontal;
        /** m_InputVec.y은 joystick이 입력받은 Vertical값을 받을 */
        m_InputVec.y = GameManager.GMInstance.Joystick.Vertical;
    }

    /** 물리 연산 프레임마다 호출되는 생명주기 함수 */
    void FixedUpdate()
    {
        /** TODO ## 위치 이동 관련 */
        /**
        newVec은 m_InputVec의 대각선 이동 시 크기가 다르기 때문에 1의값을 반환받을 수 있도록
        normalize를 사용한다. Speed변수로 속력의 크기를 곱하고 물리프레임 하나가 소비한 시간을 곱해준다.
        */
        Vector2 newVec = m_InputVec.normalized * Speed * Time.fixedDeltaTime;
        m_rigid.MovePosition(m_rigid.position + newVec);
    }

    /** 업데이트 함수가 끝나고 다음 프레임으로 넘어가기 직전에 사용되는 생명주기 함수 */
    void LateUpdate()
    {
     
        /** 만약 입력받은 x값이 있다면(움직이는 중 이라면) */
        if(m_InputVec.x != 0)
        {
            /** Animator의 Parameter에 있는 IsMove를 true로 바꿔준다. */
            m_anim.SetBool("IsMove", true);

            /** m_InputVec.x이 0보다 작으면 X축 반전 */
            m_sprite.flipX = m_InputVec.x < 0;
        }
        /** 만약 받은 x값이 없다면(움직이지 않는 중 이라면) */ 
        else if (m_InputVec.x == 0)
        {
            /** Animator의 Parameter에 있는 IsMove를 false로 바꿔준다. */
            m_anim.SetBool("IsMove", false);
        }
    }
}
