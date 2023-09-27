using System;
using UnityEngine;

namespace Chapter.Visitor
{
    public class Shield : MonoBehaviour, IVisitorElement
    {
        public float health = 50.0f;

        public float Damage(float damage)
        {
            health -= damage;
            return health;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        private void OnGUI()
        {
            GUI.color = Color.green;

            GUI.Label(new Rect(125, 20, 200, 20), " Shield Health :" + health);
        }
    }
}
