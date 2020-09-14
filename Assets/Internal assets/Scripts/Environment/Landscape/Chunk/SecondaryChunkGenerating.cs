using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ChunkData))]
[RequireComponent(typeof(CoordinatingData))]

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

[RequireComponent(typeof(MeshCreator))]

public class SecondaryChunkGenerating : MonoBehaviour, IEventSub
{
    //event :)
    [SerializeField] private EventsCollection ChunkCreated;

    //Solute the problem with sharp edges after secondary generating
    public delegate void Smooth(int x, int z);
    public static event Smooth SmoothMe;

    private ChunkData chunk;

    private EdgeSmoother edgeSmoother;
    private MeshCreator meshCreator;
    private ChunksLinker chunksLinker;

    //message 'I'm done' for chunk :)
    public delegate void GeneratingEvents();
    public static event GeneratingEvents GeneratingDone;

    public static event ChunkGenerator.CallPrimaryGenerating PrimaryGeneratingDone;
    private void Start()
    {
        Subscribe();

        chunksLinker = GetComponentInParent<ChunksLinker>();
        meshCreator = GetComponent<MeshCreator>();
        chunk = GetComponent<ChunkData>();

        if (chunk == null)
            Debug.Log("errrrr");

        edgeSmoother = GetComponentInParent<EdgeSmoother>();
        SmoothMe += edgeSmoother.Smooth;
    }

    private void Update()
    {
        if (chunk.readyForUpdate)
        {
            chunk.readyForUpdate = false;
            if (chunk.fullUpdated)
                return;

            if (chunk.loaded == false)
            {
                chunk.loaded = true;
                chunksLinker.LinkChunk(GetComponent<CoordinatingData>());
                AveragingDots();
            }

            meshCreator.CreateMesh(ref chunk);
            GetComponent<MeshCollider>().sharedMesh = chunk.mesh;
        }
    }


    //Smooth chunk, but with sharp edges
    public void SecondaryGenerating()
    {
        AveragingDots();

        meshCreator.CreateMesh(ref chunk);

        MeshCollider meshCollider = this.gameObject.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = chunk.mesh;

        SmoothMe(chunk.argX, chunk.argZ);
        SmoothMe -= edgeSmoother.Smooth;
    }

    private void AveragingDots()
    {
        if (chunk != null)
            Debug.Log(chunk.argX);

        for (int i = 0; i < chunk.dots.Length; i++)
        {
            if (((i % (ChunkData.Size + 1)) != 0) && ((i + 1) % (ChunkData.Size + 1) != 0) && (i < (ChunkData.Size * (ChunkData.Size + 1) + 1)) && (i > ChunkData.Size))
            {
                chunk.dots[i].y = MeshCreator.Average(chunk.dots[i - 1].y, chunk.dots[i + (ChunkData.Size + 1)].y, chunk.dots[i - (ChunkData.Size + 1)].y, chunk.dots[i + 1].y);
            }
        }
    }

    private void ChangeUpdateState()
    {
        chunk.readyForUpdate = true;
    }

    public void Subscribe()
    {
        ManagerEvents.StartListening(ChunkCreated.currentEvent, ChangeUpdateState);
    }

    public void UnSubscribe()
    {
        ManagerEvents.StopListening(ChunkCreated.currentEvent, ChangeUpdateState);
    }
}
