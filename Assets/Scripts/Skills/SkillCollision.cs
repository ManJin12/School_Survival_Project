
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
using My;

public class SkillCollision : MonoBehaviour
{
    /** 스킬이펙트 배열 */
    public GameObject[] SkillEffect;
    /** 스킬 사라지는 거리 */
    public float DestroySkillLength;
    /** 플레이어 */
    public GameObject Player;
    /** 스킬 id */
    public int Skills_ID;
    /** rigid */
    Rigidbody2D rigid;
    bool CharacterDir;

    /** 볼텍스 방향 랜덤 */
    int RandomDir;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }


    void Start()
    {
        if (GameManager.GMInstance.CurrentScene != ESceneType.PlayScene)
        {
            enabled = false;
            return;
        }

        /** 플레이어 찾음 */
        Player = GameManager.GMInstance.Player;

        /** 이름이 Mateo(Clone)라면 */
        if (gameObject.name == "Mateo(Clone)")
        {
            /** Skills_ID는 스킬이 저장된 인덱스 번호가 된다 */
            Skills_ID = (int)ESkillType.Skill_Mateo;
            /** 중력값은 랜덤으로 준다 */
            rigid.gravityScale = Random.Range(3, 5);
            /** 사라질 순간의 y값을 랜덤으로 준다. */
            DestroySkillLength = Random.Range(Player.transform.position.y - 4.5f, Player.transform.position.y + 4.5f);
            /** 크기를 2만큼한다 */
            gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }

        /** 이름이 토네이도라면 */
        if (gameObject.name == "Tornado(Clone)")
        {
            CharacterDir = GameManager.GMInstance.playerCtrl.m_sprite.flipX;
        }

        /** TODO ## SkillCollision.cs 볼텍스 방향 조정 */
        /** 이름이 Vortex라면 */
        if (gameObject.name == "Vortex(Clone)")
        {
            CharacterDir = GameManager.GMInstance.playerCtrl.m_sprite.flipX;
            /** 볼텍스 방향 랜덤 */
            RandomDir = Random.Range(0, 3);
            // Debug.Log(RandomDir);

            if (RandomDir == 0)
            {
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
            }
            else if (RandomDir == 1)
            {
                gameObject.GetComponent<SpriteRenderer>().flipY = true;
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
            else if (RandomDir == 2)
            {
                
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
                gameObject.GetComponent<SpriteRenderer>().flipY = true;
            }
        }

        /** 트랩 일 시 */
        if (gameObject.name == "Trap(Clone)")
        {
            /** 5초 후 게임오브젝트 삭제 */
            Destroy(gameObject, 5.0f);
        }
    }

    void Update()
    {
        /** 이름이 Mateo(Clone)일때 */
        if (gameObject.name == "Mateo(Clone)")
        {
            /** 이펙트 생성 */
            MakeMateoEffect();
        }

        /** 만약 게임오브젝트 이름이 Tornado면 */
        if (gameObject.name == "Tornado(Clone)")
        {
            /** 캐릭터가 우측 이동하고 있을 때 */
            if (CharacterDir == false)
            {
                gameObject.transform.Translate(Vector2.right * Time.deltaTime * 5);
            }
            /** 캐릭터가 좌측 이동하고 있을 떄 */
            else if (CharacterDir == true)
            {
                gameObject.transform.Translate(Vector2.right * -1.0f * Time.deltaTime * 5);
            }
        }

        /** 만약 게임오브젝트 이름이 Vortex면 */
        if (gameObject.name == "Vortex(Clone)")
        {
            /** 캐릭터가 우측 이동하고 있을 때 */
            if (CharacterDir == false)
            {
                if (RandomDir == 0)
                {
                    /** 오른쪽으로 발사 */
                    gameObject.transform.Translate(Vector2.down * Time.deltaTime * 5);
                }
                else if (RandomDir == 1)
                {
                    /** 위로 발사 */
                    gameObject.transform.Translate(Vector2.up * Time.deltaTime * 5);
                }
                else if (RandomDir == 2)
                {
                    /** 아래로 발사 */
                    gameObject.transform.Translate(Vector2.up * Time.deltaTime * 5);
                }

            }
            /** 캐릭터가 좌측 이동하고 있을 떄 */
            else if (CharacterDir == true)
            {
                if (RandomDir == 0)
                {
                    gameObject.GetComponent<SpriteRenderer>().flipY = true;
                    /** 오른쪽으로 왼쪽으로 발사 */
                    gameObject.transform.Translate(Vector2.down * -1.0f * Time.deltaTime * 5);
                }
                else if (RandomDir == 1)
                {
                    
                    /** 위로 발사 */
                    gameObject.transform.Translate(Vector2.up * Time.deltaTime * 5);
                }
                else if (RandomDir == 2)
                {
                    /** 아래로 발사 */
                    gameObject.transform.Translate(Vector2.up * Time.deltaTime * 5);
                }

            }
        }

        ///** 만약 게임오브젝트 이름이 Tornado면 */
        //if (gameObject.name == "Tornado(Clone)")
        //{
        //    /** 캐릭터가 우측 이동하고 있을 때 */
        //    if (GameManager.GMInstance.playerCtrl.m_InputVec.x >= 0)
        //    {
        //        gameObject.transform.Translate(Vector2.right * Time.deltaTime * 5);
        //    }
        //    /** 캐릭터가 좌측 이동하고 있을 떄 */
        //    else if (GameManager.GMInstance.playerCtrl.m_InputVec.x <= 0)
        //    {
        //        gameObject.transform.Translate(Vector2.right * -1.0f * Time.deltaTime * 5);
        //    }
        //}
    }

    void MakeMateoEffect()
    {
        /** 오브젝트의 y값이 DestroySkillLength보다 작으면 */
        if (gameObject.transform.position.y < DestroySkillLength)
        {
            /** SkillEffect 0번에 저장된 게임오브젝트 생성 */
            GameObject MateoEffect = Instantiate(SkillEffect[0]);
            /** 효과음 재생 */
            GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.MateoEffect);

            /** 위치는 오브젝트의 위치 */
            MateoEffect.transform.position = gameObject.transform.position;
            /** 메테오 이펙트 크기 조절 */
            MateoEffect.transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);

            /** 스킬과 이펙트 사라짐 */
            Destroy(gameObject);
            Destroy(MateoEffect, 0.5f);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        /** 게임 오브젝트 이름이 Trap(Clone)일 때 */
        if (gameObject.name == "Trap(Clone)")
        {
            /** 몬스터에 닿았다면 */
            if (collision.CompareTag("Monster"))
            {
                /** SkillEffect 0번에 저장된 게임오브젝트 생성 */
                GameObject TrapEffect = Instantiate(SkillEffect[0]);

                /** 효과음 재생 */
                GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.TrapEffect);

                /** 폭발 이펙트는 오브젝트의 위치 */
                TrapEffect.transform.position = gameObject.transform.position;
                TrapEffect.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);

                Destroy(gameObject);
                Destroy(TrapEffect, 0.5f);
            }
        }
        /** 게임 오브젝트 이름이 Bomb(Clone)일 때 */
        else if (gameObject.name == "BombArrow(Clone)")
        {
            /** 몬스터에 닿았다면 */
            if (collision.CompareTag("Monster"))
            {
                /** SkillEffect 0번에 저장된 게임오브젝트 생성 */
                GameObject BombEffect = Instantiate(SkillEffect[0]);

                /** 효과음 재생 */
                GameManager.GMInstance.SoundManagerRef.PlaySFX(SoundManager.SFX.BombArrowEffect);

                /** 폭발 이펙트는 오브젝트의 위치 */
                BombEffect.transform.position = gameObject.transform.position;
                BombEffect.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);

                Destroy(BombEffect, 0.5f);
            }
        }
    }

}

