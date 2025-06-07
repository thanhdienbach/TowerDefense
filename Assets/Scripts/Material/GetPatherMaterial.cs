using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class GetPatherMaterial : MonoBehaviour
{
    Renderer material;
    private void Start()
    {
        material = GetComponentInParent<Renderer>();
    }
    void Update()
    { 

    }
}
