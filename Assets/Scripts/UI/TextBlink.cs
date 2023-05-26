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

    /** �ؽ�Ʈ ���İ� 0 -> 1�� ��ȯ */
    IEnumerator FadeFullText()
    {
        /** �ؽ�Ʈ�� ���� ���� 0���� �ʱ�ȭ. */
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);

        /** �ؽ�Ʈ ���İ��� 1���� ������ */
        while (text.color.a < 1.0f)
        {
            /** text�� ���İ� �ð��� ������ ���� ���� */
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime / 2.0f));
            yield return null;
        }

        StartCoroutine(FadeZeroText());
    }

    /** �ؽ�Ʈ ���İ� 1 -> 0���� */
    IEnumerator FadeZeroText()
    {
        /** �ؽ�Ʈ ���İ� 1�� �ʱ�ȭ */
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);

        /** text�� ���İ��� 0���� ũ�� ��� ���� */
        while(text.color.a > 0.0f)
        {
            /** text�� ���İ� �ð��� ������ ���� ���� */
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime / 2.0f));
            yield return null;
        }

        StartCoroutine(FadeFullText());
    }
}
