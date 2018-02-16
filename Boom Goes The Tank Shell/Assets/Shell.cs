using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour {

    protected bool hasExploded = false;
    protected ShellBase shell;

    private void Start()
    {
        shell = Shells.singleton.FindShellByID(0);
    }

    protected virtual void Update()
    {                
        CheckDeath();    
    }

    public void SetShell(ShellBase shell)
    {
        this.shell = shell;
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (hasExploded) return;
        hasExploded = true;

        RaycastHit[] hits = Physics.SphereCastAll(transform.position, shell.BlastRadius, transform.up);

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.gameObject.GetComponent<Tank>())
            {
                Debug.Log("Boop - - - " +  Vector3.Distance(transform.position, hit.collider.transform.position / shell.BlastRadius));

                float damage = shell.BlastDamage *  Mathf.Lerp(0.0f, 1.0f, shell.BlastRadius/ Vector3.Distance(transform.position, hit.collider.transform.position));  
                
                Debug.Log("Damage should have been " + shell.BlastDamage + " but blast damage is " + damage + " -- Distance was " + Vector3.Distance(transform.position, hit.collider.transform.position));

                hit.collider.gameObject.GetComponent<Tank>().TakeDamage(damage);
                hit.collider.GetComponent<Rigidbody>().AddForceAtPosition(new Vector3(damage, damage, damage) * Time.deltaTime, transform.position, ForceMode.Impulse);
            }
        }
    }

    protected virtual void CheckDeath()
    {
        if(hasExploded)
        {
            if(GetComponent<Rigidbody>().IsSleeping())
            {
                Destroy(gameObject);
            }
        }
    }

    protected virtual void OnDestroy()
    {
        //GameManager.singleton.SwitchTurns();
    }

}
