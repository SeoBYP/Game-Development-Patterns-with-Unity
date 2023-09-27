using System;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace Chapter.Visitor
{
    public class Shoose : MonoBehaviour, IVisitorElement
    {
        public float Boost = 25.0f;
        public float maxBoost = 200.0f;

        private bool _isSprintOn;
        private float _defaultSpeed = 300.0f;

        public float CurrentSpeed
        {
            get
            {
                if (_isSprintOn)
                    return _defaultSpeed + Boost;
                return _defaultSpeed;
            }
        }

        public void ToggleSprint()
        {
            _isSprintOn = !_isSprintOn;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        private void OnGUI()
        {
            GUI.color = Color.green;

            GUI.Label(new Rect(125, 0, 200, 20), " Sprint :" + Boost);
        }
    }
}
