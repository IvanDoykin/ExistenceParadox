using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterIdle : MonsterState
{
    private Monster monster;
    private MonsterPursue monsterPursue;
    private NavMeshAgent navigatorAgent;

    [SerializeField] private float reactionInSeconds;

    private void Start()
    {
        monster = GetComponent<Monster>();
        monsterPursue = GetComponent<MonsterPursue>();
        navigatorAgent = GetComponent<NavMeshAgent>();
        StartCoroutine(Example());
    }

    public override void Pursue(Monster monster, bool call = false)
    {
        Debug.Log("Start pursue " + GetInstanceID());

        navigatorAgent.Warp(gameObject.transform.position);

        monster.State = monsterPursue;
        monster.isCalled = call;
        GetComponent<Renderer>().material.SetColor("_BaseColor", Color.red);
        StartCoroutine(WaitForTime());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (monster.State != this)
            return;

        if (other.gameObject.GetComponent<PlayerController>() == null)
            return;

        monsterPursue.SetPursueAim(other.gameObject.transform);
        Pursue(monster);
    }

    private IEnumerator Example()
    {
        yield return new WaitForSeconds(0.1f);

        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);
        Debug.DrawRay(transform.position, Vector3.down * 100, Color.red, Mathf.Infinity);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Debug.Log("YEP");
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.green, Mathf.Infinity);
            navigatorAgent.Warp(hit.transform.position);
        }

    }

    private IEnumerator WaitForTime()
    {
        yield return new WaitForSeconds(reactionInSeconds);
        monster.isCalled = true;
    }
}
