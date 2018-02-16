using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankControls : MonoBehaviour {

    [SerializeField] private float movementSpeed = 15.0f;
    [SerializeField] private float gunRotationSpeed = 150.0f;

    private GameObject gunRotationParent;

    private new Rigidbody rigidbody;
    private Tank tank;

	// Use this for initialization
	void Start () {
        gunRotationParent = transform.Find("GunRotationParent").gameObject;
        rigidbody = GetComponent<Rigidbody>();
        tank = GetComponent<Tank>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!GameManager.singleton.CheckTurn(tank.GetPlayerType))
            return;

        Rotation();
        Fire();
        SwitchShell();
	}

    private void FixedUpdate()
    {
        if (!GameManager.singleton.CheckTurn(tank.GetPlayerType))
            return;

        Movement();
    }

    private void Movement()
    {
        if (tank.GetPlayerType == Tank.PlayerType.First)
            rigidbody.velocity = new Vector3(Input.GetAxis("P1Horizontal"), rigidbody.velocity.y, 0.0f) * movementSpeed;
        else
            rigidbody.velocity = new Vector3(Input.GetAxis("P2Horizontal"), rigidbody.velocity.y, 0.0f) * movementSpeed;
    }

    private void Rotation()
    {           
        if (tank.GetPlayerType == Tank.PlayerType.First)
            gunRotationParent.transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f) * Input.GetAxis("P1Vertical") * gunRotationSpeed * Time.deltaTime);
        else gunRotationParent.transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f) * Input.GetAxis("P2Vertical") * gunRotationSpeed * Time.deltaTime);
    }

    private void Fire()
    {
        if (tank.GetPlayerType == Tank.PlayerType.First)
        {
            if (Input.GetKeyDown(KeyCode.W))
                GetComponent<TankGun>().Fire();
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.I))
            {
                GetComponent<TankGun>().Fire();
            }
        }                
    }

    private void SwitchShell()
    {
        KeyCode keycode;
        keycode = (tank.GetPlayerType == Tank.PlayerType.First) ? KeyCode.S : KeyCode.K;

        if(Input.GetKeyDown(keycode))
        {
            GetComponent<TankGun>().Fire();
        }
    }
}
