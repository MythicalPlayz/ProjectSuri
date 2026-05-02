using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    private Register register;
    private Takeout takeout;
    private NavMeshAgent agent;
    private OrderManager orderManager;


    private Animator animator;
    public GameObject endZone;


    private QueueManager registerQueue;
    private QueueManager takeoutQueue;

    private Outline outline;

    public int state = 0; // 0 = move to start line, 1 = wait at register, 2 = move to takeout line, 3 = wait at takeout line, 4 = leave, 5 destroy


    public float maxWaitTime = 30f;
    private HandleBar handleBar;
    public Material[] materials;

    void Start()
    {
        register = GameObject.FindFirstObjectByType<Register>();
        takeout = GameObject.FindFirstObjectByType<Takeout>();
        agent = GetComponent<NavMeshAgent>();
        orderManager = GameObject.FindFirstObjectByType<OrderManager>();
        handleBar = GameObject.FindFirstObjectByType<HandleBar>();
        animator = GetComponent<Animator>();
        endZone = GameObject.FindFirstObjectByType<SpawnManager>().end;
        outline = GetComponent<Outline>();

        if (register != null)
        {
            registerQueue = register.GetComponentInChildren<QueueManager>();
        }

        if (takeout != null)
        {
            takeoutQueue = takeout.GetComponentInChildren<QueueManager>();
        }
        AddMaterial();
    }

    void AddMaterial()
    {
        // Random number between 0 and the length of the materials array (exclusive)
        int index = Random.Range(0, materials.Length);

        // GetComponentsInChildren finds all MeshRenderers on this object AND all its children/grandchildren
        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();

        // Loop through each found renderer and apply the random material
        foreach (MeshRenderer renderer in renderers)
        {
            renderer.material = materials[index];
        }
    }

    // Add this at the top of your class variables
    private int previousState = -1;

    void Update()
    {
        if (agent != null && animator != null)
        {
            bool isMoving = agent.velocity.magnitude > 0.1f;
            animator.SetBool("isWalking", isMoving);
        }

        // This trigger is true ONLY on the exact frame the state changes to a new number
        bool enterState = (state != previousState);
        previousState = state;

        switch (state)
        {
            case 0:
                // 0: Move into queue start line
                if (enterState)
                {
                    // Assuming StartLine is a Transform or GameObject property. 
                    // If it's a method that returns a position, adjust accordingly.
                    agent.SetDestination(registerQueue.StartLine().transform.position);
                    handleBar.gameObject.SetActive(false);
                }

                // Auto-transition to State 1 when destination reached
                if (HasReachedDestination())
                {
                    state = 1;
                }
                break;

            case 1:
                // 1: Join order queue and request order
                if (enterState)
                {
                    registerQueue.JoinLine(this.gameObject);
                    // Legacy logic from old code for requesting an order
                    register.pendingOrdersToAccept++;
                    register.pendingCustomers.Enqueue(gameObject);
                    handleBar.gameObject.SetActive(true);
                    handleBar.StartTimer();
                    StartCoroutine(WaitTilRemoval());
                }
                // Stays in State 1. External queue/register logic should set state = 2 when done.
                break;

            case 2:
                // 2: Leave order queue and head to takeout start line
                if (enterState)
                {
                    StopAllCoroutines();
                    handleBar.gameObject.SetActive(false);
                    registerQueue.LeaveLine();

                    agent.SetDestination(takeoutQueue.StartLine().transform.position);
                }

                // Auto-transition to State 3 when destination reached
                if (HasReachedDestination())
                {
                    state = 3;
                }
                break;

            case 3:
                // 3: Enter TO Line
                if (enterState)
                {
                    takeoutQueue.JoinLine(this.gameObject);
                }
                // Stays in State 3. External takeout logic should set state = 4 when food is received.
                break;

            case 4:
                // 4: Leave TO queue and head to endzone
                if (enterState)
                {
                    takeoutQueue.LeaveLine();
                    agent.SetDestination(endZone.transform.position);
                }

                // Auto-transition to State 5 when destination reached
                if (HasReachedDestination())
                {
                    state = 5;
                }
                break;

            case 5:
                // 5: Destroy
                if (enterState)
                {
                    Destroy(gameObject);
                }
                break;
        }
    }

    /// <summary>
    /// Reliable helper method to check if the NavMeshAgent has arrived at its destination.
    /// </summary>
    private bool HasReachedDestination()
    {
        // If the path is still computing, we haven't reached the end
        if (agent.pathPending) return false;

        // Check if we are within stopping distance
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            // Ensure the agent isn't being pushed and has actually stopped moving
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            {
                return true;
            }
        }
        return false;
    }

    IEnumerator WaitTilRemoval()
    {
        yield return new WaitForSeconds(maxWaitTime);

        if (state == 1)
        {
            register.pendingOrdersToAccept--;
            register.pendingCustomers.Dequeue();
            registerQueue.LeaveLine();
            state = 4;
        }
    }
}