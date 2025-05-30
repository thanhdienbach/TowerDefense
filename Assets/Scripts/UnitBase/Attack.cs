using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float damage;
    List<Weapon> weapons = new List<Weapon>();
    public int id;

    public void Init(UnitConfig config)
    {
        damage = config.damage;
        Debug.Log("Inited attack");
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
