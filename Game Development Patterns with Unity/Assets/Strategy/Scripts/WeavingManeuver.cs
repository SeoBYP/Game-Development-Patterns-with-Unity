using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.Strategy
{
    public class WeavingManeuver : MonoBehaviour, IManeuverBehaviour
    {
        public void Manenuver(Drone drone)
        {
            StartCoroutine(Weave(drone));
        }

        IEnumerator Weave(Drone drone)
        {
            float time = 0;
            bool isReverse = false;
            float speed = drone.speed;
            Vector3 startPosition = drone.transform.position;
            Vector3 endPosition = startPosition;
            endPosition.z = drone.weavingDistance;

            while(true)
            {
                time = 0;
                Vector3 start = drone.transform.position;
                Vector3 end = (isReverse) ? startPosition : endPosition;
                while (time < speed)
                {
                    drone.transform.position = Vector3.Lerp(start, end, time / speed);
                    time += Time.deltaTime;
                    yield return null;
                }

                yield return new WaitForSeconds(1);
                isReverse= !isReverse;
            }

        }

    }
}
