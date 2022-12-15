using System.Collections;
using UnityEngine;

namespace Chapter.Decorator
{
    public class BikeWeapon : MonoBehaviour
    {
        public WeaponConfig weaponConfig;
        public WeaponAttachment mainAttachment;
        public WeaponAttachment secondaryAttachment;

        private bool _isFiring;
        private IWeapon _weapon;
        private bool _isDecorated;

        private void Start()
        {
            _weapon = new Weapon(weaponConfig);
        }

        private void OnGUI()
        {
            GUI.color = Color.green;

            GUI.Label(new Rect(5, 50, 150, 100), "Range:" + _weapon.Range);

            GUI.Label(new Rect(5, 70, 150, 100), "Range:" + _weapon.Strength);

            GUI.Label(new Rect(5, 90, 150, 100), "Range:" + _weapon.Cooldown);

            GUI.Label(new Rect(5, 110, 150, 100), "Range:" + _weapon.Rate);

            GUI.Label(new Rect(5, 130, 150, 100), "Range:" + _isFiring);
            
            if(mainAttachment && _isDecorated)
            {
                GUI.Label(new Rect(5, 150, 150, 100), "Main Attachment : " + mainAttachment.name);
            }
            if (secondaryAttachment && _isDecorated)
            {
                GUI.Label(new Rect(5, 170, 150, 100), "Secondary Attachment : " + secondaryAttachment.name);
            }
        }

        public void ToggleFire()
        {
            _isFiring = !_isFiring;
            if (_isFiring)
                StartCoroutine(FireWeapon());
        }
        IEnumerator FireWeapon()
        {
            float firingRate = 1.0f / _weapon.Rate;

            while (_isFiring)
            {
                yield return new WaitForSeconds(firingRate);
                Debug.Log("Fire");
            }
        }

        public void Reset()
        {
            _weapon = new Weapon(weaponConfig);
            _isDecorated = !_isDecorated;
        }

        public void Decorate()
        {
            if (mainAttachment && !secondaryAttachment)
            {
                _weapon = new WeaponDecorator(_weapon, mainAttachment);
            }
            if (mainAttachment && secondaryAttachment)
            {
                _weapon = new WeaponDecorator(new WeaponDecorator(_weapon, mainAttachment), secondaryAttachment);
            }

            _isDecorated = !_isDecorated;
        }

    }
}
