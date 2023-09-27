using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.Visitor
{
    public interface IVisitor
    {
        void Visit(Weapon weapon);
        void Visit(Shield shield);
        void Visit(Shoose shoose);
    }
}
