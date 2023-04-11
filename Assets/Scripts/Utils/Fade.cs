using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    Image Black_screen;
    public float Fade_Time = 2f;
    public float Fade_Max = 1f;
    float _time;
    public bool FadeIn_ing = true;
    public bool FadeOut_ing;

    void Start()
    {
        Black_screen = GetComponent<Image>();
    }

    void Update()
    {
        /** 페이드 인 중이라면 */
        if (FadeIn_ing)
        {
            /** _time은 틱당 올라감 */
            _time += Time.deltaTime;
            /** Black_screen.color는 검은 화면에서 투명색 화면으로 Fade_Time시간 만큼 전환 */
            Black_screen.color = Color.Lerp(new Color(0, 0, 0, Fade_Max), new Color(0, 0, 0, 0), _time / Fade_Time);
        }

        /** 페이드 아웃 중이라면 */
        if (FadeOut_ing)
        {
            /** _time은 틱당 올라감 */
            _time += Time.deltaTime;
            /** Black_screen.color는 투명색 화면에서 검은 화면 Fade_Time시간 만큼 전환 */
            Black_screen.color = Color.Lerp(new Color(0, 0, 0, 0), new Color(0, 0, 0, Fade_Max), _time / Fade_Time);
        }

        /** 만약 _time 시간이 Fade_Time보다 크다면 화면 전환이 끝났다면 */
        if (_time >= Fade_Time)
        {
            /** _time = 0으로 초기화 */
            _time = 0;

            /** 화면전환이 끝났기 때문에 FadeOut_ing false */
            FadeOut_ing = false;

            /** FadeIn_ing이 true면 */
            if (FadeIn_ing == true)
            {
                /** 이 클래스 가지고 있는 오브젝트 제거 */
                Destroy(this.gameObject);
            }
        }
    }

    public void FadeIn()
    {
        FadeIn_ing = true;
    }

    public void FadeOut()
    {
        FadeOut_ing = true;
    }


}