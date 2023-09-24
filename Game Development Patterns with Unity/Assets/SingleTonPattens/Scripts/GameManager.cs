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
            //- �÷��̾� ������ �ҷ�����
            //- ����� �����Ͱ� ������ �÷��̾ ��� ȭ������ ���𷺼�
            //- �鿣�� ȣ���Ͽ� ���� ���� ���� �� ���� ��������

            _sessionStartTime = DateTime.Now;
            Debug.Log("���� ���� ���� �ð� @: " + DateTime.Now);
        }

        private void OnApplicationQuit()
        {
            _sessionEndTime = DateTime.Now;
            TimeSpan timeDifference = _sessionEndTime.Subtract(_sessionStartTime);
            Debug.Log("���� ���� ���� �ð� @: " + DateTime.Now);
            Debug.Log("���� ���� ���� �ð�: " + timeDifference);
        }

        private void OnGUI()
        {
            if (GUI.Button(new Rect(10, 120, 200, 50), $"{SceneManager.GetActiveScene().name} ���� ���"))
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
