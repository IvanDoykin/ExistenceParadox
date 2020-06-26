using UnityEngine;
using System.Collections;
using Object = System.Object;

//[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(ChunksBlockAssemblerData))]
public class ChunksBlockAssembler : MonoBehaviour, IEventTrigger
{
    ChunksBlockAssemblerData assemblerData;
    [SerializeField] private EventsCollection chunkCreated;
    public static event ChunkGenerating.GeneratingEvents GeneratingDone;
    public static event ChunksBlock.ChunkAssembly AssemblyChunk;

    private void Start()
    {
        assemblerData = GetComponent<ChunksBlockAssemblerData>();
        assemblerData.needGeneratedChunks = ChunksBlockAssemblerData.startNeedGeneratedChunks;
    }

    private void Starting()
    {
        DeleteOldChunksBlock();
        AssemblyChunksBlock();
    }

    public void SetNeedGeneratedChunks(int value)
    {
        assemblerData.needGeneratedChunks = value;
    }

    public void ChunkIsReady()
    {
        assemblerData.generatedChunks++;

        if (assemblerData.generatedChunks == assemblerData.needGeneratedChunks)
            Starting();
    }

    private void AssemblyChunksBlock()
    {
        assemblerData.generatedChunks = 0;

        Vector3 chunksBlockPosition = gameObject.transform.position;
        gameObject.transform.position = Vector3.zero;

        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        for (int i = 0; i < meshFilters.Length; i++)
        {
            combine[i].mesh = meshFilters[i].mesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            //meshFilters[i].gameObject.SetActive(false);
        }

        transform.GetComponent<MeshFilter>().mesh = new Mesh();
        transform.GetComponent<MeshFilter>().mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine, true, true);
        transform.gameObject.SetActive(true);

        gameObject.transform.position = chunksBlockPosition;
        MeshCollider meshCol = this.gameObject.AddComponent<MeshCollider>();
        TriggerEvent(chunkCreated.currentEvent);
    }

    private void DeleteOldChunksBlock()
    {
        Destroy(gameObject.GetComponent<MeshCollider>());
        GetComponent<MeshFilter>().mesh = new Mesh();
    }

    public void ReassemblyChunksBlock()
    {
        DeleteOldChunksBlock();
        AssemblyChunksBlock();
    }

    public void TriggerEvent(string eventName, params Object[] arguments)
    {
        ManagerEvents.CheckTriggeringEvent(eventName);
    }
}