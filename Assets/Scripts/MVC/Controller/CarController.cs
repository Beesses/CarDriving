using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private CarModel model;
    private bool vertHold = false;
    private bool vertHoldBack = false;
    private bool horHoldLeft = false;
    private bool horHoldRight = false;
    private void Awake()
    {
        model = new CarModel(Resources.Load<CarData>("SO/CarData"));
    }

    private float horizontalInput, verticalInput;
    private float currentSteerAngle, currentbreakForce;
    private bool isBreaking;

    [SerializeField] private WheelCollider frontLeftWheelCollider, frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider, rearRightWheelCollider;

    [SerializeField] private Transform frontLeftWheelTransform, frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform, rearRightWheelTransform;

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    private void GetInput()
    {
        if (vertHold || vertHoldBack)
        {
            if (vertHoldBack)
            {
                Back();
            }
            else
            {
                Gas();
            }
        }
        else
        {
            verticalInput = Mathf.MoveTowards(verticalInput, 0, Time.deltaTime);
        }
        if (horHoldLeft || horHoldRight)
        {
            if (horHoldLeft)
            {
                Left();
            }
            else
            {
                Right();
            }
        }
        else
        {
            horizontalInput = Mathf.MoveTowards(horizontalInput, 0, Time.deltaTime);
        }
    }

    public void Hold()
    {
        vertHold = true;
    }

    public void StopHold()
    {
        vertHold = false;

    }

    public void HoldBack()
    {
        vertHoldBack = true;
    }

    public void StopHoldBack()
    {
        vertHoldBack = false;

    }

    public void HoldLeft()
    {
        horHoldLeft = true;
    }

    public void StopHoldLeft()
    {
        horHoldLeft = false;

    }

    public void HoldRight()
    {
        horHoldRight = true;
    }

    public void StopHoldRight()
    {
        horHoldRight = false;

    }

    public void HoldBreak()
    {
        isBreaking = true;
    }

    public void StopHoldBreak()
    {
        isBreaking = false;

    }

    public void Gas()
    {
        verticalInput = Mathf.Lerp(verticalInput, 1, Time.deltaTime);
    }

    public void Back()
    {
        verticalInput = Mathf.Lerp(verticalInput, -1, Time.deltaTime);
    }

    public void Left()
    {
        horizontalInput = Mathf.Lerp(horizontalInput, -1, Time.deltaTime);
    }

    public void Right()
    {
        horizontalInput = Mathf.Lerp(horizontalInput, 1, Time.deltaTime);
    }

    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * model.motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * model.motorForce;
        currentbreakForce = isBreaking ? model.breakForce : 0f;
        ApplyBreaking();
    }

    private void ApplyBreaking()
    {
        frontRightWheelCollider.brakeTorque = currentbreakForce;
        frontLeftWheelCollider.brakeTorque = currentbreakForce;
        rearLeftWheelCollider.brakeTorque = currentbreakForce;
        rearRightWheelCollider.brakeTorque = currentbreakForce;
    }

    private void HandleSteering()
    {
        currentSteerAngle = model.maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
}
