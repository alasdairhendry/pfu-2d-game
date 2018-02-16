using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellInventory : MonoBehaviour {

    public List<InventoryEntry> entries = new List<InventoryEntry>();

    public void AddShell(int shellID, int amount)
    {
        bool entryFound = false;

        for (int i = 0; i < entries.Count; i++)
        {
            if(entries[i].ShellID == shellID)
            {
                entries[i].Amount += amount;
                entryFound = true;
            }
        }

        if(!entryFound)
        {
            entries.Add(new InventoryEntry(shellID, amount));
        }
    }

    public void RemoveShell(int shellID, int amount)
    {
        for (int i = 0; i < entries.Count; i++)
        {
            if (entries[i].ShellID == shellID)
            {
                entries[i].Amount -= amount;

                if (entries[i].Amount <= 0)
                    entries.RemoveAt(i);
            }
        }
    }

    public bool CheckHasAmount(int shellID, int amount)
    {
        bool foundAmount = false;

        for (int i = 0; i < entries.Count; i++)
        {
            if (entries[i].ShellID == shellID)
            {
                if(entries[i].Amount >= amount)
                {
                    foundAmount = true;
                }
            }
        }

        return foundAmount;
    }
}

[System.Serializable]
public class InventoryEntry
{
    [SerializeField] private int shellID = 0;
    public int ShellID { get { return shellID; } }

    [SerializeField] private int amount = 1;
    public int Amount { get { return amount; } set { amount = value; } }

    public InventoryEntry(int shellID, int amount)
    {
        this.shellID = shellID;
        this.amount = amount;
    }
}
