using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shells : MonoBehaviour {

    public static Shells singleton;

    private void Awake()
    {
        if (singleton == null)
            singleton = this;
        else if (singleton != this)
            Destroy(gameObject);

        AssignIDs();
    }

    [SerializeField] private List<ShellBase> shells = new List<ShellBase>();

    private void AssignIDs ()
    {
        for (int i = 0; i < shells.Count; i++)
        {
            shells[i].SetID(this);
        }
    }

    public ShellBase FindShellByID(int id)
    {
        foreach (ShellBase item in shells)
        {
            if (item.ID == id)
                return item;
        }

        return null;
    }
}
