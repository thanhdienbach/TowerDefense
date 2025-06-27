using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    public void LookToTarget(Transform target)
    {
        transform.LookAt(target);
    } 
    
}
