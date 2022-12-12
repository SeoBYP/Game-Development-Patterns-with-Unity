using Chapter.Command;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Chapter.Command
{
    public class TurnRight : Command
    {
        private BikeController _controller;
        public TurnRight(BikeController controller)
        {
            _controller = controller;
        }
        public override void Execute()
        {
            _controller.Turn(BikeController.Direction.Right);
        }
    }
}
