using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class xrStarter : MonoBehaviour
{
	enum TrackingSpace
	{
		Stationary,
		RoomScale
	}

	public GameObject xrLayer;
	public GameObject pcLayer;

	[Header("Camera Settings")]

	[SerializeField]
	TrackingSpace trackingSpace = TrackingSpace.Stationary;

	[SerializeField]
	float cameraOffset = 1.36144f;

	void Awake()
	{
		Debug.LogFormat("XR {0}", XRSettings.enabled ? "enabled" : "disabled");
		xrLayer.SetActive(XRSettings.enabled);
		pcLayer.SetActive(!XRSettings.enabled);

		if (XRSettings.enabled) SetCameraHeight();
	}

	private void Update()
	{
		if (!XRSettings.enabled) return;

		if (xrHand.LeftHand.MenuDown())
		{
			SetCameraHeight();
		}

	}

	void SetCameraHeight()
	{
		float cameraYOffset = cameraOffset;
		if (trackingSpace == TrackingSpace.Stationary)
		{
			XRDevice.SetTrackingSpaceType(TrackingSpaceType.Stationary);
			InputTracking.Recenter();
		}
		else if (trackingSpace == TrackingSpace.RoomScale)
		{
			if (XRDevice.SetTrackingSpaceType(TrackingSpaceType.RoomScale))
				cameraYOffset = 0;
		}

		//Move camera to correct height
		xrLayer.transform.localPosition = new Vector3(xrLayer.transform.localPosition.x, cameraYOffset, xrLayer.transform.localPosition.z);
	}

	

}
