using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using My;

public class HUD : MonoBehaviour
{
    public enum InfoType { Exp, Level, Kill, Time, Health }
    public InfoType type;

    Text myText;
    Slider mySlider;

    void Awake()
    {
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
    }

    void LateUpdate()
    {
        switch (type)
        {
            case InfoType.Exp:
                float curExp = GameManager.GMInstance.exp;
                float maxExp = GameManager.GMInstance.nextExp[GameManager.GMInstance.level];
                mySlider.value = curExp / maxExp;
                break;
            case InfoType.Level:
                myText.text = string.Format("Lv.{0:F0}", GameManager.GMInstance.level);
                break;
            case InfoType.Kill:
                myText.text = string.Format("{0:F0}", GameManager.GMInstance.killcount);
                break;
            case InfoType.Time:
                float remainTime = GameManager.GMInstance.maxGameTime - GameManager.GMInstance.gameTime;
                int min = Mathf.FloorToInt(remainTime / 60);
                int sec = Mathf.FloorToInt(remainTime % 60);
                myText.text = string.Format("{0:D2}:{1:D2}", min, sec);
                break;
            case InfoType.Health:
                float curHealth = GameManager.GMInstance.Health;
                float maxHealth = GameManager.GMInstance.MaxHealth;
                mySlider.value = curHealth / maxHealth;
                break;
        }
    }
}
