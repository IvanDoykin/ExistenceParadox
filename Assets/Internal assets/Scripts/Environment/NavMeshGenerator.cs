using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class NavMeshGenerator : MonoBehaviour, IEventSub
{

    [SerializeField]
    private EventsCollection _chunkCreated;
    private NavMeshSurface navigation;
    void Awake()
    {
        navigation = GetComponent<NavMeshSurface>();
        Subscribe();
    }
    public void StartListening(string eventName, UnityAction listener)
    {
        ManagerEvents.StartListening(eventName, listener);
    }

    public void StopListening(string eventName, UnityAction listener)
    {
        ManagerEvents.StopListening(eventName, listener);
    }

    void RebakenavMesh()
    {
        navigation.BuildNavMesh();
    }

    public void Subscribe()
    {
        StartListening(_chunkCreated.currentEvent, RebakenavMesh);
    }

    public void UnSubscribe()
    {
        StopListening(_chunkCreated.currentEvent, RebakenavMesh);
    }
}
