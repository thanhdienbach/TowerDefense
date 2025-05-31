using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth;
    public float curentHealth;
    public void Init(UnitConfig config)
    {
        maxHealth = config.maxHP;
        curentHealth = maxHealth;
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
