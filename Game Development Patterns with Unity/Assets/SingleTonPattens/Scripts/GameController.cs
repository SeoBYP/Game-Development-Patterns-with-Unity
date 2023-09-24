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
        // Singleton 인스턴스에 접근
        SingletonExample.Instance.IncreaseScore(10);
        Debug.Log("현재 점수: " + SingletonExample.Score);
    }

    private void OnGUI()
    {
        // 점수 텍스트를 위한 GUIStyle 생성
        GUIStyle style = new GUIStyle();
        style.fontSize = 24;
        style.normal.textColor = Color.white;

        // 점수 표시 위치 및 크기 정의
        Rect scoreRect = new Rect(10, 10, 200, 50); // 이 값을 필요에 따라 조절

        // 현재 점수 표시
        GUI.Label(scoreRect, "점수: " + SingletonExample.Score, style);

        // 버튼 클릭 감지 및 점수 증가
        if (GUI.Button(new Rect(10, 70, 200, 50), "점수 5 점 증가"))
        {
            SingletonExample.Instance.IncreaseScore(5);
        }
    }
}
