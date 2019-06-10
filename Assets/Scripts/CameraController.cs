using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public Transform[] views;
	public GameObject cartLights;
	public GameObject objectAndScreen;
	public float transitionSpeed;
	private Transform _currentView;
	private int _viewIndex = 0;

	// Use this for initialization
	void Start()
	{

	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			cartLights.SetActive(false);
			_currentView = views[0];
			_viewIndex = 0;
		}
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			objectAndScreen.SetActive(false);
			cartLights.SetActive(true);
			if (_viewIndex == 2)
				_currentView = views[2];
			else
				_currentView = views[1];
			_viewIndex = 1;
		}
		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			objectAndScreen.SetActive(true);
			cartLights.SetActive(false);
			transform.rotation = views[2].rotation;
			_currentView = views[3];
			_viewIndex = 2;
		}
		if (Input.GetKeyDown(KeyCode.Alpha4))
		{
			cartLights.SetActive(false);
			_currentView = views[4];
			_viewIndex = 3;
		}
	}

	// Update is called once per frame
	void LateUpdate()
	{
		UpdatePosition();
	}

	private void UpdatePosition()
	{
		transform.position = Vector3.Lerp(transform.position, _currentView.position, Time.deltaTime * transitionSpeed);

		Vector3 currentAngle = new Vector3(
			Mathf.Lerp(transform.rotation.eulerAngles.x, _currentView.transform.eulerAngles.x, Time.deltaTime * transitionSpeed),
			Mathf.Lerp(transform.rotation.eulerAngles.y, _currentView.transform.eulerAngles.y, Time.deltaTime * transitionSpeed),
			Mathf.Lerp(transform.rotation.eulerAngles.z, _currentView.transform.eulerAngles.z, Time.deltaTime * transitionSpeed)
		);

		transform.eulerAngles = currentAngle;
	}
}
