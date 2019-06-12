using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	private float _fadeSpeed = 3f;
	private int _viewIndex = 0;
	[HideInInspector] public int currentView = 0;
	private Transform _currentView;
	public float transitionSpeed;
	public GameObject levelMenu;
	public GameObject mainMenu;
	public GameObject objectAndScreen;
	public LevelController levelController;
	public LevelSelection levelSelector;
	public Transform[] views;
	public UnityEngine.Light cartLights;

	// Use this for initialization
	void Start()
	{
		_currentView = transform;
	}

	void Update()
	{
		if (_viewIndex != 0 && (Input.GetKeyDown(KeyCode.Alpha1) || currentView == 0))
			Location1();
		if (_viewIndex != 1 && (Input.GetKeyDown(KeyCode.Alpha2) || currentView == 1))
			Location2();
		if (_viewIndex != 2 && (Input.GetKeyDown(KeyCode.Alpha3) || currentView == 2))
			Location3();
		if (_viewIndex != 3 && Input.GetKeyDown(KeyCode.Alpha4))
			Location4();
	}

	private void Location1()
	{
		levelSelector.canSelect = false;
		if (_viewIndex != 0)
			StartCoroutine(fadeInAndOut(cartLights, false, _fadeSpeed));
		if (_viewIndex == 1)
			transform.rotation = views[1].rotation;
		mainMenu.SetActive(true);
		_currentView = views[0];
		_viewIndex = 0;
	}

	private void Location2()
	{
		levelSelector.canSelect = true;
		StartCoroutine(HideMainMenu());
		levelController.SetLevelActive(false);
		StartCoroutine(fadeInAndOut(cartLights, true, _fadeSpeed));
		if (_viewIndex == 2)
			_currentView = views[2];
		else
			_currentView = views[1];
		_viewIndex = 1;
	}

	private void Location3()
	{
		levelSelector.canSelect = false;
		levelController.SetLevelActive(true);
		StartCoroutine(fadeInAndOut(cartLights, false, _fadeSpeed));
		transform.rotation = views[2].rotation;
		_currentView = views[3];
		_viewIndex = 2;
	}

	private void Location4()
	{
		levelSelector.canSelect = false;
		_currentView = views[4];
		_viewIndex = 3;
	}

	IEnumerator fadeInAndOut(Light lightToFade, bool fadeIn, float duration)
	{
		float minLuminosity = 0; // min intensity
		float maxLuminosity = 2.22f; // max intensity

		float counter = 0f;

		//Set Values depending on if fadeIn or fadeOut
		float a, b;

		if (fadeIn)
		{
			a = minLuminosity;
			b = maxLuminosity;
		}
		else
		{
			a = maxLuminosity;
			b = minLuminosity;
		}

		float currentIntensity = lightToFade.intensity;

		while (counter < duration)
		{
			counter += Time.deltaTime;

			lightToFade.intensity = Mathf.Lerp(a, b, counter / duration);

			yield return null;
		}
	}

	private IEnumerator HideMainMenu()
	{
		yield return new WaitForSeconds(2);
		mainMenu.SetActive(false);
	}

	private IEnumerator HideObjectAndScreen()
	{
		yield return new WaitForSeconds(2);
		objectAndScreen.SetActive(false);
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
