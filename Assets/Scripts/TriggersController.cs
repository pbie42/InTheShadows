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
			Debug.Log("desiredCollider.name: " + desiredCollider.name);
			Debug.Log("other.gameObject.name: " + other.gameObject.name);
			Debug.Log("\n");
			_validCoroutine = ValidateShadow();
			StartCoroutine(_validCoroutine);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (GameObject.ReferenceEquals(desiredCollider, other.gameObject))
		{
			Debug.Log("LEAVING desiredCollider.name: " + desiredCollider.name);
			Debug.Log("LEAVING other.gameObject.name: " + other.gameObject.name);
			Debug.Log("\n");
			StopCoroutine(_validCoroutine);
			theObjectsController.colliderExited();
		}
	}

	private IEnumerator ValidateShadow()
	{
		yield return new WaitForSeconds(0f);
		theObjectsController.colliderHit();
	}
}
