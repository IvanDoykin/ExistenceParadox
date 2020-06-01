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

    private void RemakeNavMesh()
    {
        _navigation.BuildNavMesh();
    }

    public void Subscribe()
    {
        ManagerEvents.StartListening(chunkCreated.currentEvent, RemakeNavMesh);
    }

    public void UnSubscribe()
    {
        ManagerEvents.StopListening(chunkCreated.currentEvent, RemakeNavMesh);
    }
}