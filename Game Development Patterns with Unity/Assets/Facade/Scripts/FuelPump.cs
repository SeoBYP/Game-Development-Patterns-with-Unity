using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.Facade
{
    public class FuelPump : MonoBehaviour
    {
        public BikeEngine engine;
        public IEnumerator burnFuel;
        void Start()
        {
            burnFuel = BurnFuel();
        }

        IEnumerator BurnFuel()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                engine.fuelAmount -= engine.burnRate;
                if(engine.fuelAmount <= 0.0f)
                {
                    engine.TurnOff();
                    yield return 0;
                }
            }
        }

        private void OnGUI()
        {
            GUI.color = Color.green;
            GUI.Label(new Rect(100, 40, 500, 20), "Fuel : " + engine.fuelAmount);
        }
    }

}
