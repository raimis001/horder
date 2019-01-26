using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAccepter : MonoBehaviour
{
	public delegate void DeleteEvent(ObjectItem item);
	public static event DeleteEvent OnDeleteItem;

	public ObjectKind acceptKind;

	private ObjectMover mover;

	private void Start()
	{
		mover = Camera.main.gameObject.GetComponent<ObjectMover>();
	}

	private void OnTriggerEnter(Collider other)
	{
		ObjectItem obj = other.GetComponent<ObjectItem>();
		if (!obj) return;

		if (obj.kind != acceptKind) return;

		OnDeleteItem.Invoke(obj);

		Destroy(obj.gameObject);
	}


}
