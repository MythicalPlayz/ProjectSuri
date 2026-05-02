using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Camera Sensitivities")]
    // Split your old lookSensitivity into two separate values
    [SerializeField] private float mouseSensitivity = 0.5f;
    [SerializeField] private float gamepadSensitivity = 150f;

    [Space]
    [SerializeField] private GameObject playerCamera;

    private float xRotation = 0f;
    private GameManager gameManager;
    private Rigidbody rb;

    private InputAction moveAction;
    private InputAction lookAction;

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        lookAction = InputSystem.actions.FindAction("Look");
        gameManager = GameObject.FindFirstObjectByType<GameManager>();
        rb = GetComponent<Rigidbody>();
        LockMouse();
    }

    public void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void FixedUpdate()
    {
        if (gameManager.isGameActive == false) return;

        Vector2 inputVector = moveAction.ReadValue<Vector2>();
        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y).normalized;
        rb.MovePosition(rb.position + transform.TransformDirection(moveDirection) * moveSpeed * Time.fixedDeltaTime);
    }

    private void LateUpdate()
    {
        if (gameManager.isGameActive == false) return;

        // Get the raw input value
        Vector2 lookInput = lookAction.ReadValue<Vector2>();

        // Check WHICH device is actually sending this input right now
        if (lookAction.activeControl != null)
        {
            if (lookAction.activeControl.device is Gamepad)
            {
                // GAMEPAD: Scale by large sensitivity AND Time.deltaTime
                lookInput *= gamepadSensitivity * Time.deltaTime;
            }
            else if (lookAction.activeControl.device is Mouse)
            {
                // MOUSE: Scale by small sensitivity (No deltaTime needed for mouse delta)
                lookInput *= mouseSensitivity;
            }
        }
        else
        {
            // Fallback (usually when input is exactly 0,0 and no device is actively pushing)
            lookInput *= mouseSensitivity;
        }

        // Horizontal (Yaw) → rotate Player
        // (Notice we removed * lookSensitivity here, because lookInput is already multiplied above)
        transform.Rotate(Vector3.up * lookInput.x);

        // Vertical (Pitch) → rotate playerCamera
        xRotation -= lookInput.y;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}