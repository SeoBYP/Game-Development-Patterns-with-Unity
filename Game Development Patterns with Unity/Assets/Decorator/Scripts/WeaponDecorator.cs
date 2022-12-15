using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.Decorator
{
    public class WeaponDecorator : IWeapon
    {
        private readonly IWeapon _decoratedWeapon;
        private readonly WeaponAttachment _attachment;

        public WeaponDecorator(IWeapon decoratedWeapon, WeaponAttachment attachment)
        {
            _decoratedWeapon = decoratedWeapon;
            _attachment = attachment;
        }

        public float Rate => (_decoratedWeapon.Rate + _attachment.Rate);
        public float Range => (_decoratedWeapon.Range + _attachment.Range);

        public float Strength => (_decoratedWeapon.Strength + _attachment.Strength);

        public float Cooldown => (_decoratedWeapon.Cooldown + _attachment.Cooldown);
    }
}
