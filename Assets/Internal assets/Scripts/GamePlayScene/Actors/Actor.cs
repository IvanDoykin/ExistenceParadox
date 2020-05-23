using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    // public ActorDynamicData data;

    public List<ScriptableObject> behavioursList;

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