using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    [Header("Movimiento")]
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;

    [Header("Rotación con Ratón")]
    public float mouseSensitivity = 100f;
    public float maxVerticalAngle = 80f;

    [Header("Referencias")]
    public Transform cameraTransform;

    private CharacterController _controller;
    private Vector3 _velocity;
    private float _xRotation = 0f;
    private bool _isGrounded;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked; // Bloquea el cursor en el centro
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMovement();
        HandleMouseLook();
        HandleJump();
        ApplyGravity();
    }

    private void HandleMovement()
    {
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        _controller.Move(move * currentSpeed * Time.deltaTime);
    }

    private void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -maxVerticalAngle, maxVerticalAngle);

        cameraTransform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    private void ApplyGravity()
    {
        _isGrounded = _controller.isGrounded;

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f; // Pequeña fuerza hacia abajo para asegurar el contacto con el suelo
        }

        _velocity.y += gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
    }
}