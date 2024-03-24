using UnityEngine;

public class CarAcceleration : MonoBehaviour
{
    public float maxSpeed = 10f;
    public float acceleration = 5f;
    public float deceleration = 10f;
    public float brakeForce = 20f;
    public AudioClip motorcycleSound; // Sound clip for moving (forward or reverse)
    public AudioClip idleSound; // Sound clip for idle

    private AudioSource audioSource; // AudioSource component
    private float currentSpeed = 0f;
    private bool isMoving = false; // Track moving state

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // Initially play the idle sound
        audioSource.clip = idleSound;
        audioSource.loop = true;
        audioSource.Play();
    }

    private void Update()
    {
        float accelerateInput = Input.GetAxis("Vertical");
        bool brakeInput = Input.GetKey(KeyCode.Space);

        // Apply brake if brake input is detected
        if (brakeInput)
        {
            ApplyBrake(brakeForce * Time.deltaTime);
            if (isMoving)
            {
                // Switch to idle sound when braking
                SwitchToIdleSound();
                isMoving = false;
            }
        }
        else
        {
            if (Mathf.Abs(accelerateInput) > 0f) // Check for any acceleration (forward or reverse)
            {
                if (!isMoving)
                {
                    // Switch to motorcycle sound when starting to move
                    SwitchToMovingSound();
                    isMoving = true;
                }
                currentSpeed = Mathf.MoveTowards(currentSpeed, accelerateInput > 0 ? maxSpeed : -maxSpeed, acceleration * Time.deltaTime);
            }
            else if (isMoving)
            {
                // Switch to idle sound when stopping
                SwitchToIdleSound();
                isMoving = false;
            }
        }

        // Move the car based on the current speed
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
    }

    public void ApplyBrake(float force)
    {
        currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, force);
    }

    private void SwitchToMovingSound()
    {
        if (audioSource.clip != motorcycleSound)
        {
            audioSource.clip = motorcycleSound;
            audioSource.Play();
        }
    }

    private void SwitchToIdleSound()
    {
        if (audioSource.clip != idleSound)
        {
            audioSource.clip = idleSound;
            audioSource.Play();
        }
    }
}
