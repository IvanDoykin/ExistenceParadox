using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IArgumentativeEventTrigger
{
    void TriggerEvent(string eventName, params dynamic[] arguments);
}