using Chapter.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Singleton �ν��Ͻ��� ����
        SingletonExample.Instance.IncreaseScore(10);
        Debug.Log("���� ����: " + SingletonExample.Score);
    }

    private void OnGUI()
    {
        // ���� �ؽ�Ʈ�� ���� GUIStyle ����
        GUIStyle style = new GUIStyle();
        style.fontSize = 24;
        style.normal.textColor = Color.white;

        // ���� ǥ�� ��ġ �� ũ�� ����
        Rect scoreRect = new Rect(10, 10, 200, 50); // �� ���� �ʿ信 ���� ����

        // ���� ���� ǥ��
        GUI.Label(scoreRect, "����: " + SingletonExample.Score, style);

        // ��ư Ŭ�� ���� �� ���� ����
        if (GUI.Button(new Rect(10, 70, 200, 50), "���� 5 �� ����"))
        {
            SingletonExample.Instance.IncreaseScore(5);
        }
    }
}
