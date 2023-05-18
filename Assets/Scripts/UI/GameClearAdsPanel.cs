using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using My;

public class GameClearAdsPanel : MonoBehaviour
{
    // Start is called before the first frame update

    public RectTransform GameClearPanelrect;

    void Start()
    {
        GameClearPanelrect = GetComponent<RectTransform>();

        GameManager.GMInstance.GameClearAdsPanelRef = this;

        GameClearPanelrect.localScale = Vector3.zero;
    }
}
