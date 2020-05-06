using UnityEngine;

[RequireComponent(typeof(CoordinatesData))]

public class Coordinating : MonoBehaviour
{
    private CoordinatesData coordinatesData;

    public static event Spaceman.CoordinatesChanging CoordinatesChanged;

    private void Start()
    {
        SetUpCoordinates();
    }

    private void OnEnable()
    {
        coordinatesData = GetComponent<CoordinatesData>();
    }

    public void SetUpCoordinates()
    {
        coordinatesData.x = (int)Mathf.Floor(transform.position.x / 16);
        coordinatesData.z = (int)Mathf.Floor(transform.position.z / 16);
    }

    
}
