using Chapter.EventBus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventBus.Scripts
{
    public class ClientEventBus : MonoBehaviour
    {
        private bool _isButtonEnabled;

        private void Start()
        {
            gameObject.AddComponent<HUDController>();
            gameObject.AddComponent<CountDownTimer>();
            gameObject.AddComponent<BikeController>();

            _isButtonEnabled = true;
        }

        private void OnEnable()
        {
            RaceEventBus.Subscribe(RaceEventType.STOP, ReStart);
        }

        private void OnDisable()
        {
            RaceEventBus.Unsubscribe(RaceEventType.STOP, ReStart);
        }

        private void ReStart()
        {
            _isButtonEnabled = true;
        }

        private void OnGUI()
        {
            if (_isButtonEnabled)
            {
                if(GUILayout.Button("Start CountDown"))
                {
                    _isButtonEnabled= false;
                    RaceEventBus.Publish(RaceEventType.COUNTDOWN);
                }
            }
        }
    }
}
