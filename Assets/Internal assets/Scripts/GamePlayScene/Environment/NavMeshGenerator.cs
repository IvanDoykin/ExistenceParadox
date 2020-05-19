using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class NavMeshGenerator : MonoBehaviour, IEventSub
{
    [SerializeField] private EventsCollection chunkCreated;
    private NavMeshSurface _navigation;

    void Awake()
    {
        _navigation = GetComponent<NavMeshSurface>();
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

    private void RemakeNavMesh()
    {
        _navigation.BuildNavMesh();
    }

    public void Subscribe()
    {
        StartListening(chunkCreated.currentEvent, RemakeNavMesh);
    }

    public void UnSubscribe()
    {
        StopListening(chunkCreated.currentEvent, RemakeNavMesh);
    }
}