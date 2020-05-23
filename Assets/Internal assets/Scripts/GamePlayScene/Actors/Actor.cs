using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public ActorDynamicData data;

    public List<GameObject> dataList;
    public List<ScriptableObject> behavioursList;

    [System.Serializable]
    public struct ActorDynamicData
    {
        public DataHp dataHp;
        public DataDamage dataDamage;
    }


    private static void SendDataToBehaviours(Actor actor)
    {
        foreach (ScriptableObject behaviour in actor.behavioursList)
        {
            ((Behaviour) behaviour).ReceiveActor(actor);
        }
    }


    private void Start()
    {
        SendDataToBehaviours(this);
    }
}