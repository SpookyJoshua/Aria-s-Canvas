using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PU_UI : MonoBehaviour
{
    public bool isUsed;

    [SerializeField] private Image PU_Image;

    private void Update()
    {
        if(PU_Image.color.a == 0)
        {
            GetComponent<RawImage>().color = PU_Image.color;
        }
        else
        {
            GetComponent<RawImage>().color = new Color(PU_Image.color.r, PU_Image.color.g, PU_Image.color.b, 1);
        }
    }
}
