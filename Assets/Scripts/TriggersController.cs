using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggersController : MonoBehaviour
{
	public ObjectsController theObjectsController;
	public GameObject desiredCollider;
	private IEnumerator _validCoroutine;

	private void OnTriggerEnter(Collider other)
	{
		if (GameObject.ReferenceEquals(desiredCollider, other.gameObject))
		{
			Debug.Log("Triggered!");
			Debug.Log("other.gameObject.name: " + other.gameObject.name);
			_validCoroutine = ValidateShadow();
			StartCoroutine(_validCoroutine);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (GameObject.ReferenceEquals(desiredCollider, other.gameObject))
		{
			StopCoroutine(_validCoroutine);
			theObjectsController.colliderExited();
		}
	}

	private IEnumerator ValidateShadow()
	{
		yield return new WaitForSeconds(1f);
		theObjectsController.colliderHit();
	}
}
