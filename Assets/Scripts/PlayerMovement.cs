using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Input Actions")]
    [SerializeField] private InputAction thrust;
    [SerializeField] private InputAction rotation;

    [Header("Movement Settings")]
    [SerializeField] private float moveForce = 10f;
    [SerializeField] private float rotationForce = 2.5f;

    [Header("Particle Systems")]
    [SerializeField] private ParticleSystem thustParticlesSystem;
    [SerializeField] private ParticleSystem leftThustParticlesSystem;
    [SerializeField] private ParticleSystem rightThustParticlesSystem;

    [Header("Audio")]
    [SerializeField] private AudioClip thrustAudioClip;
    

    Rigidbody rb;
    AudioSource audioSource;
    void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
    void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }
    /// <summary>
    /// Processes the rotation input action. Depending on the input value,
    /// it rotates the player left or right, or stops the rotation if no input is detected.
    /// </summary>
    private void ProcessRotation()
    {
        rb.freezeRotation = true; //freezing rotation so we can manually rotate
        float rotationValue = rotation.ReadValue<float>();

        if (rotationValue < 0)
        {
            RotateRight();
        }
        else if (rotationValue > 0)
        {
            RotateLeft();
        }
        else
        {
            StopRotation();
        }
    }
    /// <summary>
    /// Stops the rotation of the player by stopping both left and right thrust particle systems.
    /// </summary>
    private void StopRotation()
    {
        leftThustParticlesSystem.Stop();
        rightThustParticlesSystem.Stop();
    }
    /// <summary>
    /// Rotates the player to the left by applying a rotation force and managing the particle systems for visual feedback.
    /// </summary>
    private void RotateLeft()
    {
        transform.Rotate(Vector3.back * rotationForce);
        if (!leftThustParticlesSystem.isPlaying)
        {
            leftThustParticlesSystem.Play();
            rightThustParticlesSystem.Stop();
        }
    }
    /// <summary>
    /// Rotates the player to the right by applying a rotation force and managing the particle systems for visual feedback.
    /// </summary>
    private void RotateRight()
    {
        transform.Rotate(Vector3.forward * rotationForce);
        if (!rightThustParticlesSystem.isPlaying)
        {
            rightThustParticlesSystem.Play();
            leftThustParticlesSystem.Stop();
        }
    }
    /// <summary>
    /// Processes the thrust input action. If the thrust action is pressed,
    /// it starts the thrusting action; otherwise, it stops the thrusting action.
    /// </summary>
    private void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }
    /// <summary>
    /// Stops the thrusting action by stopping the particle system and audio playback.
    /// </summary>
    private void StopThrusting()
    {

        thustParticlesSystem.Stop();
        audioSource.Stop();
    }
    /// <summary>
    /// Starts the thrusting action by applying force to the Rigidbody, 
    /// playing the particle system, and playing the audio clip if they are not already active.
    /// </summary>
    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * moveForce);

        if (!thustParticlesSystem.isPlaying)
        {
            thustParticlesSystem.Play();
        }
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(thrustAudioClip);
        }
    }
}
