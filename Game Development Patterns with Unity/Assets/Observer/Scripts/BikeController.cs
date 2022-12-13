using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Chapter.Observer
{
    public class BikeController : Subject
    {
        public bool IsTurboOn
        {
            get; private set;
        }
        public float CurrentHealth
        {
            get { return _health; }
        }

        private bool _isEngineOn;
        private HUDController _hudController;
        private CameraController _cameraController;
        [SerializeField]
        private float _health = 100.0f;

        private void Awake()
        {
            _hudController = gameObject.AddComponent<HUDController>();
            _cameraController = gameObject.AddComponent<CameraController>();
        }

        private void Start()
        {
            StartEngine();
        }

        private void OnEnable()
        {
            if (_hudController)
                Attact(_hudController);
            if (_cameraController)
                Attact(_cameraController);
        }

        private void OnDisable()
        {
            if (_hudController)
                Detach(_hudController);
            if (_cameraController)
                Detach(_cameraController);
        }

        private void StartEngine()
        {
            _isEngineOn = true;
            NotifyObservers();
        }

        public void ToggleTurbo()
        {
            if (_isEngineOn)
                IsTurboOn = !IsTurboOn;

            NotifyObservers();
        }

        public void TakeDamage(float amount)
        {
            _health -= amount;
            IsTurboOn = false;

            NotifyObservers();

            if (_health < 0)
                Destroy(gameObject);
        }

    }
}
