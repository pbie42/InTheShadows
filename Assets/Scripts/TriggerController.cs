using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerController : MonoBehaviour
{

	public ObjectController theObject;
	public GameObject desiredCollider;
	private IEnumerator _validCoroutine;

	private void OnTriggerEnter(Collider other)
	{
		if (GameObject.ReferenceEquals(desiredCollider, other.gameObject))
		{
			_validCoroutine = ValidateShadow();
			StartCoroutine(_validCoroutine);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (GameObject.ReferenceEquals(desiredCollider, other.gameObject))
		{
			StopCoroutine(_validCoroutine);
			theObject.colliderExited();
		}
	}

	private IEnumerator ValidateShadow()
	{
		yield return new WaitForSeconds(1.5f);
		theObject.colliderHit();
	}
}
