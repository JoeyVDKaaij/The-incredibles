using System.Collections.Generic;
using UnityEngine;

public class DeleteRope : MonoBehaviour
{
    private void OnEnable()
    {
        Arrow.OnArrowHitRope += Delete;
    }
    private void OnDisable()
    {
        Arrow.OnArrowHitRope -= Delete;
    }
    
    private void Delete()
    {
        Destroy(gameObject, 0.5f);
    }
}
