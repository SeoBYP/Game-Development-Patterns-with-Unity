using UnityEngine;

namespace Chapter.Observer
{
    public class CameraController : Observer
    {
        private bool _isTurboOn;
        private Vector3 _initialPosition;
        private float _shakemagnitude = 0.1f;
        private BikeController _bikeController;

        private void OnEnable()
        {
            _initialPosition = gameObject.transform.localPosition;
        }

        private void Update()
        {
            if (_isTurboOn)
            {
                gameObject.transform.localPosition =
                    _initialPosition + (Random.insideUnitSphere * _shakemagnitude);
            }
            else
            {
                gameObject.transform.localPosition = _initialPosition;
            }
        }

        public override void Notify(Subject subject)
        {
            if(!_bikeController)
                _bikeController = subject.GetComponent<BikeController>();
            if(_bikeController)
                _isTurboOn = _bikeController.IsTurboOn;
        }
    }
}
