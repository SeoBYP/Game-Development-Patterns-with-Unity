using UnityEngine;

namespace Chapter.Visitor
{
    public class PickUp : MonoBehaviour
    {
        public PowerUp powerUp;

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<BikeController>())
            {
                other.GetComponent<BikeController>().Accept(powerUp);
                Destroy(gameObject);
            }
        }

    }
}
