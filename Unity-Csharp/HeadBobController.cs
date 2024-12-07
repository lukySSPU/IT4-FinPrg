using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBobController : MonoBehaviour
{
    public bool enableHeadBob;

    [SerializeField, Range(0, 0.1f)] private float amp = 0.015f;
    [SerializeField, Range(0, 30)] private float frequency = 10f;

    [SerializeField] private Transform cam = null;
    [SerializeField] private Transform cameraHolder = null;

    public float toggleSpeed = 3f;
    private Vector3 startPos;
    private CharacterController controller;

    private Vector3 previousPosition;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        startPos = cam.localPosition;
        previousPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!enableHeadBob) return;

        CheckMotion();
        ResetPosition();
        //cam.LookAt(FocusTarget());
    }

    void PlayMotion(Vector3 motion)
    {
        cam.localPosition += motion;
    }

    Vector3 FootStepMotion()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Sin(Time.time * frequency) * amp;
        pos.x += Mathf.Cos(Time.time * frequency / 2) * amp * 2;
        return pos;
    }

    void CheckMotion()
    {
        float speed;

        Vector3 currentPosition = transform.position;

        Vector3 movement = currentPosition - previousPosition;
        speed = new Vector3(movement.x, 0, movement.z).magnitude / Time.deltaTime;

        previousPosition = currentPosition;

        if (speed < toggleSpeed) return;
        if (!controller.isGrounded) return;

        PlayMotion(FootStepMotion());
    }

    void ResetPosition()
    {
        if (cam.localPosition == startPos) return;
        cam.localPosition = Vector3.Lerp(cam.localPosition, startPos, 1 * Time.deltaTime);
    }

    Vector3 FocusTarget()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + cameraHolder.localPosition.y, transform.localPosition.z);
        pos += cameraHolder.forward * 15f;
        return pos;
    }
}
