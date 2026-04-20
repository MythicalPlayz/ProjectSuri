using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject selectedGameObject;

    private GameObject holding;
    [SerializeField] private GameObject hand;

    private InputAction interactAction;
    void Start()
    {
        interactAction = InputSystem.actions.FindAction("InteractWith");
    }

    // Update is called once per frame
    void Update()
    {
        bool interact = interactAction.WasPressedThisFrame();
        if (interact)
        {
            if (selectedGameObject.CompareTag("Consumeable"))
            {
                if (holding)
                    return;

                Debug.Log("Picked Up:" + selectedGameObject?.name);
                selectedGameObject?.transform.SetParent(hand.transform);
                selectedGameObject.transform.position = hand.transform.position;
                holding = selectedGameObject;
            }
            else if (selectedGameObject.CompareTag("Interactable"))
            {
                Debug.Log("Interacted with:" + selectedGameObject?.name);
                InteractType interactType = selectedGameObject.GetComponent<InteractType>();
                switch (interactType.type)
                {
                    case GameManager.InteractableType.Register:
                        break;
                    case GameManager.InteractableType.SuriHolder:
                        break;
                    case GameManager.InteractableType.PotatoFreezer:
                        break;
                    case GameManager.InteractableType.ChickenFreezer:
                        break;
                    case GameManager.InteractableType.Fryer:
                        break;



                    case GameManager.InteractableType.SuriMaker:
                        if (!holding)
                            return;
                        selectedGameObject.GetComponent<IngredientsHolding>().AddIngredient(holding);
                        holding = null;
                        break;


                    case GameManager.InteractableType.SuriFlatter:
                        break;
                    case GameManager.InteractableType.Wrapper:
                        break;
                    case GameManager.InteractableType.TakeOut:
                        break;
                }
            }

        }
       
    }
}
