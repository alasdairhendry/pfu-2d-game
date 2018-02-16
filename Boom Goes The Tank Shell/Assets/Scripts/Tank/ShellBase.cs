using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShellBase {

    [SerializeField] private int id = 0;
    public int ID { get { return id; } }
    [SerializeField] private string name = "New Shell";
    public string Name { get { return name; } }
    [SerializeField] private string description = "New Description";
    public string Description { get { return description; } }
    [SerializeField] private GameObject prefab;
    public GameObject Prefab { get { return prefab; } }

    [SerializeField] private float firingVelocity = 15.0f;
    public float FiringVelocity { get { return firingVelocity; } }
    [SerializeField] private float blastRadius = 1.5f;
    public float BlastRadius { get { return blastRadius; } }
    [SerializeField] private float blastDamage = 10.0f;
    public float BlastDamage { get { return blastDamage; } }

    [SerializeField] private float costPerShell = 10.0f;
    public float CostPerShell { get { return costPerShell; } }

    public void SetID(MonoBehaviour sendType)
    {
        if (sendType.GetType() != Shells.singleton.GetType())
            return; 

        Debug.Log("boop");
    }
}
