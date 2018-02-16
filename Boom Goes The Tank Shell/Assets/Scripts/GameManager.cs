using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager singleton;

    private void Awake()
    {
        if (singleton == null)
            singleton = this;
        else if (singleton != this)
            Destroy(gameObject);
    }

    [SerializeField] private Tank.PlayerType playerTurn = Tank.PlayerType.First;
    [SerializeField] private GameObject tankPrefab;

    private Tank playerOneTank;
    public Tank GetPlayerOne { get { return playerOneTank; } }
    private Tank playerTwoTank;
    public Tank GetPlayerTwo { get { return playerTwoTank; } }

    private void Start()
    {
        SpawnTanks();
        SetUITanks();
    }

    private void SpawnTanks()
    {
        playerOneTank = Instantiate(tankPrefab).GetComponent<Tank>();
        playerTwoTank = Instantiate(tankPrefab).GetComponent<Tank>();

        playerOneTank.transform.position = new Vector3(-15.0f, 0.0f, 0.0f);
        playerTwoTank.transform.position = new Vector3(15.0f, 0.0f, 0.0f);

        playerOneTank.transform.eulerAngles = new Vector3(0.0f, 90.0f, 0.0f);
        playerTwoTank.transform.eulerAngles = new Vector3(0.0f, -90.0f, 0.0f);

        playerOneTank.Setup("Alipwnzor", Tank.ControlType.Player, Tank.PlayerType.First);
        playerTwoTank.Setup("Ross", Tank.ControlType.Player, Tank.PlayerType.Second);
    }

    private void SetUITanks()
    {
        PlayerUIPanel[] panels = GameObject.FindObjectsOfType<PlayerUIPanel>();

        foreach (PlayerUIPanel item in panels)
        {
            if (item.GetPlayerType == Tank.PlayerType.First)
                item.SetTargetTank(playerOneTank);
            else item.SetTargetTank(playerTwoTank);
        }
    }

    public void SwitchTurns()
    {
        if (playerTurn == Tank.PlayerType.First)
        {
            playerTurn = Tank.PlayerType.Second;
            GameObject.Find("TurnIndicatior_Text").GetComponent<Text>().text = playerTwoTank.Username;
        }
        else if (playerTurn == Tank.PlayerType.Second)
        {
            playerTurn = Tank.PlayerType.First;
            GameObject.Find("TurnIndicatior_Text").GetComponent<Text>().text = playerOneTank.Username;
        }
    }

    public bool CheckTurn(Tank.PlayerType requestorType)
    {
        return requestorType == playerTurn;        
    }
}
