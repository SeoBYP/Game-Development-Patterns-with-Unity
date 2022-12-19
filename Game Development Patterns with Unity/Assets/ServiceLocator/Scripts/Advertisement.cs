using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.ServiceLocator
{
    public class Advertisement : IAdvertisement
    {
        public void DisplayAd()
        {
            Debug.Log("Displaying video advertisement");
        }
    }
}
