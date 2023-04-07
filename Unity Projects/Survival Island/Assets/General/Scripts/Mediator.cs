using System;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivalIsland.Utils
{
    public abstract class Mediator<T> : MonoBehaviour where T : Mediator<T>
    {
        private static T _instance;
        protected Mediator() { }

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();
                }
                return _instance;
            }
        }

        private readonly List<Action<T>> subscribers = new();

        public void Subscribe(Action<T> subscriber)
        {
            subscribers.Add(subscriber);
        }

        public void Unsubscribe(Action<T> subscriber)
        {
            subscribers.Remove(subscriber);
        }

        protected void Publish(T publisher)
        {
            foreach (var subscriber in subscribers)
            {
                subscriber?.Invoke(publisher);
            }
        }
    }
}