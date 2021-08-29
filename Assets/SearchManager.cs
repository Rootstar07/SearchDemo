using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchManager : MonoBehaviour
{
    TouchScreenKeyboard keyboard;
    public Text txt;
    string fromis;

    private void Update()
    {
        if (TouchScreenKeyboard.visible == false && keyboard != null)
        {

            // 모바일 키보드에서 완료를 누르면
            if (keyboard.done)
            {
                fromis = keyboard.text;
                txt.text = fromis;
                keyboard = null;
            }
        }
    }

    public void SearchButtonClicked()
    {
        Debug.Log("이벤트 시작");
        keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
    }

}
