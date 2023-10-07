using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.EventBus
{
    public enum GameEventType
    {
        COUNTDOWN,
        START,
        RESTART,
        PAUSE,
        STOP,
        FINISH,
        QUIT,
    }

    public struct GameStatesEvent
    {
        public GameEventType gameEventType;

        public GameStatesEvent(GameEventType gameEventType)
        {
            this.gameEventType = gameEventType;
        }

        static GameStatesEvent e;

        public static void Trigger(GameEventType gameEventType)
        {
            e.gameEventType = gameEventType;
            GameEventManager.TriggerEvent<GameStatesEvent>(e);
        }
    }

}
