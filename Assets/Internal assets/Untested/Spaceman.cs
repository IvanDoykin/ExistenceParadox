using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Coordinating))]
public class Spaceman : MonoBehaviour
{
    public delegate void ChangingCoordinates();
    public static ChangingCoordinates CoordinatesChanged;

    //add event for generator - event(x,z) when player changes coordinates

    private CoordinatesData coordinates;
    private Coordinating coordinating;

    private int previousCoordinateX;
    private int previousCoordinateZ;
    private void Start()
    {
        coordinates = GetComponent<CoordinatesData>();
        coordinating = GetComponent<Coordinating>();

        CoordinatesChanged += coordinating.SetUpCoordinates;

        previousCoordinateX = coordinates.x;
        previousCoordinateZ = coordinates.z;
    }

    private void Update()
    {
        if((previousCoordinateX != coordinates.x) || (previousCoordinateZ != coordinates.z))
        {
            //event
            Debug.Log("Call event!");
            previousCoordinateX = coordinates.x;
            previousCoordinateZ = coordinates.z;
        }

        CoordinatesChanged();
    }
}
