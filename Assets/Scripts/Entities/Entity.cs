using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [field: SerializeField] public string Name { get; protected set; } = "Default Entity Name";
}