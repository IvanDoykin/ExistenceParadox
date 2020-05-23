using UnityEngine;

[RequireComponent(typeof(Coordinating))]
public class Spaceman : MonoBehaviour, ITick
{
    [SerializeField] ChunksBlock chunksBlock;

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
        ManagerUpdate.AddTo(this);
        
        coordinating = GetComponent<Coordinating>();
        coordinatesData = GetComponent<CoordinatesData>();
        
        CoordinatesChanged += coordinating.SetUpCoordinates;
        CoordinatesChanged();

        SendChange += chunksBlock.ChunksUpdate;

        previousX = coordinatesData.x;
        previousZ = coordinatesData.z;
    }

    public void Tick()
    {
        if ((previousX != coordinatesData.x) || (previousZ != coordinatesData.z))
        {
            SendChange(coordinatesData.x - previousX, coordinatesData.z - previousZ);

            previousX = coordinatesData.x;
            previousZ = coordinatesData.z;
        }

        CoordinatesChanged();
    }
}
