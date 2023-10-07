using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.EventBus
{
    public class PlayerController : MonoBehaviour,EventListener<GameStatesEvent>
    {
        private string _status;
        private void OnEnable()
        {
            this.EventStartingListening<GameStatesEvent>();
        }

        private void OnDisable()
        {
            this.EventStopListening<GameStatesEvent>();
        }

        private void GameStart()
        {
            _status = "Game Started";
        }
        private void GameStoped()
        {
            _status = "Stoped";
        }

        private void OnGUI()
        {
            GUI.color = Color.green;
            GUI.Label(new Rect(10, 60, 200, 20), "Game Status : " + _status);
        }

        public void OnEvent(GameStatesEvent eventType)
        {
            switch(eventType.gameEventType){
                case GameEventType.START:
                    GameStart();
                    break;
                case GameEventType.STOP:
                    GameStoped();
                    break;
            }
        }
    }

}
