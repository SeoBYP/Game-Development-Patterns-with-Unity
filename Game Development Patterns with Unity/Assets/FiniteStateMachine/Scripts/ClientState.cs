using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Chapter.State
{
    public class ClientState : MonoBehaviour
    {
        private BikeController _bikeController;
        // Start is called before the first frame update
        void Start()
        {
            _bikeController = (BikeController)FindObjectOfType(typeof(BikeController));
        }

        private void OnGUI()
        {
            if(GUILayout.Button("Start Bike"))
                _bikeController.StartBike();
            if (GUILayout.Button("Trun Left"))
                _bikeController.Turn(Direction.Left);
            if (GUILayout.Button("Trun Right"))
                _bikeController.Turn(Direction.Right);
            if (GUILayout.Button("Stop Bike"))
                _bikeController.StopBike();
        }
    }
}