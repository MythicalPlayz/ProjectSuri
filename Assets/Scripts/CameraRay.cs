using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRay : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Camera CAM;
    private GameObject holding;
    private Vector3 screenCenter;
    void Start()
    {
        screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
    }

    private void FixedUpdate()
    {
        screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
        Ray ray = CAM.ScreenPointToRay(screenCenter); //camera.gameObject.transform.position
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.CompareTag("Consumeable") && (holding == null || holding != hit.collider.gameObject))
            {
                if (holding != null)
                {
                    Debug.Log("Dropping: " + holding.name);
                    holding.GetComponent<Outline>().enabled = false;
                }
                Debug.Log("Hit: " + hit.collider.gameObject.name);
                holding = hit.collider.gameObject;
                holding.GetComponent<Outline>().enabled = true;
            }
                
        }
    }
}
