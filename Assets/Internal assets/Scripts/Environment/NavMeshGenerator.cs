using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class NavMeshGenerator : MonoBehaviour
{
    [SerializeField] private GameObject person;

    private NavMeshSurface _navigation;

    private const float needLength = 24f;

    void Awake()
    {
        _navigation = GetComponent<NavMeshSurface>();
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
}