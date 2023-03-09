using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ImageChange : MonoBehaviour
{

    public Sprite[] CharImage;
    public Image Image;
    // Start is called before the first frame update
    void Start()
    {
        Image = GetComponent<Image>();

        Image.sprite = CharImage[(int)GameManager.GMInstance.CurrentChar];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
