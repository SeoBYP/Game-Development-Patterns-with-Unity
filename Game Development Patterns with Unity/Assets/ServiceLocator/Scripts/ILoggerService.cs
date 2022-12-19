using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.ServiceLocator
{
    public interface ILoggerService
    {
        void Log(string message);
    }
}
