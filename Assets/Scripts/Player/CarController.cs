using UnityEngine;

public class CarController : MonoBehaviour
{
    public float speed = 10f;
    public float boostSpeed = 20f;
    public float rotationSpeed = 100f;
    public float brakeForce = 30f; // Increase brake force
    public float stabilizationForce = 100f; // Force applied to stabilize the bike
    public float stabilizationSpeed = 2f; // Speed at which the bike stabilizes itself

    private float baseSpeed;
    private bool isBoosting = false;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        baseSpeed = speed;
    }

    private void Update()
    {
        float moveInput = Input.GetAxis("Vertical");
        float turnInput = Input.GetAxis("Horizontal");
        bool boostInput = Input.GetKey(KeyCode.LeftShift);
        bool brakeInput = Input.GetKey(KeyCode.Space);

        float currentSpeed = isBoosting ? boostSpeed : speed;
        float moveAmount = currentSpeed * moveInput * Time.deltaTime;
        rb.MovePosition(transform.position + transform.forward * moveAmount);

        float turnAmount = rotationSpeed * turnInput * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turnAmount, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);

        if (boostInput && !isBoosting)
        {
            isBoosting = true;
            speed = boostSpeed;
        }
        else if (!boostInput && isBoosting)
        {
            isBoosting = false;
            speed = baseSpeed;
        }

        if (brakeInput)
        {
            ApplyBrake(brakeForce * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        StabilizeBike();
    }

    public void ApplyBrake(float force)
    {
        rb.velocity -= rb.velocity * force;
    }

    private void StabilizeBike()
    {
        Vector3 predictedUp = Quaternion.AngleAxis(
            rb.angularVelocity.magnitude * Mathf.Rad2Deg * stabilizationSpeed / stabilizationForce,
            rb.angularVelocity
        ) * transform.up;

        Vector3 torqueVector = Vector3.Cross(predictedUp, Vector3.up);
        rb.AddTorque(torqueVector * stabilizationForce * stabilizationSpeed);
    }
}
