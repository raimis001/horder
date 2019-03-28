using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleManager : MonoBehaviour
{
    private bool isConsoleActive = false;

    [SerializeField] Text consoleOutput;
    [SerializeField] InputField consoleInput;
    [SerializeField] Canvas configCanvas;

    List<string> commandWords = new List<string>();
    StringBuilder consoleContents = new StringBuilder();

    List<string> inputKeys = new List<string>();
    List<string> configParameters = new List<string>();

    private void Awake()
    {
        configCanvas.gameObject.SetActive(false);
        LoadConfig();
        consoleOutput.GetComponent<Text>().text = "";
        consoleContents.Clear();
        PopulateInputKeys();
        PopulateParameters();
    }

    private void LoadConfig()
    {
        if(PlayerPrefs.GetInt("configIsSet") == 0)
        {
            // load default
        }
        else
        {
            // load from file
        }
    }

    /// <summary>
    /// Init keyword list
    /// </summary>
    private void PopulateInputKeys()
    {
        inputKeys.Add("set");
    }
    /// <summary>
    /// Init parameter list
    /// </summary>
    private void PopulateParameters()
    {
        configParameters.Add("lootboxDropAmount");
    }

    
    /// <summary>
    /// if input contains any predefined key from InputKeys list
    /// </summary>
    /// <param name="input"> Input string form UI console </param>
    /// <returns></returns>
    private bool ContainsKeys(string input)
    {
        string[] potentialKeys = input.Split(' ');

        return inputKeys.Contains(potentialKeys[0]);
    }
    /// <summary>
    /// if input contains any predefined param from Parameters list
    /// </summary>
    /// <param name="input"> Input string form UI console </param>
    /// <returns></returns>
    private bool ContainsParam(string input)
    {
        string[] potentialParam = input.Split(' ');

        return configParameters.Contains(potentialParam[1]);
    }

    /// <summary>
    /// called on press Enter UI EditField
    /// </summary>
    public void GetInput()
    {
        Debug.Log("called");
        string[] lines = consoleContents.ToString().Split(' ');

        string input = consoleInput.GetComponent<InputField>().text;
        
        if (String.IsNullOrEmpty(input))
        {
            return;
        }

        consoleContents.AppendLine(input);
        consoleOutput.GetComponent<Text>().text = consoleContents.ToString();
        consoleInput.GetComponent<InputField>().text = "";
        consoleInput.ActivateInputField();

        if (!ContainsKeys(input) || !ContainsParam(input))
        {
            return;
        }

        string containedKeyword = GetInputKeyword(input);

        Debug.Log("contains keyword");
        // TODO: execute config alter
    }

    private string GetInputKeyword(string input)
    {
        return input.Split(' ')[0];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isConsoleActive = !isConsoleActive;
            GameManager.instance.GameInputMode = (isConsoleActive) ?  GameManager.GameInputModeEnum.Console : GameManager.GameInputModeEnum.Play;

            consoleInput.GetComponent<InputField>().text = "";
            configCanvas.gameObject.SetActive(isConsoleActive);
        }
    }

    private void ExecuteKeyword(string keyword )
    {
        PlayerPrefs.SetInt("configIsSet", 1);

        switch (keyword)
        {
            case "set":
                
                break;
            case "print":

                break;
            case "spawn":

                break;
            default:

                break;
        }
    }
}