using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBlink : MonoBehaviour
{
    Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();

        StartCoroutine(FadeZeroText());
    }

    /** 텍스트 알파값 0 -> 1로 전환 */
    IEnumerator FadeFullText()
    {
        /** 텍스트의 알파 값은 0으로 초기화. */
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);

        /** 텍스트 알파값이 1보다 작으면 */
        while (text.color.a < 1.0f)
        {
            /** text의 알파값 시간이 지남에 따라 증가 */
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime / 2.0f));
            yield return null;
        }

        StartCoroutine(FadeZeroText());
    }

    /** 텍스트 알파값 1 -> 0으로 */
    IEnumerator FadeZeroText()
    {
        /** 텍스트 알파값 1로 초기화 */
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);

        /** text의 알파값이 0보다 크면 계속 진행 */
        while(text.color.a > 0.0f)
        {
            /** text의 알파값 시간이 지남에 따라 감소 */
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime / 2.0f));
            yield return null;
        }

        StartCoroutine(FadeFullText());
    }
}
