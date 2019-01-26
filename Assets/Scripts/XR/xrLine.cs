using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xrLine : MonoBehaviour
{
	private LineRenderer line;
	private xrHand hand;

	void Start()
	{
		line = GetComponent<LineRenderer>();
		hand = GetComponentInParent<xrHand>();
	}

	private void Update()
	{

		line.SetPosition(0, hand.ray.origin);
		line.SetPosition(1, hand.ray.origin + hand.ray.direction  * 1f);
	}

}
