using Chapter.EventBus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventBus.Scripts
{
    public class ClientEventBus : MonoBehaviour,EventListener<GameStatesEvent>
    {
        private bool _isButtonEnabled;

        private void Start()
        {
            gameObject.AddComponent<HUDController>();
            gameObject.AddComponent<CountDownTimer>();
            gameObject.AddComponent<PlayerController>();

            _isButtonEnabled = true;
        }

        private void OnEnable()
        {
            this.EventStartingListening<GameStatesEvent>();
        }

        private void OnDisable()
        {
            this.EventStopListening<GameStatesEvent>();
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
                    GameEventManager.TriggerEvent(new GameStatesEvent{
                    gameEventType = GameEventType.COUNTDOWN});
                }
            }
        }

        public void OnEvent(GameStatesEvent eventType)
        {
            switch(eventType.gameEventType){
                case GameEventType.STOP:
                    ReStart();
                    break;
            }
        }
    }
}
