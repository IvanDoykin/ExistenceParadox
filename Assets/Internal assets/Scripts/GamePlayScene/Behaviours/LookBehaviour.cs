using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

public enum RotationAxes
{
    MouseXAndY = 0,
    MouseX = 1,
    MouseY = 2
}

[CreateAssetMenu(fileName = "Look", menuName = "CustomBehaviours/Look")]
public class LookBehaviour : CustomBehaviour, ITick
{
    readonly List<Entity> _players = new List<Entity>();
    public TickData tickData { get; set; }

    public override void Subscribe() //подписка на ивенты
    {
    }
    public override void UnSubscribe() //отписка
    {
    }

    public override void TriggerEvent(string eventName, params dynamic[] arguments) 
    {
    }

    protected override void ClearModule() //тут просто отписываемся
    {
        UnSubscribe();
    }

    protected override void InitializeCurrentBehaviourByReceivedEntityInstance(Entity instance) //тут просто подписываемся
    {
        _players.Add(instance);
        Rigidbody body = instance.GetComponent<Rigidbody>();
        if (body != null)
            body.freezeRotation = true;

        Subscribe();
    }


    public void Tick()
    {
        Look();
    }

    private void Look()
    {
        foreach (var player in _players)
        {
            if (EntitiesDataDictionary.TryGetValue(player, out Dictionary<string, Data> playerEntity))
            {
                playerEntity.TryGetValue("PersData", out var receivedData);
                var persData = (PlayerMovementData)receivedData;
                if (persData != null && persData.IsDisabled != true)
                {
                    if (persData.axes == RotationAxes.MouseX)
                    {
                        player.transform.Rotate(0, Input.GetAxis("Mouse X") * persData.sensitivityHor, 0);
                    }

                    else if (persData.axes == RotationAxes.MouseY)
                    {
                        persData.rotationX -= Input.GetAxis("Mouse Y") * persData.sensitivityVert;
                        persData.rotationX = Mathf.Clamp(persData.rotationX, persData.minimumVert, persData.maximumVert);
                        float rotationY = player.transform.localEulerAngles.y;
                        player.transform.localEulerAngles = new Vector3(persData.rotationX, rotationY, 0);
                    }

                    else
                    {
                        persData.rotationX -= Input.GetAxis("Mouse Y") * persData.sensitivityVert;
                        persData.rotationX = Mathf.Clamp(persData.rotationX, persData.minimumVert, persData.maximumVert);
                        float delta = Input.GetAxis("Mouse X") * persData.sensitivityHor;
                        float rotationY = player.transform.localEulerAngles.y + delta;
                        player.transform.localEulerAngles = new Vector3(persData.rotationX, rotationY, 0);
                    }
                }
            }
        }
    }
}
