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
            if (selectedGameObject == null)
                return;
            if (selectedGameObject.CompareTag("Consumeable"))
            {
                if (holding)
                {
                    // check if suri making
                    Suri s = selectedGameObject.GetComponent<Suri>();
                    if (s && s.maker)
                    {
                        s.maker.GetComponent<SuriMaker>().MakeSuri(holding);
                    }
                    return;
                }

                if (selectedGameObject.GetComponent<Suri>() && selectedGameObject.GetComponent<Suri>().maker)
                {
                    Suri s = selectedGameObject.GetComponent<Suri>();
                    s.maker.GetComponent<SuriMaker>().RemoveSuri();
                    s.maker = null;
                }

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
                    case GameManager.InteractableType.PotatoFreezer:
                    case GameManager.InteractableType.ChickenFreezer:

                        if (holding != null)
                            return;
                        holding = selectedGameObject.GetComponent<ItemGiver>().GiveItem(hand);
                        break;


                    case GameManager.InteractableType.Fryer:

                        if (holding == null || !holding.CompareTag("FrozenFood"))
                            return;
                            selectedGameObject.GetComponent<Fryer>().FryFood(holding);
                        break;



                    case GameManager.InteractableType.ItemHolding:
                        if (!holding)
                            return;
                        selectedGameObject.GetComponent<IngredientsHolding>().AddIngredient(holding);
                        holding = null;
                        break;


                    case GameManager.InteractableType.SuriMaker:

                            if (holding == null)
                                return;
                            selectedGameObject.GetComponent<SuriMaker>().MakeSuri(holding);
                            if (hand.transform.childCount == 0)
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
