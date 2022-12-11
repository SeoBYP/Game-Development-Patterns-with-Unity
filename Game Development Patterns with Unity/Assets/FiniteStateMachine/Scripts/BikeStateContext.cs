using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.State
{
    public interface IBikeState
    {
        void Handle(BikeController controller);
    }

    public enum Direction
    {
        Left = -1,
        Right = 1,
    }

    public class BikeStateContext 
    {
        public IBikeState CurrentState { get; set; }

        private readonly BikeController _controller;

        public BikeStateContext(BikeController controller)
        {
            _controller = controller;
        }

        public void Transition()
        {
            CurrentState.Handle(_controller);
        }

        public void Transition(IBikeState state)
        {
            CurrentState = state;
            CurrentState.Handle(_controller);
        }

    }

}
