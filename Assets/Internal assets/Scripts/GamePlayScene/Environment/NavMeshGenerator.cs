using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class NavMeshGenerator : MonoBehaviour, IEventSub
{
    [SerializeField] private EventsCollection chunkCreated;
    [SerializeField] private GameObject person;

    private NavMeshSurface _navigation;

    private const float needLength = 24f;

    void Awake()
    {
        _navigation = GetComponent<NavMeshSurface>();
        Subscribe();
    }

    void Update()
    {
        if(Vector3.Distance(gameObject.transform.position, person.transform.position) >= needLength)
        {
            gameObject.transform.position = person.transform.position;
            RemakeNavMesh();
        }
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