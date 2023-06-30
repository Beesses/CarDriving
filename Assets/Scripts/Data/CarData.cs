using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CarData", menuName = "Data/CarData")]
public class CarData : ScriptableObject
{
    public float motorForce;
    public float breakForce;
    public float maxSteerAngle;
}
