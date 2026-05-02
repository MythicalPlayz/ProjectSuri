using System.Collections.Generic;
using UnityEngine;

public class QueueManager : MonoBehaviour
{
    public Transform[] queueSpots; // Drag your Empty GameObjects here in the Inspector
    private List<GameObject> agentsInLine = new List<GameObject>();

    // Call this to add an agent to the line
    public void JoinLine(GameObject agent)
    {
        if (agentsInLine.Count < queueSpots.Length - 1)
        {
            agentsInLine.Add(agent);
            UpdateLinePositions();
        }
        else
        {
            //Debug.Log("The line is full!");
            agent.gameObject.GetComponent<Customer>().state = 4;
        }
    }

    // Call this when the first person in line is served/leaves
    public void LeaveLine()
    {
        if (agentsInLine.Count > 0)
        {
            // Remove the first agent
            agentsInLine.RemoveAt(0);
            // Tell everyone else to move forward
            UpdateLinePositions();
        }
    }

    // Tells each agent to walk to their current spot in the array
    private void UpdateLinePositions()
    {
        for (int i = 0; i < agentsInLine.Count; i++)
        {
            // Get the NavMeshAgent component of the agent in line
            UnityEngine.AI.NavMeshAgent navAgent = agentsInLine[i].GetComponent<UnityEngine.AI.NavMeshAgent>();

            // Tell them to walk to their designated spot
            navAgent.SetDestination(queueSpots[i].position);
        }
    }

    public GameObject StartLine()
    {
        return queueSpots[queueSpots.Length - 1].gameObject;
    }
}