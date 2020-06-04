using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using Object = System.Object;

[CreateAssetMenu(fileName = "ManagerEvents", menuName = "Managers/ManagerEvents")]
public class ManagerEvents : ManagerBase
{
    private Dictionary<string, UnityEvent> _eventsDictionary;
    private Dictionary<string, OneArgumentEvent> _oneArgumentEventsDictionary;
    private Dictionary<string, TwoArgumentEvent> _twoArgumentsEventsDictionary;
    private Dictionary<string, ThreeArgumentEvent> _threeArgumentsEventsDictionary;
    private Dictionary<string, FourArgumentEvent> _fourArgumentsEventsDictionary;


    private class OneArgumentEvent : UnityEvent<object>
    {
    }

    private class TwoArgumentEvent : UnityEvent<object, object>
    {
    }

    private class ThreeArgumentEvent : UnityEvent<object, object, object>
    {
    }

    private class FourArgumentEvent : UnityEvent<object, object, object, object>
    {
    }


    private void OnEnable()
    {
        _eventsDictionary = new Dictionary<string, UnityEvent>();
        _oneArgumentEventsDictionary = new Dictionary<string, OneArgumentEvent>();
        _twoArgumentsEventsDictionary = new Dictionary<string, TwoArgumentEvent>();
        _threeArgumentsEventsDictionary = new Dictionary<string, ThreeArgumentEvent>();
        _fourArgumentsEventsDictionary = new Dictionary<string, FourArgumentEvent>();
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

    public static void StartListening(string eventName, UnityAction<object> listener)
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

    public static void StartListening(string eventName, UnityAction<object, object> listener)
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

    public static void StartListening(string eventName, UnityAction<object, object, object> listener)
    {
        ManagerEvents mngEvents = Toolbox.Get<ManagerEvents>();
        //We need to create place in memory for reference to the object 

        if (mngEvents._threeArgumentsEventsDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            //If there is no event with this name add new one to dictionary.
            thisEvent = new ThreeArgumentEvent();
            thisEvent.AddListener(listener);
            mngEvents._threeArgumentsEventsDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StartListening(string eventName, UnityAction<object, object, object, object> listener)
    {
        ManagerEvents mngEvents = Toolbox.Get<ManagerEvents>();
        //We need to create place in memory for reference to the object 

        if (mngEvents._fourArgumentsEventsDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            //If there is no event with this name add new one to dictionary.
            thisEvent = new FourArgumentEvent();
            thisEvent.AddListener(listener);
            mngEvents._fourArgumentsEventsDictionary.Add(eventName, thisEvent);
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

    public static void StopListening(string eventName, UnityAction<object> listener)
    {
        ManagerEvents mngEvents = Toolbox.Get<ManagerEvents>();
        if (mngEvents._oneArgumentEventsDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void StopListening(string eventName, UnityAction<object, object> listener)
    {
        ManagerEvents mngEvents = Toolbox.Get<ManagerEvents>();
        if (mngEvents._twoArgumentsEventsDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void StopListening(string eventName, UnityAction<object, object, object> listener)
    {
        ManagerEvents mngEvents = Toolbox.Get<ManagerEvents>();
        if (mngEvents._threeArgumentsEventsDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void StopListening(string eventName, UnityAction<object, object, object, object> listener)
    {
        ManagerEvents mngEvents = Toolbox.Get<ManagerEvents>();
        if (mngEvents._fourArgumentsEventsDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    private static void TriggerEvent(string eventName)
    {
        ManagerEvents mngEvents = Toolbox.Get<ManagerEvents>();
        if (mngEvents._eventsDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent.Invoke();
        }
    }

    private static void TriggerEvent(string eventName, object argument)
    {
        ManagerEvents mngEvents = Toolbox.Get<ManagerEvents>();
        if (mngEvents._oneArgumentEventsDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent.Invoke(argument);
        }
    }

    private static void TriggerEvent(string eventName, object argument, object secondArgument)
    {
        ManagerEvents mngEvents = Toolbox.Get<ManagerEvents>();
        if (mngEvents._twoArgumentsEventsDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent.Invoke(argument, secondArgument);
        }
    }

    private static void TriggerEvent(string eventName, object argument, object secondArgument, object thirdArgument)
    {
        ManagerEvents mngEvents = Toolbox.Get<ManagerEvents>();
        if (mngEvents._threeArgumentsEventsDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent.Invoke(argument, secondArgument, thirdArgument);
        }
    }

    private static void TriggerEvent(string eventName, object argument, object secondArgument, object thirdArgument,
        object fourthArgument)
    {
        ManagerEvents mngEvents = Toolbox.Get<ManagerEvents>();
        if (mngEvents._fourArgumentsEventsDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent.Invoke(argument, secondArgument, thirdArgument, fourthArgument);
        }
    }

    public static void CheckTriggeringEvent(string eventName, params Object[] arguments)
    {
        switch (arguments.Length)
        {
            case 0:
                TriggerEvent(eventName);
                break;
            case 1:
                TriggerEvent(eventName, arguments[0]);
                break;
            case 2:
                TriggerEvent(eventName, arguments[0], arguments[1]);
                break;
            case 3:
                TriggerEvent(eventName, arguments[0], arguments[1], arguments[2]);
                break;
            case 4:
                TriggerEvent(eventName, arguments[0], arguments[1], arguments[2], arguments[3]);
                break;
        }
    }
}