using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MeleeAttack", menuName = "CustomBehaviours/MeleeAttack")]
public class MeleeAttackBehaviour : CustomBehaviour, ITick
{
    readonly List<Entity> _meleeAttackers = new List<Entity>();
    public TickData tickData { get; set; }

    protected override void InitializeCurrentBehaviourByReceivedEntityInstance(Entity instance)
    {
        tickData = new TickData();
        Subscribe();
    }

    public override void TriggerEvent(string eventName, params dynamic[] arguments)
    {
    }

    public override void Subscribe()
    {
        ManagerEvents.StartListening(
            DetectingEvents.PlayerEnteredTheRadiusOfMeleeAttack + $"by:{EntityInstance.name}",
            PrepareWeapon);
        ManagerEvents.StartListening(DetectingEvents.PlayerExitTheRadiusOfMeleeAttack + $"by:{EntityInstance.name}",
            CoverWeapon);
    }

    public override void UnSubscribe()
    {
    }

    protected override void ClearModule()
    {
        _meleeAttackers?.Clear();
        UnSubscribe();
    }

    private void ToAttackOfMelee()
    {
        foreach (var attacker in _meleeAttackers)
        {
            if (EntitiesDataDictionary.TryGetValue(attacker, out Dictionary<string, Data> attackerEntity))
            {
                attackerEntity.TryGetValue("meleeAttackData", out var receivedData);
                var meleeAttackData = (PursueData) receivedData;
                if (meleeAttackData != null && meleeAttackData.IsDisabled != true)
                {
                    meleeAttackData.navMeshAgent.destination = meleeAttackData.player.transform.position;
                }
            }
        }
    }

    private void PrepareWeapon<TDetectingEntity>(
        TDetectingEntity detectingEntity)
    {
        Debug.Log("meleeAttack");
        Entity entity = detectingEntity as Entity;
        if (entity != null)
        {
            _meleeAttackers.Add(entity);
        }
    }

    private void CoverWeapon<TAttackerEntity>(TAttackerEntity attackerEntity)
    {
        Entity entity = attackerEntity as Entity;
        if (entity != null)
        {
            _meleeAttackers.Remove(entity);
        }
    }

    public void Tick()
    {
        ToAttackOfMelee();
    }
}