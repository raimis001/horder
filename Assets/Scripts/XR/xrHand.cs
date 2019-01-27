using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xrHand : BaseMotion
{
	enum HandAnims
	{
		Idle, Point, GrabLarge, GrabSmall, GrabStickUp, GrabStickFront,
		ThumbUp, Fist, Gun, GunShoot, PushButton, Spread, MiddleFinger, Peace, OK,
		Phone, Rock, Natural, Number3, Number4, Number3V2
	}

	public static xrHand RightHand;
	public static xrHand LeftHand;

	public HandKind kind;
	public LayerMask objectLayer;
	public Transform takePoint;

	[Range(0, 5)]
	public float rotateSpeed = 1;

	private Animator anim;
	private HandAnims animState;

	string touchpadX;
	string touchpadY;
	string touchpad;
	string menupad;
	string xAxis;
	string yAxis;

	internal bool isEmpty
	{
		get { return !pickedUpObject; }
	}

	internal Ray ray
	{
		get { return new Ray(takePoint.position, takePoint.forward); }
	}

	void Start()
	{
		anim = GetComponentInChildren<Animator>();
		Play(HandAnims.Natural);
		//kind = tag == "LeftHand" ? HandKind.Left : HandKind.Right;

		touchpadX = kind == HandKind.Left ? "LeftTouchX" : "RightTouchX";
		touchpadY = kind == HandKind.Left ? "LeftTouchY" : "RightTouchY";
		touchpad = kind == HandKind.Left ? "joystick button 16" : "joystick button 17";
		menupad = kind == HandKind.Left ? "joystick button 6" : "joystick button 7";
		xAxis = kind == HandKind.Left ? "Horzontal" : "Rotate";
		yAxis = kind == HandKind.Left ? "Vertical" : "RotateX";

		if (kind == HandKind.Left) LeftHand = this;
		if (kind == HandKind.Right) RightHand = this;
	}

	private void Update()
	{

		if (TriggerDown())
		{

			if (!Physics.Raycast(ray, out RaycastHit hit, 1, objectLayer)) return;

            pickedUpObject = hit.collider.gameObject;
            pickedUpObject.transform.SetParent(transform);
            //pickedUpObject.GetComponent<Rigidbody>().useGravity = false;
			PickupItem(pickedUpObject);
			Play(HandAnims.GrabSmall);
			return;
		}

		if (!pickedUpObject) return;


		if (TriggerUp())
		{
      pickedUpObject.transform.SetParent(null);
			throwStrength = Mathf.Abs(Input.GetAxis(yAxis));
			Throw();
			pickedUpObject = null;
			Play(HandAnims.Natural);
			return;
		}

		if (TouchpadKey())
		{
			Vector2 t = Touchpad() * rotateSpeed;
			Rotate(new Vector3(t.x, 0, t.y));
		}
	}



	private void Play(HandAnims trigger = HandAnims.Natural)
	{
		if (!anim) return;
		if (animState == trigger) return;

		animState = trigger;
		anim.SetTrigger(trigger.ToString());
	}


	#region INPUT

	public bool Trigger()
	{
		return xrMotion.GetTrigger(kind);
	}
	public bool TriggerDown()
	{
		return xrMotion.GetTriggerDown(kind);
	}
	public bool TriggerUp()
	{
		return xrMotion.GetTriggerUp(kind);
	}
	public float TriggerValue()
	{
		return 0;
	}

	public bool TouchpadDown()
	{
		return Input.GetKeyDown(touchpad);
	}
	public bool TouchpadUp()
	{
		return Input.GetKeyUp(touchpad);
	}
	public bool TouchpadKey()
	{
		return Input.GetKey(touchpad);
	}

	public Vector2 Touchpad()
	{
		Vector2 result = Vector2.zero;

		result.x = Input.GetAxis(touchpadX);
		result.y = Input.GetAxis(touchpadY);

		//Debug.Log(result);
		return result;

	}

	public bool MenuDown()
	{
		return Input.GetKeyDown(menupad);
	}
	public bool MenuUp()
	{
		return Input.GetKeyUp(menupad);
	}

	public bool MenuKey()
	{
		return Input.GetKey(menupad);
	}

	#endregion
}
