using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Chapter.Visitor
{
    public class GameController : MonoBehaviour, IVisitorElement
    {
        private List<IVisitorElement> _visitorElements = new List<IVisitorElement>();

        private void Start()
        {
            _visitorElements.Add(gameObject.AddComponent<Shield>());
            _visitorElements.Add(gameObject.AddComponent<Shoose>());
            _visitorElements.Add(gameObject.AddComponent<Weapon>());
        }

        public void Accept(IVisitor visitor)
        {
            foreach(IVisitorElement element in _visitorElements)
            {
                element.Accept(visitor);
            }
        }
    }
}
