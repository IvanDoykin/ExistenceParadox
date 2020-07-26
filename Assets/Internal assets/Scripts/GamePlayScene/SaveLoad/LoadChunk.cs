using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public sealed class LoadChunk : LoadBase<ChunkData>
{
    private string chunksFolder;

    public override bool TryLoadData(ref ChunkData loadingData, string key)
    {
        chunksFolder = Application.persistentDataPath + "/Cached/Chunks/";

        GetData(ref loadingData, key);
        if (loadingData == null)
            return false;

        else return true;
    }

    protected override void GetData(ref ChunkData chunk, string key)
    {
        if (true)
        {
            string path = chunksFolder + key + DataFormat;
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            using (Stream fStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                if (!File.Exists(path))
                {
                    chunk = null;
                    return;
                }
                SavedChunk savedChunk = (SavedChunk)binaryFormatter.Deserialize(fStream);

                Debug.Log("Nice load :)");
                Debug.Log("Load from: " + path);
                chunk.SetStartData(ConvertVecToVector(savedChunk.position), savedChunk.rangeOffset, savedChunk.fullUpdated, ConvertVecToVector(savedChunk.dots));
            }
        }
    }

    private Vector3[] ConvertVecToVector(Vec3[] vecMassive)
    {
        Vector3[] vectorMassive = new Vector3[vecMassive.Length];

        for(int i = 0; i < vectorMassive.Length; i++)
        {
            vectorMassive[i] = ConvertVecToVector(vecMassive[i]);
        }

        return vectorMassive;
    }

    private Vector3 ConvertVecToVector(Vec3 vec)
    {
        return new Vector3(vec.x, vec.y, vec.z);
    }

    protected override bool TryGetFromDictionary(out ChunkData loadingData, string key)
    {
        if (savingChunks.TryGetValue(key, out loadingData))
            return true;

        return false;
    }
}
