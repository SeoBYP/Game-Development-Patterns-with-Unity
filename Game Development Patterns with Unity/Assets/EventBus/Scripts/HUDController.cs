using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.EventBus
{
    public class HUDController : MonoBehaviour, EventListener<GameStatesEvent>
    {
        private bool _isDispalyOn;

        private void OnEnable()
        {
            this.EventStartingListening<GameStatesEvent>();
        }

        private void OnDisable()
        {
            this.EventStopListening<GameStatesEvent>();
        }

        private void DisplayHUD()
        {
            _isDispalyOn = true;
        }

        private void OnGUI()
        {
            if (_isDispalyOn)
            {
                if(GUILayout.Button("Stop Game"))
                {
                    _isDispalyOn = false;
                    GameEventManager.TriggerEvent(new GameStatesEvent{
                        gameEventType = GameEventType.STOP});
                }
            }
        }

        public void OnEvent(GameStatesEvent eventType)
        {
            switch(eventType.gameEventType){
                case GameEventType.START:
                    DisplayHUD();
                    break;
            }
        }
    }

}
