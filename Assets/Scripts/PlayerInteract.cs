using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject selectedGameObject;

    private GameObject holding;
    private GameManager gameManager;
    private OrderManager orderManager;


    [SerializeField] private GameObject hand;

    private InputAction interactAction;
    void Start()
    {
        interactAction = InputSystem.actions.FindAction("InteractWith");
        gameManager = GameObject.FindFirstObjectByType<GameManager>();
        orderManager = GameObject.FindFirstObjectByType<OrderManager>();
    }

    // Update is called once per frame
    void Update()
    {
        bool interact = interactAction.WasPressedThisFrame();
        if (interact)
        {
            if (selectedGameObject == null)
                return;

            if (selectedGameObject.CompareTag("OrderReciept"))
            {
                Debug.Log("Picked Up:" + selectedGameObject?.name);
                selectedGameObject?.transform.SetParent(hand.transform);
                selectedGameObject.transform.position = hand.transform.position;
                holding = selectedGameObject;
                selectedGameObject.GetComponent<OrderReciept>().orderBoard.GetComponent<OrderBoard>().FreeSpace(selectedGameObject.GetComponent<OrderReciept>().id);
            }


            else if (selectedGameObject.CompareTag("Consumeable"))
            {
                if (holding)
                {
                    // check if suri making
                    Suri s = selectedGameObject.GetComponent<Suri>();
                    if (s && s.maker)
                    {
                        s.maker.GetComponent<SuriMaker>().MakeSuri(holding);
                        if (hand.transform.childCount == 0)
                            holding = null;
                    }
                    if (s && s.takeout && holding.GetComponent<OrderReciept>())
                    {
                        s.takeout.GetComponent<Takeout>().FreeLocation(s.tID);
                        gameManager.UpdateScore(orderManager.CompleteOrder(selectedGameObject,holding.GetComponent<OrderReciept>()));
                        Destroy(selectedGameObject);
                        Destroy(holding);
                        holding = null;
                    }
                    return;
                }

                if (selectedGameObject.GetComponent<Suri>() && selectedGameObject.GetComponent<Suri>().maker)
                {
                    Suri s = selectedGameObject.GetComponent<Suri>();
                    if (!s.UnTouched())
                        s.Wrap(true);
                    s.maker.GetComponent<SuriMaker>().RemoveSuri();
                    s.maker = null;
                }

                if (selectedGameObject.GetComponent<Suri>() && selectedGameObject.GetComponent<Suri>().takeout)
                {
                    return;
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

                        if (holding)
                            return;
                        selectedGameObject.GetComponent<Register>().RegisterOrder();

                        break;
                    case GameManager.InteractableType.SuriHolder:
                    case GameManager.InteractableType.PotatoFreezer:
                    case GameManager.InteractableType.ChickenFreezer:

                        if (holding != null)
                        {
                            if (holding.GetComponent<Suri>() != null && holding.GetComponent<Suri>().UnTouched() )
                            {
                                selectedGameObject.GetComponent<ItemGiver>().RemoveItem(holding);
                                if (hand.transform.childCount == 0)
                                    holding = null;
                            }
                            else if (holding.GetComponent<FrozenFood>() != null)
                            {
                                selectedGameObject.GetComponent<ItemGiver>().RemoveItem(holding);
                                if (hand.transform.childCount == 0)
                                    holding = null;
                            }
                            return;
                        }
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

                    case GameManager.InteractableType.SuriGrill:

                        selectedGameObject.GetComponent<SuriGrill>().Grill(holding);
                        if (hand.transform.childCount == 0)
                            holding = null;
                        break;

                    case GameManager.InteractableType.Wrapper:

                        if (holding == null)
                            return;
                        selectedGameObject.GetComponent<SuriBagger>().BagSuri(holding);
                        break;

                    case GameManager.InteractableType.Trash:

                        if (holding == null)
                            return;
                        selectedGameObject.GetComponent<Trash1>().Remove(holding);
                        if (hand.transform.childCount == 0)
                            holding = null;
                        break;

                    case GameManager.InteractableType.TakeOut:

                        if (holding == null)
                            return;
                        selectedGameObject.GetComponent<Takeout>().TakeLocation(holding);
                        if (hand.transform.childCount == 0)
                            holding = null;
                        break;

                    case GameManager.InteractableType.OrdersBoard:
                        if (holding == null || !holding.CompareTag("OrderReciept"))
                            return;
                        selectedGameObject.GetComponent<OrderBoard>().PlaceReciept(holding);
                        if (hand.transform.childCount == 0)
                            holding = null;
                        break;
                }
            }

        }
       
    }
}
