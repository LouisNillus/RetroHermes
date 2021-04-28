using System.Collections.Generic;
using UnityEngine;

public class CargoContainer <T> : MonoBehaviour
{
    public List<T> myCargoList = new List<T>();
}