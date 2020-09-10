using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PrimaryChunkGenerating))]
[RequireComponent(typeof(SecondaryChunkGenerating))]
[RequireComponent(typeof(Coordinating))]
[RequireComponent(typeof(MeshCreator))]
[RequireComponent(typeof(LoadChunk))]
[RequireComponent(typeof(ChunksLinker))]

[RequireComponent(typeof(ChunkData))]
[RequireComponent(typeof(CoordinatingData))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class ChunkGenerator : MonoBehaviour
{
    public bool loaded; //load from hard-drive or not?

    //some additional functionality
    private PrimaryChunkGenerating primaryChunkGenerating;
    private SecondaryChunkGenerating secondaryChunkGenerating;
    private Coordinating coordinating;
    private MeshCreator meshCreator;
    private LoadChunk loadChunk;
    private ChunksLinker chunksLinker;

    private ChunkData chunk;
    private CoordinatingData coordinatesData;

    //Call for link one chunk with others
    public delegate void CallChunkLinking(CoordinatingData coordinatesData);
    public static event CallChunkLinking NeedLink;

    //If we are generating, throw message 'Start next generating stage, plz'
    public delegate void CallPrimaryGenerating();
    public static event CallPrimaryGenerating PrimaryGeneratingDone;

    private void Start()
    {
        loadChunk = GetComponent<LoadChunk>();
        meshCreator = GetComponent<MeshCreator>();

        //starter coordinating
        coordinating = GetComponent<Coordinating>();
        coordinating.SetUpCoordinates();

        chunksLinker = GetComponentInParent<ChunksLinker>();
        NeedLink += chunksLinker.LinkChunk;

        //get ready for next generating stage
        primaryChunkGenerating = GetComponent<PrimaryChunkGenerating>();
        secondaryChunkGenerating = GetComponent<SecondaryChunkGenerating>();
        PrimaryGeneratingDone += secondaryChunkGenerating.SecondaryGenerating;

        chunk = GetComponent<ChunkData>();
        coordinatesData = GetComponent<CoordinatingData>();

        //try find earlier saved chunk
        chunk.loaded = loadChunk.TryLoadData(ref chunk, gameObject.GetComponent<ChunkNameData>().value);

        //if not, we generate this
        if (chunk.loaded == false)
        {
            primaryChunkGenerating.InitializeChunk();

            NeedLink(coordinatesData);
            NeedLink -= chunksLinker.LinkChunk;

            primaryChunkGenerating.DiamondSquare(ChunkData.Size);
            PrimaryGeneratingDone();
        }

        //else apply some technical changes
        else
        {
            chunk.mesh = new Mesh();
            meshCreator.CreateMesh(ref chunk);

            MeshCollider meshCollider = this.gameObject.AddComponent<MeshCollider>();
            meshCollider.sharedMesh = chunk.mesh;
        }

        //all done -- this chunk ready for scene
        chunk.constructed = true;
        NeedLink -= chunksLinker.LinkChunk;
        PrimaryGeneratingDone -= secondaryChunkGenerating.SecondaryGenerating;
    }

}
