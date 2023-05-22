using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] InputAction movement;
    [SerializeField] float controlSpeed = 30f;
    [SerializeField] GameObject[] lasers; 
    [SerializeField] InputAction fire;

    float xThrow, yThrow;

    float xRange = 16f;
    float yRange = 9f;

    float posPitchFactor = -2f;
    float ctrlPitchFactor = -16f;
    float posYawFactor = 2f;
    float ctrlRollFactor = -20f;

    private void OnEnable()
    {
        movement.Enable();
        fire.Enable();
    }

    private void OnDisable()
    {
        movement.Disable();
        fire.Disable();
    }

    void Update()
    {
        Move();   
        Rotate();
        ProcessFiring();
    }

    void Move()
    {
        xThrow = movement.ReadValue<Vector2>().x;
        yThrow = movement.ReadValue<Vector2>().y;

        // X-Axis
        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float newXpos = transform.localPosition.x + xOffset;
        float clampedXpos = Mathf.Clamp(newXpos, -xRange, xRange);

        // Y-Axis
        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float newYpos = transform.localPosition.y + yOffset;
        float clampedYpos = Mathf.Clamp(newYpos, -yRange, yRange);

        transform.localPosition = new Vector3 (clampedXpos,
            clampedYpos,
            transform.localPosition.z);
    }

    void Rotate()
    {
        float pitchDueToPos = transform.localPosition.y * posPitchFactor;
        float pitchDueToThrow = yThrow * ctrlPitchFactor;

        float pitch = pitchDueToPos + pitchDueToThrow;
        float yaw = transform.localPosition.x * posYawFactor;
        float roll = xThrow * ctrlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProcessFiring()
    {
        if (fire.ReadValue<float>() > 0.5f)
        {
            SetLasersActive(true);
        }
        else
        {
            SetLasersActive(false);
        }
    }

    void SetLasersActive(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }
}
