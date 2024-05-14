using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FilterFlockBehaviour : FlockBehavior
{
    [SerializeField] protected ContextFilter filter;
}
