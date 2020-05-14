using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class NavMeshGenerator : MonoBehaviour, IEventSub
{
                        //not need in empty line
    [SerializeField]
    private EventsCollection _chunkCreated; //with _
    private NavMeshSurface navigation; //without _
    //let's give private name without _ 
    void Awake()
    {
        navigation = GetComponent<NavMeshSurface>();
        Subscribe();
    } //add empty line
    public void StartListening(string eventName, UnityAction listener)
    {
        ManagerEvents.StartListening(eventName, listener);
    }

    public void StopListening(string eventName, UnityAction listener)
    {
        ManagerEvents.StopListening(eventName, listener);
    }

    void RebakenavMesh() //better RebakeNavMesh
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
