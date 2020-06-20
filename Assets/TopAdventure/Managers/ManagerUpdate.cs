using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Апдейт менеджер позволяет использовать Update как и всегда он используется в unity,
/// но при этом только лишь один update наследуется от MonoBehaviour
[CreateAssetMenu(fileName = "ManagerUpdate", menuName = "Managers/ManagerUpdate")]
public class ManagerUpdate : ManagerBase, IAwake
{
    private List<ITick> ticks = new List<ITick>(); //список для апдейтов
    private List<ITickFixed> ticksFixes = new List<ITickFixed>(); //список для фиксед апдейтов
    private List<ITickLate> ticksLate = new List<ITickLate>(); //список для лейт апдейтов


    /// <summary>
    /// Добавление в данный менеджер(в соответствующий список) реализации интерфесйсов ITick, ITickFixed, или ITickLate
    /// </summary>
    /// <param name="updateble"></param>
    public static void AddTo(object updateble)
    {
        var mngUpdate = Toolbox.Get<ManagerUpdate>(); // возвращение созданной в стартере копии менеджера апдейта

        if (updateble is ITick)
            mngUpdate.ticks.Add(updateble as ITick);

        if (updateble is ITickFixed)
            mngUpdate.ticksFixes.Add(updateble as ITickFixed);

        if (updateble is ITickLate)
            mngUpdate.ticksLate.Add(updateble as ITickLate);
    }

    public static void RemoveFrom(object updateble)
    {
        var mngUpdate = Toolbox.Get<ManagerUpdate>();
        if (updateble is ITick)
            mngUpdate.ticks.Remove(updateble as ITick);

        if (updateble is ITickFixed)
            mngUpdate.ticksFixes.Remove(updateble as ITickFixed);

        if (updateble is ITickLate)
            mngUpdate.ticksLate.Remove(updateble as ITickLate);
    }

    /// <summary>
    /// Проигрывание тика апдейта
    /// </summary>
    public void Tick()
    {
        for (var i = 0; i < ticks.Count; i++)
        {
            if (ticks[i] != null)
                ticks[i].Tick();
        }
    }

    public void TickFixed()
    {
        for (var i = 0; i < ticksFixes.Count; i++)
        {
            ticksFixes[i].TickFixed();
        }
    }

    public void TickLate()
    {
        for (var i = 0; i < ticksLate.Count; i++)
        {
            ticksLate[i].TickLate();
        }
    }


    public void OnAwake()
    {
        GameObject.Find("[SETUP]").AddComponent<ManagerUpdateComponent>().Setup(this);
    }
}