using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Chapter.Singleton
{
    public class GameManager : SingleTon<GameManager>
    {
        private DateTime _sessionStartTime;
        private DateTime _sessionEndTime;

        private void Start()
        {
            //TODO:
            //- 플레이어 데이터 불러오기
            //- 저장된 데이터가 없으면 플레이어를 등록 화면으로 리디렉션
            //- 백엔드 호출하여 일일 도전 과제 및 보상 가져오기

            _sessionStartTime = DateTime.Now;
            Debug.Log("게임 세션 시작 시간 @: " + DateTime.Now);
        }

        private void OnApplicationQuit()
        {
            _sessionEndTime = DateTime.Now;
            TimeSpan timeDifference = _sessionEndTime.Subtract(_sessionStartTime);
            Debug.Log("게임 세션 종료 시간 @: " + DateTime.Now);
            Debug.Log("게임 세션 지속 시간: " + timeDifference);
        }

        private void OnGUI()
        {
            if (GUI.Button(new Rect(10, 120, 200, 50), $"{SceneManager.GetActiveScene().name} 다음 장면"))
            {
                if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCount)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                else
                {
                    SceneManager.LoadScene(0);
                }
            }
        }
    }

}
