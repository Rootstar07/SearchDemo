using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LeTai.TrueShadow;

public class ThemeManager : MonoBehaviour
{
    public GameObject[] 기본색;
    public GameObject[] 강조색;
    public GameObject[] 그림자색;
    public TrueShadow[] 하단바버튼그림자;
    public GameObject 배경;
    [Space]
    public Color basicColor;
    public Color anoColor;
    public Color shadowColor;
    public Color backgroundColor;

    private void Start()
    {
        ChangeTheme();
    }

    public void ChangeTheme()
    {
        for (int i =0; i < 기본색.Length; i++)
        {
            if (기본색[i] != null)
                기본색[i].GetComponent<Image>().color = basicColor;
        }

        for (int i = 0; i < 강조색.Length; i++)
        {
            if (강조색[i] != null)
                강조색[i].GetComponent<Image>().color = anoColor;
        }

        for (int i = 0; i < 그림자색.Length; i++)
        {
            if (그림자색[i] != null)
                그림자색[i].GetComponent<Image>().color = shadowColor;
        }

        for (int i = 0; i < 하단바버튼그림자.Length; i++)
        {
            하단바버튼그림자[i].Color = shadowColor;
        }

        배경.GetComponent<Image>().color = backgroundColor;
    }

}
