using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float lookSensitivity = 1f;
    [SerializeField] private GameObject playerCamera;
    private float xRotation = 0f;

    InputAction moveAction;
    InputAction lookAction;
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        lookAction = InputSystem.actions.FindAction("Look");
        LockMouse();
    }

    public void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputVector = moveAction.ReadValue<Vector2>();
        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y).normalized;
        gameObject.transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.Self);

    }

    private void LateUpdate()
    {
        Vector2 lookInput = lookAction.ReadValue<Vector2>();

        // Horizontal (Yaw) → rotate Player
        transform.Rotate(Vector3.up * lookInput.x * lookSensitivity);

        // Vertical (Pitch) → rotate FacialPoint
        xRotation -= lookInput.y * lookSensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
