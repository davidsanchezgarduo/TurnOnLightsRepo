using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CivilController : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject goal;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        goal = GameObject.FindGameObjectWithTag("Goal");
        UnitiesManager.instance.AddCivil(this);

    }

    public void ActiveCivil() {
        agent.SetDestination(new Vector3(goal.transform.position.x, 0.5f, goal.transform.position.z));
        agent.speed = speed;
    }
}
