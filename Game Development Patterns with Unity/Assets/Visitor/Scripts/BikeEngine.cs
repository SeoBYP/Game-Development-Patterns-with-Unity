using System;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace Chapter.Visitor
{
    public class BikeEngine : MonoBehaviour, IBikeElement
    {
        public float turboBoost = 25.0f;
        public float maxTurboBoost = 200.0f;

        private bool _isTurboOn;
        private float _defaultSpeed = 300.0f;

        public float CurrentSpeed
        {
            get
            {
                if (_isTurboOn)
                    return _defaultSpeed + turboBoost;
                return _defaultSpeed;
            }
        }

        public void ToggleTurbo()
        {
            _isTurboOn = !_isTurboOn;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        private void OnGUI()
        {
            GUI.color = Color.green;

            GUI.Label(new Rect(125, 0, 200, 20), " Turbo Boost :" + turboBoost);
        }
    }
}
