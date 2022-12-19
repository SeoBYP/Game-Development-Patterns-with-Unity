using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.ServiceLocator
{
    public static class ServiceLocator 
    {
        public static readonly IDictionary<Type,object> Services = new Dictionary<Type,object>();
        public static void RegisterService<T>(T service)
        {
            if (!Services.ContainsKey(typeof(T)))
            {
                Services[typeof(T)] = service;
            }
            else
            {
                throw new ApplicationException("Service already registered");
            }
        }
        public static T GetService<T>()
        {
            try
            {
                return (T)Services[typeof(T)];
            }
            catch
            {
                throw new ApplicationException("Requested service not found");
            }
        }
    }
}
