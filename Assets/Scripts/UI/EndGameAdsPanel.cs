using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using My;

public class EndGameAdsPanel : MonoBehaviour
{
    // Start is called before the first frame update

    public RectTransform EndGamePanelrect;

    void Start()
    {
        if (gameObject.name == "GameClearPanel")
        {
            GameManager.GMInstance.EndGameAdsPanelRef = this;
        }
        else if (gameObject.name == "GameOverPanel")
        {
            GameManager.GMInstance.GameFailedAdsPanelRef = this;
        }

        EndGamePanelrect = GetComponent<RectTransform>();
        /** 시작 시 안보이게 */
        EndGamePanelrect.localScale = Vector3.zero;
    }
}
