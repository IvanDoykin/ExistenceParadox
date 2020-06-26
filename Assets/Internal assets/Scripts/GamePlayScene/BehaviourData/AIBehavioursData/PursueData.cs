using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public class PursueData : Data
{
    public NavMeshAgent navMeshAgent;
    public GameObject player;
}