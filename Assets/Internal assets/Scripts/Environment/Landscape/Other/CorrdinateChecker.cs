using System;
using UnityEngine;

[RequireComponent(typeof(Coordinating))]
[RequireComponent(typeof(CoordinatingData))]
public class CorrdinateChecker : MonoBehaviour
{
    [SerializeField] private ChunksUpdater ChunksUpdater;

    public delegate void CoordinatesChanging();
    public static event CoordinatesChanging CoordinatesChanged;
    
    public delegate void SendChanging(int x, int z);
    public static event SendChanging SendChange;

    private Coordinating coordinating;
    private CoordinatingData coordinatingData;

    private int previousX;
    private int previousZ;

    private void Start()
    {        
        coordinating = GetComponent<Coordinating>();
        coordinatingData = GetComponent<CoordinatingData>();
        
        CoordinatesChanged += coordinating.SetUpCoordinates;
        CoordinatesChanged();

        SendChange += ChunksUpdater.ChunksUpdate;

        previousX = coordinatingData.x;
        previousZ = coordinatingData.z;
    }

    public void Update()
    {
        if ((previousX != coordinatingData.x) || (previousZ != coordinatingData.z))
        {
            SendChange(coordinatingData.x - previousX, coordinatingData.z - previousZ);

            previousX = coordinatingData.x;
            previousZ = coordinatingData.z;
        }

        CoordinatesChanged();
    }
}
