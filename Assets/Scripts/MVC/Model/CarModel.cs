using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarModel
{
    public float motorForce;
    public float breakForce;
    public float maxSteerAngle;
    public CarModel(CarData data)
    {
        motorForce = data.motorForce;
        breakForce = data.breakForce;
        maxSteerAngle = data.maxSteerAngle;
    }
}
