using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    private Register register;
    private Takeout takeout;
    private NavMeshAgent agent;
    private OrderManager orderManager;

    public GameObject waitZone;
    public GameObject endZone;

    // References to the Queue Managers attached to the children
    private QueueManager registerQueue;
    private QueueManager takeoutQueue;

    public int state = 0; // 0 = move to cashier, 1 = move to safe zone, 2 = move to takeout, 3 = leave store, 4 = destroyed

    // Safety checks so we only trigger logic once per state
    private bool registered = false;
    private bool joinedTakeout = false;
    public float maxWaitTime = 30f;
    private HandleBar handleBar;

    void Start()
    {
        register = GameObject.FindFirstObjectByType<Register>();
        takeout = GameObject.FindFirstObjectByType<Takeout>();
        agent = GetComponent<NavMeshAgent>();
        orderManager = GameObject.FindFirstObjectByType<OrderManager>();
        handleBar = GameObject.FindFirstObjectByType<HandleBar>();

        // 1. Get the QueueManagers from the child objects of Register and Takeout
        if (register != null)
        {
            registerQueue = register.GetComponentInChildren<QueueManager>();
        }

        if (takeout != null)
        {
            takeoutQueue = takeout.GetComponentInChildren<QueueManager>();
        }

        // Since the customer starts in state 0, they should join the register line immediately
        //if (registerQueue != null)
        //{
        //    registerQueue.JoinLine(this.gameObject);
        //}
    }

    void Update()
    {
        switch (state)
        {
            case 0:
                // We REMOVED agent.SetDestination here because registerQueue handles movement now!
                if (registered == false)
                {
                    registered = true;
                    register.pendingOrdersToAccept++;
                    register.pendingCustomers.Enqueue(gameObject);
                    registerQueue.JoinLine(this.gameObject);
                    handleBar.StartTimer();
                    StartCoroutine(WaitTilRemoval());
                }
                break;

            case 1:
                // Still using standard movement for the wait zone
                agent.SetDestination(waitZone.transform.position);
                break;

            case 2:
                // 2. Tell the takeout queue we want to join, but only do it ONCE
                StopAllCoroutines();
                if (joinedTakeout == false)
                {
                    handleBar.gameObject.SetActive(false);
                    registerQueue.LeaveLine();
                    joinedTakeout = true;
                    if (takeoutQueue != null)
                    {
                        takeoutQueue.JoinLine(this.gameObject);
                    }
                }
                // Again, NO agent.SetDestination here. takeoutQueue handles it!
                break;

            case 3:
                agent.SetDestination(endZone.transform.position);
                break;

            case 4:
                takeoutQueue.LeaveLine();
                Destroy(gameObject);
                break;
        }
    }

    IEnumerator WaitTilRemoval()
    {
        yield return new WaitForSeconds(maxWaitTime);

        if (state == 0)
        {
            register.pendingOrdersToAccept--;
            register.pendingCustomers.Dequeue();
            registerQueue.LeaveLine();
            Destroy(gameObject);
        }
    }
}