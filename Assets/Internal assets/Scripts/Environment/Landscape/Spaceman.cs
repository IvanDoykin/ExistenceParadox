using System;
using UnityEngine;

[RequireComponent(typeof(Coordinating))]
[RequireComponent(typeof(CoordinatingData))]
public class Spaceman : MonoBehaviour, ITick
{
    [SerializeField] ChunksUpdater ChunksUpdater;

    public delegate void CoordinatesChanging();
    public static event CoordinatesChanging CoordinatesChanged;
    
    public delegate void SendChanging(int x, int z);
    public static event SendChanging SendChange;

    private Coordinating coordinating;
    private CoordinatingData coordinatingData;

    private int previousX;
    private int previousZ;
    public TickData tickData { get; set; }

    private void Start()
    {
        tickData = new TickData();

        ManagerUpdate.AddTo(this);
        
        coordinating = GetComponent<Coordinating>();
        coordinatingData = GetComponent<CoordinatingData>();
        
        CoordinatesChanged += coordinating.SetUpCoordinates;
        CoordinatesChanged();

        SendChange += ChunksUpdater.ChunksUpdate;

        previousX = coordinatingData.x;
        previousZ = coordinatingData.z;
    }

    public void Tick()
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
