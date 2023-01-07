using Assets.Assets.Script.Configuration;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCameraController : MonoBehaviour
{

    [SerializeField]
    Transform target;

    [SerializeField]
    Vector3 offset;

    [SerializeField]
    float pitch = 2f;

    [SerializeField]
    float zoomSpeed = 4f;

    [SerializeField]
    float minZoom = 5f;

    [SerializeField]
    float maxZoom = 10f;

    [SerializeField]
    float yawSpeed = 100f;

    private float _currentZoom = 10f;
    private float _currentYaw = 0f;


    // Update is called once per frame
    void Update()
    {
        _currentZoom -= Input.GetAxis(ButtonConfiguration.MouseScrollWheel) * zoomSpeed;
        _currentZoom = Mathf.Clamp(_currentZoom, minZoom, maxZoom);

        _currentYaw -= Input.GetAxis(ButtonConfiguration.RotateCam) * yawSpeed * Time.deltaTime;
    }

    private void LateUpdate()
    {
        transform.position = target.position - offset * _currentZoom;
        transform.LookAt(target.position + Vector3.up * pitch);

        transform.RotateAround(target.position, Vector3.up, _currentYaw);
    }
}
