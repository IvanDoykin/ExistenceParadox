using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IEventTrigger
{
    void TriggerEvent(string eventName);
}
