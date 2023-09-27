using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Chapter.Visitor
{
    // CreateAssetMenu 속성을 사용하여 에디터에서 생성 가능한 파워업 스크립터블 오브젝트를 정의합니다.
    [CreateAssetMenu(fileName = "PowerUP", menuName = "PowerUP")]
    public class PowerUp : ScriptableObject, IVisitor
    {
        // 파워업의 속성들을 정의합니다.
        public string powerupName;
        public GameObject powerupPrefab;
        public string powerupDescription;
        [Tooltip("실드를 완전히 회복합니다.")]
        public bool healShield;

        [Range(0.0f, 50f)]
        [Tooltip("스프린트 설정을 최대 50만큼 높입니다.")]
        public float Boost;
        [Range(0.0f, 25)]
        [Tooltip("무기 사거리를 최대 25만큼 늘립니다.")]
        public int weaponRange;
        [Range(0.0f, 50f)]
        [Tooltip("무기 강도를 최대 50%만큼 높입니다.")]
        public float weaponStrength;

        // 방문자 패턴에 따라 무기 객체를 방문하는 Visit 메서드입니다.
        public void Visit(Weapon weapon)
        {
            // 무기의 사거리를 늘립니다.
            int range = weapon.range += weaponRange;
            if (range >= weapon.maxRange)
                weapon.range = weapon.maxRange;
            else
                weapon.range = range;

            // 무기의 강도를 늘립니다.
            float strength = weapon.strength +=
                Mathf.Round(weapon.strength * weaponStrength / 100);
            if (strength <= weapon.maxStrength)
                weapon.strength = weapon.maxStrength;
            else
                weapon.strength = strength;
        }

        // 방문자 패턴에 따라 실드 객체를 방문하는 Visit 메서드입니다.
        public void Visit(Shield shield)
        {
            // 실드를 회복합니다.
            if (healShield)
                shield.health = 100.0f;
        }

        // 방문자 패턴에 따라 신발 객체를 방문하는 Visit 메서드입니다.
        public void Visit(Shoose shoose)
        {
            // 스프린트를 적용합니다.
            float boost = shoose.Boost += Boost;
            if (boost < 0.0f) shoose.Boost = 0.0f;
            if (boost >= shoose.maxBoost)
                shoose.Boost = shoose.maxBoost;
        }
    }
}
