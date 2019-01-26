using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

	public TMPro.TMP_Text hintText;
	public TMPro.TMP_Text resultText;
	public GameObject orderLayer;

	private void Update()
	{
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("StartScene");
            Cursor.lockState = CursorLockMode.None;
        }

	}
}
