using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Chapter.State
{
    public class BikeTurnState : MonoBehaviour,IBikeState
    {
        private Vector3 _turnDirection;
        private BikeController _bikeController;

        public void Handle(BikeController controller)
        {
            if (!_bikeController)
                _bikeController = controller;

            _turnDirection.x = (float)_bikeController.CurrentTurnDirection;
            if(_bikeController.CurrentSpeed > 0)
            {
                transform.Translate(_turnDirection * _bikeController._turnDistance);
            }
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}