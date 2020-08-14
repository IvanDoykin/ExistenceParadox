using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunksCoordination : MonoBehaviour
{
    //set zero points at need coordinates
    public void StarterCoordinating()
    {
        ChunksBlockData.zeroPointX = (int)SetUpCoordinate(transform.position.x) + ChunksBlockData.halfChunkBlockSize;
        ChunksBlockData.zeroPointZ = (int)SetUpCoordinate(transform.position.z) + ChunksBlockData.halfChunkBlockSize;
    }

    //convert distance in units -> distance in chunks
    public static float SetUpCoordinate(float coordinate)
    {
        return (int)Mathf.Floor(coordinate / ChunkData.Metric);
    }

    //convert distance in chunks -> distance in units
    public static float PopUpCoordinate(float coordinate)
    {
        return (coordinate * ChunkData.Metric);
    }

}
