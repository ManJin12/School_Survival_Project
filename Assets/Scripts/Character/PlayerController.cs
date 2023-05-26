using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using My;
using static Define;

public class PlayerController : MonoBehaviour
{
    /** 입력받은 위치 값을 적용 할 m_InputVec이름의 변수 선언 */
    public Vector2 m_InputVec;
    /** VariableJoystick 스크립트를 Joystick라고 선언해준다. */
    // public VariableJoystick Joystick;

    /** 캐릭터 속도 값 */
    //public float Speed;
    /** SpriteRenderer기능을 사용할 수 있도록 sprite이름의 변수 선언 */
    public SpriteRenderer m_sprite;
    /** Rigidbody2D기능을 사용할 수 있도록 rigid이름의 변수 선언 */
    Rigidbody2D m_rigid;
    /** Animator기능을 사용할 수 있도록 anim이름의 변수 선언 */
    Animator m_anim;

    public Scanner scanner;

    public Button DashBtn;

    float DashDirection;
    public bool bIsDash = true;
    // public float DashSpeed;
    // public float DashCooltime = 5.0f;
    // public float RemainingDashCoolTime = 5.0f;

    public FloatingJoystick Joystick;

    // Start is called before the first frame update
    void Start()
    {

        /** 만약 Scene이름이  PlayScene이 아니라면 */
        if (GameManager.GMInstance.CurrentScene != Define.ESceneType.PlayScene)
        {
            /** 스크립트 비활성화 */
            enabled = false;
        }

        /** 만약 오브젝트 생성 시 현재 Scene이름이 PlayScene이라면 */
        if (GameManager.GMInstance.CurrentScene == Define.ESceneType.PlayScene)
        {

            /** FloatingJoystick타입의 조이스틱을 찾는다. */
            Joystick = FindObjectOfType<FloatingJoystick>();

            #region DashBtn
            /** TODO ## PlayerController.cs DashBtn 주석처리 */
            /** DashBtn에 DashBtn이라는 이름을 가진 버튼을 넣어주고 */
            // DashBtn = GameObject.Find("DashBtn").GetComponent<Button>();

            /** DashBtn이 연결 됬다면 */
            // if (DashBtn != null)
            // {
            /** 클릭 시 함수 호출 */
            // DashBtn.onClick.AddListener(OnClickedDash);
            // }
            #endregion
        }


        /** rigid가 Rigidbody2D컴포넌트의 기능에 접근할 수 있도록 한다. */
        m_rigid = GetComponent<Rigidbody2D>();
        /** sprite가 SpriteRenderer컴포넌트의 기능에 접근할 수 있도록 한다. */
        m_sprite = GetComponent<SpriteRenderer>();
        /** anim이 Animator컴포넌트의 기능에 접근할 수 있도록 한다. */
        m_anim = GetComponent<Animator>();
        /** DashSpeed는 GameManager.GMInstance.DashSpeed로 바꾼다. */
        // DashSpeed = GameManager.GMInstance.DashSpeed;
        scanner = GetComponent<Scanner>();
    }


    // Update is called once per frame
    void Update()
    {
        if (!GameManager.GMInstance.bIsLive)
        {
            return;
        }

        #region DashBtnReturn
        /** DashBtn버튼 못찾았으면 return */
        //if (DashBtn == null)
        //{
        //    return;
        //}
        #endregion
        /** Joystick 못 찾았으면 return */
        if (Joystick == null)
        {
            return;
        }

        /** TODO ## PlayerController.cs TEST용 Input */
        // m_InputVec.x = Input.GetAxisRaw("Horizontal");
        // m_InputVec.y = Input.GetAxisRaw("Vertical");

        /** m_InputVec.x은 joystick이 입력받은 Horizontal값을 받을 */
        m_InputVec.x = Joystick.Horizontal;
        /** m_InputVec.y은 joystick이 입력받은 Vertical값을 받을 */
        m_InputVec.y = Joystick.Vertical;

        /** bIsDash가 false면 RemainingDashCoolTime을 깎아준다 */
        //if (bIsDash == false)
        //{
        //    RemainingDashCoolTime -= Time.deltaTime;
        //}

        /** RemainingDashCoolTime이 0보다 작거나 같으면 */
        //if (RemainingDashCoolTime <= 0)
        //{
        //    /** 대쉬가 가능해진다. */
        //    bIsDash = true;
        //    /** 줄어드는 쿨타임을 설정된 대쉬의 쿨타임으로 바꿔준다. */
        //    RemainingDashCoolTime = DashCooltime;
        //}
    }

    /** 물리 연산 프레임마다 호출되는 생명주기 함수 */
    void FixedUpdate()
    {
        if (!GameManager.GMInstance.bIsLive)
        {
            return;
        }

        /** TODO ## PlayerController.cs 위치 이동 관련 */
        /**
        newVec은 m_InputVec의 대각선 이동 시 크기가 다르기 때문에 1의값을 반환받을 수 있도록
        normalize를 사용한다. Speed변수로 속력의 크기를 곱하고 물리프레임 하나가 소비한 시간을 곱해준다.
        */
        Vector2 newVec = m_InputVec.normalized * GameManager.GMInstance.PlayerSpeed * Time.fixedDeltaTime;
        m_rigid.MovePosition(m_rigid.position + newVec);
    }

    /** 업데이트 함수가 끝나고 다음 프레임으로 넘어가기 직전에 사용되는 생명주기 함수 */
    void LateUpdate()
    {
        if (!GameManager.GMInstance.bIsLive)
        {
            return;
        }

        /** TODO ## PlayerController.cs 애니메이션 설정 */
        /** 만약 입력받은 x값이 있다면(움직이는 중 이라면) */
        if (m_InputVec.x != 0)
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

    #region OnClickDash
    //public void OnClickedDash()
    //{
    //    /** 만약 대쉬 상태가 false면 함수를 빠져나감 */
    //    if (bIsDash == false)
    //    {
    //        return;
    //    }

    //    /** velocity를 입력받은 값으로 키를 눌렸을 때 입력받은 값을 넣어준다. */
    //    Vector3 velocity = new Vector3(m_InputVec.x, m_InputVec.y, 0);

    //    /** 대시 스피드를 곱하여 더 멀리 캐릭터를 위치 시킨다. */
    //    transform.position += velocity * Speed * DashSpeed * Time.deltaTime;

    //    /** 대시 이동이 끝나면 bIsDash를 false로 막아 무한으로 사용할 수 없게 한다. */
    //    bIsDash = false;
    //}
    #endregion


    /** TODO ## PlayerController.cs 캐릭터 피격판정 몬스터와 캐릭터가 닿고 있다면 */
    void OnCollisionStay2D(Collision2D collision)
    {
        /** 게임이 멈춰 있다면 */
        if (GameManager.GMInstance.bIsLive == false)
        {
            return;
        }

        if (collision.gameObject.CompareTag("Monster"))
        {
            /** 플레이어의 체력을 달게 한다. */
            GameManager.GMInstance.Health -= Time.deltaTime * 10;
        }

        /** 플레이어의 피가 0이되면 */
        if (GameManager.GMInstance.Health <= 0)
        {
            /** 플레이어의 2번째 자식 오브젝트부터 반복문 */
            for (int i = 2; i < transform.childCount; i++)
            {
                /** 2번째부터 오브젝트 모두 비활성화 */
                transform.GetChild(i).gameObject.SetActive(false);
            }

            m_anim.SetTrigger("Dead");
            GameManager.GMInstance.PlaySceneManagerRef.GameOver();
        }
    }
}
