using UnityEngine;

public class ChunksBlockAssemblerData : MonoBehaviour
{
    [HideInInspector] public const int startNeedGeneratedChunks = ChunksControllerData.chunksBlockSize * ChunksControllerData.chunksBlockSize;
    [HideInInspector] public int generatedChunks = 0;
    [HideInInspector] public int needGeneratedChunks = 0;
}
