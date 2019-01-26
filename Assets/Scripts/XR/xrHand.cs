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
	public Transform takePoint;
	public LayerMask objectLayer;

	[Range(0,5)]
	public float rotateSpeed = 1;

	private Animator anim;
	private HandAnims animState;

	private ObjectItem takeItem;
	private Transform takeParent;

	string touchpadX;
	string touchpadY;
	string touchpad;
	string menupad;

	Vector3 oldPosition;

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
		if (kind == HandKind.Left) LeftHand = this;
		if (kind == HandKind.Right) RightHand = this;
	}

	private void Update()
	{
		if (TriggerDown())
		{
			//Debug.Log("Trigger down");

			if (!Physics.Raycast(ray, out RaycastHit hit, 1, objectLayer)) return;
			takeItem = hit.transform.GetComponent<ObjectItem>();
			if (!takeItem) return;

			takeParent = takeItem.transform.parent;
			takeItem.transform.SetParent(transform);
			//takeItem.GetComponent<Rigidbody>().isKinematic = true;
			takeItem.GetComponent<Rigidbody>().useGravity = false;
			Play(HandAnims.GrabSmall);
			return;
		}

		if (!takeItem) return;
	

		if (TriggerUp())
		{
			takeItem.transform.SetParent(takeParent);
			Vector3 dir = takeItem.transform.position - oldPosition;

			Rigidbody rigi = takeItem.GetComponent<Rigidbody>();
			//rigi.isKinematic = false;
			rigi.useGravity = true;
			rigi.AddForce(dir * 100, ForceMode.Impulse);

			takeItem = null;
			takeParent = null;
			Play(HandAnims.Natural);
			return;
		}

		if (TouchpadKey())
		{
			Vector2 t = Touchpad() * rotateSpeed;

			float rotX = t.y;
			float rotY = t.x;
			takeItem.transform.Rotate(Vector3.up, -rotY, Space.World);
			takeItem.transform.Rotate(transform.right, -rotX, Space.World);

		}
		oldPosition = takeItem.transform.position;
	}

	private void FixedUpdate()
	{
		if (!takeItem) return;
		//oldPosition = takeItem.transform.position;
	}

	private void Play(HandAnims trigger = HandAnims.Natural)
	{
		if (!anim) return;
		if (animState == trigger) return;

		animState = trigger;
		anim.SetTrigger(trigger.ToString());
	}

	#region EVENTS
	private void OnEnable()
	{
		ObjectAccepter.OnDeleteItem += OnDelete;
	}
	private void OnDisable()
	{
		ObjectAccepter.OnDeleteItem -= OnDelete;
	}
	private void OnDelete(ObjectItem item)
	{
		if (takeItem)
		{
			takeItem.transform.SetParent(takeParent);
			takeItem.GetComponent<Rigidbody>().isKinematic = false;
			takeItem = null;
			takeParent = null;
		}
		Play(HandAnims.Natural);
	}
	#endregion

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
