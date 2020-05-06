using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Coordinating))]
public class Spaceman : MonoBehaviour
{
    [SerializeField] ChunksBlock chunksController;

    public delegate void CoordinatesChanging();
    public static event CoordinatesChanging CoordinatesChanged;

    public delegate void SendChanging(int x, int z);
    public static event SendChanging SendChange;

    private Coordinating coordinating;
    private CoordinatesData coordinatesData;

    private int previousX;
    private int previousZ;

    private void Start()
    {
        coordinating = GetComponent<Coordinating>();
        coordinatesData = GetComponent<CoordinatesData>();

        CoordinatesChanged += coordinating.SetUpCoordinates;
        CoordinatesChanged();

        SendChange += chunksController.ChunksUpdate;

        previousX = coordinatesData.x;
        previousZ = coordinatesData.z;
    }

    private void Update()
    {
        if ((previousX != coordinatesData.x) || (previousZ != coordinatesData.z))
        {
            SendChange(coordinatesData.x - previousX, coordinatesData.z - previousZ);
            Debug.Log("call event");

            previousX = coordinatesData.x;
            previousZ = coordinatesData.z;
        }

        CoordinatesChanged();
    }
}
