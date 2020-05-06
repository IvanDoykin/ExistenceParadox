using UnityEngine;

[RequireComponent(typeof(CoordinatesData))]

public class Coordinating : MonoBehaviour
{
    private CoordinatesData coordinatesData;

<<<<<<< HEAD
    private void Start()
    {
        coordinatesData = GetComponent<CoordinatesData>();
        SetUpCoordinates();
    }

=======
    public static event Spaceman.CoordinatesChanging CoordinatesChanged;

    private void Start()
    {
        SetUpCoordinates();
    }

    private void OnEnable()
    {
        coordinatesData = GetComponent<CoordinatesData>();
    }

>>>>>>> Chunk_Gen
    public void SetUpCoordinates()
    {
        coordinatesData.x = (int)Mathf.Floor(transform.position.x / 16);
        coordinatesData.z = (int)Mathf.Floor(transform.position.z / 16);
    }
<<<<<<< HEAD
=======

    
>>>>>>> Chunk_Gen
}
