using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.Visitor
{
    public interface IVisitor
    {
        void Visit(BikeWeapon bikeWeapon);
        void Visit(BikeShield bikeWeapon);
        void Visit(BikeEngine bikeWeapon);
    }

}
