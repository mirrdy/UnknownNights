using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBlink : MonoBehaviour
{
    public Text text_Loading;
    private void Update()
    {
        // �ð��� ���� �ؽ�Ʈ �÷� ����
        text_Loading.color = new Color(text_Loading.color.r, text_Loading.color.g, text_Loading.color.b, Mathf.PingPong(Time.time, 1));
    }
}
