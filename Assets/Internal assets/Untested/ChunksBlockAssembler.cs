using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(ChunksBlockAssemblerData))]
public class ChunksBlockAssembler: MonoBehaviour
{
    ChunksBlockAssemblerData assemblerData;

    public static event ChunkGenerating.GeneratingEvents GeneratingDone;
    public static event ChunksController.ChunkAssembly AssemblyChunk;

    private void Start()
    {
        assemblerData = GetComponent<ChunksBlockAssemblerData>();

        assemblerData.generatedChunks = 0;
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
        Debug.Log(assemblerData.generatedChunks);

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
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
        }

        transform.GetComponent<MeshFilter>().mesh = new Mesh();
        transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
        transform.gameObject.SetActive(true);

        gameObject.transform.position = chunksBlockPosition;    
        MeshCollider meshCol = this.gameObject.AddComponent<MeshCollider>();
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
}