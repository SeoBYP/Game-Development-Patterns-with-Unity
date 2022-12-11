using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.EventBus
{
    public class HUDController : MonoBehaviour
    {
        private bool _isDispalyOn;

        private void OnEnable()
        {
            RaceEventBus.Subscribe(RaceEventType.START, DisplayHUD);
        }

        private void OnDisable()
        {
            RaceEventBus.Unsubscribe(RaceEventType.START, DisplayHUD);
        }

        private void DisplayHUD()
        {
            _isDispalyOn = true;
        }

        private void OnGUI()
        {
            if (_isDispalyOn)
            {
                if(GUILayout.Button("Stop Race"))
                {
                    _isDispalyOn = false;
                    RaceEventBus.Publish(RaceEventType.STOP);
                }
            }
        }
    }

}
