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
            // Behaviour instanceBehaviours = ScriptableObject.CreateInstance<Behaviour>();
            // instanceBehaviours.ReceiveActor(actor);
            // ((Behaviour) behaviour).ReceiveActor(actor);
            ((ICustomBehaviour) behaviour).ReceiveData(actor);
        }
    }

    private void Start()
    {
        SendDataToBehaviours(this);
    }
}