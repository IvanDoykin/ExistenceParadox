using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ManagerEvents", menuName = "Managers/ManagerEvents")]
public class ManagerEvents : ManagerBase
{
    [SerializeField] private List<string> eventsList;

    private Dictionary<string, UnityEvent> eventsDictionary;
    // private static ManagerEvents mngEvents;


    private void OnEnable()
    {
        eventsDictionary = new Dictionary<string, UnityEvent>();
        // mngEvents = Toolbox.Get<ManagerEvents>();
    }

    public static void StartListening(string eventName, UnityAction listener)
    {
        ManagerEvents mngEvents = Toolbox.Get<ManagerEvents>();
        //We need to create place in memory for reference to the object 
        UnityEvent thisEvent = null; //свободное местечко для слушателя события, т.е для подписчика

        if (mngEvents.eventsDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            //If there is no event with this name add new one to dictionary.
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            mngEvents.eventsDictionary.Add(eventName, thisEvent);
            mngEvents.eventsList.Add(eventName);
        }
    }

    public static void StopListening(string eventName, UnityAction listener)
    {
        ManagerEvents mngEvents = Toolbox.Get<ManagerEvents>();
        UnityEvent thisEvent = null;
        if (mngEvents.eventsDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
            mngEvents.eventsList.Remove(eventName);
        }
    }

    public static void TriggerEvent(string eventName)
    {
        ManagerEvents mngEvents = Toolbox.Get<ManagerEvents>();
        UnityEvent thisEvent = null;
        if (mngEvents.eventsDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke();
        }
    }
}