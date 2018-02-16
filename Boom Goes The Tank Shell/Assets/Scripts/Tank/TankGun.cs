using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankGun : MonoBehaviour {

    private ShellBase currentShell;
    [SerializeField] private ShellInventory inventory;
    private GameObject firepoint;

    private bool firingStarted = false;
    public bool IsFiring { get { return firingStarted; } }

    [SerializeField] private float inputFiringPower = 0.0f;

    private void Start()
    {
        inventory = GetComponent<ShellInventory>();
        firepoint = transform.Find("GunRotationParent").Find("Gun").Find("FirePoint").gameObject;
        inventory.AddShell(0, 5);
        currentShell = Shells.singleton.FindShellByID(0);
    }

    public void ChangeShell(int shellID)
    {
        ShellBase shell = Shells.singleton.FindShellByID(shellID);

        if (shell == null) return;

        currentShell = shell;
    }

    public void Fire()
    {
        if (inventory.CheckHasAmount(currentShell.ID, 1) == false)
            return;

        if (!firingStarted)
        {
            firingStarted = true;
        }
        else
        {

            inventory.RemoveShell(currentShell.ID, 1);

            GameObject go = Instantiate(currentShell.Prefab);
            go.transform.position = firepoint.transform.position;
            go.transform.eulerAngles = firepoint.transform.eulerAngles;

            float power = currentShell.FiringVelocity * Mathf.Clamp(inputFiringPower, 0.25f, 1.0f);
            //go.GetComponent<Rigidbody>().AddForce(go.transform.forward * power, ForceMode.Impulse);
            go.GetComponent<Rigidbody>().velocity = (go.transform.forward * power);
            go.GetComponent<Shell>().SetShell(currentShell);
            firingStarted = false;
        }
    }

    public void SetFiringPower(float power)
    {
        inputFiringPower = power;
    }
}
