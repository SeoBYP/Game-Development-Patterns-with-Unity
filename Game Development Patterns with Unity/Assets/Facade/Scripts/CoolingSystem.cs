using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.Facade
{
    public class CoolingSystem : MonoBehaviour
    {
        public BikeEngine engine;
        public IEnumerator coolEngine;
        private bool _isPaused;

        private void Start()
        {
            coolEngine = CoolEngine();
        }

        public void PauseCooling()
        {
            _isPaused = !_isPaused;
        }
        public void ResetTemperature()
        {
            engine.currentTemp = 0;
        }

        IEnumerator CoolEngine()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                if (!_isPaused)
                {
                    if (engine.currentTemp > engine.minTemp)
                        engine.currentTemp -= engine.tempRate;
                    if (engine.currentTemp < engine.minTemp)
                        engine.currentTemp += engine.tempRate;
                }
                else
                    engine.currentTemp += engine.tempRate;

                if (engine.currentTemp > engine.maxTemp) engine.TurnOff();
            }
        }

        private void OnGUI()
        {
            GUI.color = Color.green;
            GUI.Label(new Rect(100, 20, 500, 20), "Temp : " + engine.currentTemp);
        }
    }
}
