using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.Visitor
{
    public class ClientVisitor : MonoBehaviour
    {
        public PowerUp shoosePowerUp;
        public PowerUp shieldPowerUp;
        public PowerUp weaponPowerUp;

        private GameController _gameController;

        void Start()
        {
            _gameController = gameObject.AddComponent<GameController>();
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Power UP Shield"))
                _gameController.Accept(shieldPowerUp);
            if (GUILayout.Button("Power UP Shoose"))
                _gameController.Accept(shoosePowerUp);
            if (GUILayout.Button("Power UP Weapon"))
                _gameController.Accept(weaponPowerUp);
        }
    }

}
