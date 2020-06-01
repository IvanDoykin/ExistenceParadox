using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ManagerEvents", menuName = "Managers/ManagerEvents")]
public class ManagerEvents : ManagerBase
{
    private Dictionary<string, UnityEvent> _eventsDictionary;
    private Dictionary<string, OneArgumentEvent> _oneArgumentEventsDictionary;
    private Dictionary<string, TwoArgumentEvent> _twoArgumentsEventsDictionary;


    private class OneArgumentEvent : UnityEvent<dynamic>
    {
    }

    private class TwoArgumentEvent : UnityEvent<dynamic, dynamic>
    {
    }

    private void OnEnable()
    {
        _eventsDictionary = new Dictionary<string, UnityEvent>();
        _oneArgumentEventsDictionary = new Dictionary<string, OneArgumentEvent>();
        _twoArgumentsEventsDictionary = new Dictionary<string, TwoArgumentEvent>();

        // mngEvents = Toolbox.Get<ManagerEvents>();
    }

    public static void StartListening(string eventName, UnityAction listener)
    {
        ManagerEvents mngEvents = Toolbox.Get<ManagerEvents>();
        //We need to create place in memory for reference to the object 

        if (mngEvents._eventsDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            //If there is no event with this name add new one to dictionary.
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            mngEvents._eventsDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StartListening(string eventName, UnityAction<dynamic> listener)
    {
        ManagerEvents mngEvents = Toolbox.Get<ManagerEvents>();
        //We need to create place in memory for reference to the object 

        if (mngEvents._oneArgumentEventsDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            //If there is no event with this name add new one to dictionary.
            thisEvent = new OneArgumentEvent();
            thisEvent.AddListener(listener);
            mngEvents._oneArgumentEventsDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StartListening(string eventName, UnityAction<dynamic, dynamic> listener)
    {
        ManagerEvents mngEvents = Toolbox.Get<ManagerEvents>();
        //We need to create place in memory for reference to the object 

        if (mngEvents._twoArgumentsEventsDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            //If there is no event with this name add new one to dictionary.
            thisEvent = new TwoArgumentEvent();
            thisEvent.AddListener(listener);
            mngEvents._twoArgumentsEventsDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, UnityAction listener)
    {
        ManagerEvents mngEvents = Toolbox.Get<ManagerEvents>();
        if (mngEvents._eventsDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void StopListening(string eventName, UnityAction<dynamic> listener)
    {
        ManagerEvents mngEvents = Toolbox.Get<ManagerEvents>();
        if (mngEvents._oneArgumentEventsDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void StopListening(string eventName, UnityAction<dynamic, dynamic> listener)
    {
        ManagerEvents mngEvents = Toolbox.Get<ManagerEvents>();
        if (mngEvents._twoArgumentsEventsDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(string eventName)
    {
        ManagerEvents mngEvents = Toolbox.Get<ManagerEvents>();
        if (mngEvents._eventsDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent.Invoke();
        }
    }

    public static void TriggerEvent(string eventName, dynamic argument)
    {
        ManagerEvents mngEvents = Toolbox.Get<ManagerEvents>();
        if (mngEvents._oneArgumentEventsDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent.Invoke(argument);
        }
    }

    public static void TriggerEvent(string eventName, dynamic argument, dynamic secondArgument)
    {
        ManagerEvents mngEvents = Toolbox.Get<ManagerEvents>();
        if (mngEvents._twoArgumentsEventsDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent.Invoke(argument, secondArgument);
        }
    }
}