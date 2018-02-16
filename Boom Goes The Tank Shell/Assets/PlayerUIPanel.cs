using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIPanel : MonoBehaviour {

    [SerializeField] private Tank.PlayerType playerType = Tank.PlayerType.First;
    public Tank.PlayerType GetPlayerType { get { return playerType; } }

    [SerializeField] private RectTransform healthFillPanel;

    [SerializeField] private Text firingAngleText;
    [SerializeField] private RectTransform fireVelocityPanel;
    [SerializeField] private RectTransform fireVelocityMarker;
    [SerializeField] private float firingMarkerMovementSpeed = 5.0f;
    private float firingMarkerCounter = 0.0f;
    private bool isFiringPreviously = false;
    private bool countingUp = false;

    private Tank targetTank;
    private TankGun tankGun;
    
    private void Update()
    {
        MonitorFiringAngle();
        MonitorFiringPower();
        MonitorHealthPanel();
    }

    public void SetTargetTank(Tank tank)
    {
        targetTank = tank;
        tankGun = targetTank.GetComponent<TankGun>();
    }

    private void MonitorHealthPanel()
    {                
        float percent = targetTank.CurrentHealth / 100.0f;

        float position = 0.0f;

        if (playerType == Tank.PlayerType.First)
        {
            position = Mathf.Lerp(-256.0f, 0.0f, percent);
            healthFillPanel.offsetMax = new Vector2(position, healthFillPanel.offsetMax.y);
        }
        else
        {            
            position = Mathf.Lerp(256.0f, 0.0f, percent);
            healthFillPanel.offsetMin = new Vector2(position, healthFillPanel.offsetMin.y);
        }
    }

    private void MonitorFiringAngle()
    {
        float angle = targetTank.transform.Find("GunRotationParent").transform.eulerAngles.z;

        if (angle > 180.0f) angle -= 360.0f;

        if (angle < 0.0f) angle *= -1.0f;

        firingAngleText.text = angle.ToString("0") + "*";
    }

    private void MonitorFiringPower()
    {
        if (tankGun.IsFiring)
        {
            if (!isFiringPreviously)
            {
                firingMarkerCounter = 0.0f;
                countingUp = true;
            }

            if (playerType == Tank.PlayerType.First)
            {
                if (countingUp)
                {
                    firingMarkerCounter += Time.deltaTime * firingMarkerMovementSpeed;

                    if (firingMarkerCounter >= 112)
                    {
                        countingUp = false;
                    }

                }
                else
                {
                    firingMarkerCounter -= Time.deltaTime * firingMarkerMovementSpeed;

                    if (firingMarkerCounter <= -112)
                    {
                        countingUp = true;
                    }
                }
            }
            else
            {
                if (!countingUp)
                {
                    firingMarkerCounter += Time.deltaTime * firingMarkerMovementSpeed;

                    if (firingMarkerCounter >= 112)
                    {
                        countingUp = true;
                    }

                }
                else
                {
                    firingMarkerCounter -= Time.deltaTime * firingMarkerMovementSpeed;

                    if (firingMarkerCounter <= -112)
                    {
                        countingUp = false;
                    }
                }
            }

            fireVelocityMarker.anchoredPosition3D = new Vector3(firingMarkerCounter, 0.0f, 0.0f);

            if (playerType == Tank.PlayerType.First)
                tankGun.SetFiringPower(Mathf.InverseLerp(-112.0f, 112.0f, firingMarkerCounter));
            else
                tankGun.SetFiringPower(Mathf.InverseLerp(112.0f, -112.0f, firingMarkerCounter));
        }

        isFiringPreviously = tankGun.IsFiring;
    }
}
