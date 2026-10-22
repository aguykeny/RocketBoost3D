using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private InputAction thrust;
    [SerializeField] private InputAction rotation;

    [SerializeField] private float moveForce = 10f;
    [SerializeField] private float rotationForce = 2.5f;

    [SerializeField] private ParticleSystem thustParticlesSystem;
    [SerializeField] private ParticleSystem leftThustParticlesSystem;
    [SerializeField] private ParticleSystem rightThustParticlesSystem;

    Rigidbody rb;
    void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

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

    private void StopRotation()
    {
        leftThustParticlesSystem.Stop();
        rightThustParticlesSystem.Stop();
    }

    private void RotateLeft()
    {
        transform.Rotate(Vector3.back * rotationForce);
        if (!leftThustParticlesSystem.isPlaying)
        {
            leftThustParticlesSystem.Play();
            rightThustParticlesSystem.Stop();
        }
    }

    private void RotateRight()
    {
        transform.Rotate(Vector3.forward * rotationForce);
        if (!rightThustParticlesSystem.isPlaying)
        {
            rightThustParticlesSystem.Play();
            leftThustParticlesSystem.Stop();
        }
    }

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

    private void StopThrusting()
    {
        thustParticlesSystem.Stop();
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * moveForce);

        if (!thustParticlesSystem.isPlaying)
        {
            thustParticlesSystem.Play();
        }
    }
}
