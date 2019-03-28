using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public TMPro.TMP_Text hintText;
    public TMPro.TMP_Text resultText;
    public GameObject orderLayer;

    public static GameManager instance = null;

    private GameInputModeEnum gameInputMode;
    public GameInputModeEnum GameInputMode
    {
        get
        {
            return gameInputMode;
        }
        set
        {
            gameInputMode = value;
            MouseToggle();
            PlayerMovementToggle();
        }
    }

    public float timeFlowSpeed;

    public const int dayLengthSeconds = 120;
    public const int initMoney = 200;
    public const int baseLootboxPrice = 50;
    public const int lootboxPriceAmp = 5;
    public const int lootboxItemCount = 10;
    public const int aptWeekRentCost = 150;
    public readonly int[,] componentValues = new int[,] { { 1, 4 }, { 3, 10 }, { 7, 20 } };
    public readonly int[,] componentProbabilities = new int[,] { { 45, 65 }, { 15, 45 }, { 10, 20 } };
  

    private float currTime = 0;
    private int handMoney = 0;
    private int aptBillWeek = 0;
    // todo



    public enum GameInputModeEnum
    {
        Play,
        Console
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        GameInputMode = GameInputModeEnum.Play;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (timeFlowSpeed != 0)
            {
                timeFlowSpeed = 0;
            }
            else
            {
                timeFlowSpeed = 1;
            }
        }

        currTime += Time.deltaTime * timeFlowSpeed;
        Debug.Log(currTime);
        if(currTime >= 120)
        {
            currTime = 0;
        }
    }

    public bool IsGameModeActive()
    {
        return gameInputMode == GameInputModeEnum.Play;
    }
    public bool IsConsoleModeActive()
    {
        return gameInputMode == GameInputModeEnum.Console;
    }

    private void MouseToggle()
    {
        switch (gameInputMode)
        {
            case GameInputModeEnum.Console:
                Camera.main.GetComponent<PlayerCameraControl_Example>().Sensitivity = 0;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
            case GameInputModeEnum.Play:
                Camera.main.GetComponent<PlayerCameraControl_Example>().Sensitivity = 3;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;
        }
    }
    private void PlayerMovementToggle()
    {
        switch (gameInputMode)
        {
            case GameInputModeEnum.Console:

                break;
            case GameInputModeEnum.Play:

                break;
        }
    }
}
