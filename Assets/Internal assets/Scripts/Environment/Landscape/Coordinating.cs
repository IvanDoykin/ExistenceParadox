using UnityEngine;

[RequireComponent(typeof(CoordinatingData))]
public class Coordinating : MonoBehaviour
{
    private CoordinatingData coordinatesData;

    public static event Spaceman.CoordinatesChanging CoordinatesChanged;

    private void Awake()
    {
        coordinatesData = GetComponent<CoordinatingData>();
        SetUpCoordinates();
    }

    //Calculate coordinates units into chunks //Example: 17;13 = 2;1
    public void SetUpCoordinates()
    {
        coordinatesData.x = (int)Mathf.Floor(transform.position.x / ChunkData.Metric);
        coordinatesData.z = (int)Mathf.Floor(transform.position.z / ChunkData.Metric);
    }
}
