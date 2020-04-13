using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IEventSub
{
    void StartListening(string eventName, UnityAction listener);
    void StopListening(string eventName, UnityAction listener);
    void Subscribe();
    void UnSubscribe();
}
