using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyCam : MonoBehaviour
{
    public string horizontalAxisName = "Horizontal";
    public string verticalAxisName = "Vertical";
    public string forwardAxisName = "Jump";
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;
    public float maxSpeed = 100f;
    public float accelerationRate = 10f;
    public float decelerationRate = 10f;
    public float targetSpeed = 5f;
    public float rotationSpeed = 5f;

    private float speedMod = 1f;
    private Vector3 moveDirection, targetVelocity;
    private Vector3 targetPosition;
    private float x, y;
    private bool isMoving;

    private bool isRotating;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

    // Update is called once per frame
    void Update()
    {
        float deltaTime = Time.deltaTime;

        targetPosition = transform.position;
        moveDirection = Vector3.zero;
        bool move = isMoving;

        if (Input.GetKey(KeyCode.W))
        {
            move = true;
            moveDirection += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            move = true;
            moveDirection -= Vector3.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            move = true;
            moveDirection -= Vector3.right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            move = true;
            moveDirection += Vector3.right;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            move = true;
            moveDirection -= Vector3.up;
        }
        if (Input.GetKey(KeyCode.E))
        {
            move = true;
            moveDirection += Vector3.up;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speedMod = 3f;
        }
        else
        {
            speedMod = 1f;
        }

        if (Input.GetMouseButtonDown(1))
        {
            isRotating = !isRotating;
        }

        if (isRotating)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.x = (mousePos.x / Screen.width) - 0.5f;
            mousePos.y = (mousePos.y / Screen.height) - 0.5f;
            x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
            y = Mathf.Clamp(y, -90, 90);
        }
        Quaternion rotation = Quaternion.Euler(y, x, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

        if (move)
        {
            targetSpeed = Mathf.Clamp(targetSpeed * (1f + Input.mouseScrollDelta.y * 0.05f), 0f, maxSpeed);
            targetVelocity = Vector3.Lerp(targetVelocity, moveDirection * targetSpeed, accelerationRate * deltaTime);
        }
        else
        {
            targetVelocity = Vector3.Lerp(targetVelocity, moveDirection * targetSpeed, decelerationRate * deltaTime);
        }

        targetPosition = transform.position + transform.TransformVector(targetVelocity * deltaTime * speedMod);
        transform.position = targetPosition;
    }
}
