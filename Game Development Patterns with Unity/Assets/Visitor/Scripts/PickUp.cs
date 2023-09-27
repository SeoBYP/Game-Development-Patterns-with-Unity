using UnityEngine;

namespace Chapter.Visitor
{
    public class PickUp : MonoBehaviour
    {
        public PowerUp powerUp;

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<GameController>())
            {
                other.GetComponent<GameController>().Accept(powerUp);
                Destroy(gameObject);
            }
        }

    }
}
