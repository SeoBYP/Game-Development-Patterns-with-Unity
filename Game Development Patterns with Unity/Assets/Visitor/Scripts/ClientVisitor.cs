using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.Visitor
{
    public class ClientVisitor : MonoBehaviour
    {
        public PowerUp enginePowerUp;
        public PowerUp shieldPowerUp;
        public PowerUp weaponPowerUp;

        private BikeController _bikeController;

        void Start()
        {
            _bikeController = gameObject.AddComponent<BikeController>();
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Power UP Shield"))
                _bikeController.Accept(shieldPowerUp);
            if (GUILayout.Button("Power UP Engine"))
                _bikeController.Accept(enginePowerUp);
            if (GUILayout.Button("Power UP Weapon"))
                _bikeController.Accept(weaponPowerUp);
        }
    }

}
