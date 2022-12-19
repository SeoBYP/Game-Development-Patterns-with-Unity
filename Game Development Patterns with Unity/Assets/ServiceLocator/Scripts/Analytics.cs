using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.ServiceLocator
{
    public class Analytics : IAnalyticsService
    {
        public void SendEvent(string eventname)
        {
            Debug.Log("Sent : " + eventname);
        }
    }
}
