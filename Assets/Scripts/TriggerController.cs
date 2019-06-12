using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerController : MonoBehaviour
{

	public ObjectController theObject;
	public GameObject desiredCollider;

	private void OnTriggerEnter(Collider other)
	{
		if (GameObject.ReferenceEquals(desiredCollider, other.gameObject))
			theObject.colliderHit();
	}

	private void OnTriggerExit(Collider other)
	{
		if (GameObject.ReferenceEquals(desiredCollider, other.gameObject))
			theObject.colliderExited();
	}
}
