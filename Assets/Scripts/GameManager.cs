using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

	public TMPro.TMP_Text hintText;
	public TMPro.TMP_Text resultText;
	public GameObject orderLayer;

	private float countDown = 60;

	private void Update()
	{
		countDown -= Time.deltaTime;
		if (countDown > 0)
		{
			hintText.text = "Time until gilfriend go:" + countDown.ToString("0");
		} else
		{
			hintText.text = "You divorce";
		}

	//if (Input.GetKeyDown(KeyCode.Z))
		{
			CheckOrder();
		}
	}

	public void CheckOrder()
	{
		//ObjectItem[] items = orderLayer.GetComponentsInChildren<ObjectItem>();
		ObjectItem[] items = GameObject.FindObjectsOfType<ObjectItem>();

		int notInOrder = 0;
		foreach (ObjectItem item in items)
		{
			if (!item.IsInplace()) notInOrder++;
		}

		resultText.text = "Not in order: " + notInOrder; 
	}

}
