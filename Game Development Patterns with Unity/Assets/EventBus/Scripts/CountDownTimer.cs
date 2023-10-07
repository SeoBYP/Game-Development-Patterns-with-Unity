using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.EventBus
{
    public class CountDownTimer : MonoBehaviour, EventListener<GameStatesEvent>
    {
        private float _currentTime;
        private float _duration = 3.0f;

        private void OnEnable()
        {
            this.EventStartingListening<GameStatesEvent>();
        }
        private void OnDisable()
        {
            this.EventStopListening<GameStatesEvent>();
        }

        private void StartTimer()
        {
            StartCoroutine(CountDown());
        }

        private IEnumerator CountDown()
        {
            _currentTime = _duration;

            while (_currentTime > 0)
            {
                yield return new WaitForSeconds(1.0f);
                _currentTime--;
            }

            GameEventManager.TriggerEvent(new GameStatesEvent{
                gameEventType = GameEventType.START});
        }

        public void OnEvent(GameStatesEvent eventType)
        {
            switch(eventType.gameEventType){
                case GameEventType.COUNTDOWN:
                    StartTimer();
                    break;
            }
        }

        private void OnGUI()
        {
            GUI.color = Color.blue;
            GUI.Label(new Rect(125, 0, 100, 20), "CountDown: " + _currentTime);
        }
    }
}
