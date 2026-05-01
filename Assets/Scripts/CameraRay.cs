using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRay : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Camera CAM;
    private GameObject holding;
    private Vector3 screenCenter;
    private GameManager gameManager;

    public TextMeshProUGUI textbox;
    void Start()
    {
        screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
        gameManager = GameObject.FindFirstObjectByType<GameManager>();
    }

    private void FixedUpdate()
    {
        screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
        Ray ray = CAM.ScreenPointToRay(screenCenter);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);

        if (Physics.Raycast(ray, out hit))
        {
            if ((hit.collider.gameObject.CompareTag("Consumeable") || hit.collider.gameObject.CompareTag("Interactable") || hit.collider.gameObject.CompareTag("OrderReciept"))) // && !hit.collider.gameObject.transform.parent.CompareTag("hand"))
            {
              
                //Debug.Log("Hit: " + hit.collider.gameObject.name);
                holding = hit.collider.gameObject;
                IUIStr uiStr = holding.GetComponent<IUIStr>();
                if (uiStr != null)
                {
                    UpdateUI(true, uiStr.str);
                }
            }
            else
            {
                holding = null;
                UpdateUI(false);
            }
            UpdateObject();
            if (holding == null)
            {
                gameManager.ChangeHighlightedObject(null);
                return;
            }
            gameManager.ChangeHighlightedObject(holding);
        }
    }

    void UpdateObject()
    {
        gameObject.GetComponent<PlayerInteract>().selectedGameObject = holding;
    }

    void UpdateUI(bool isEnabled,string str = "")
    {
        if (isEnabled)
        {
            textbox.text = str;
            textbox.gameObject.SetActive(true);
        }
        else
        {
            textbox.gameObject.SetActive(false);
        }
    }
}
