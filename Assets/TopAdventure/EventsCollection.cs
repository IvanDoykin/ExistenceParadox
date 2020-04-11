using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EventsCollection", menuName = "EventsCollection")]
public class EventsCollection : ScriptableObject
{
    [SerializeField]
    public string currentEvent;
}
