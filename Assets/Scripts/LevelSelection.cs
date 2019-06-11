using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelection : MonoBehaviour
{
	public UnityEngine.Light level1TopLight;
	public UnityEngine.Light level1SpotLight;
	public UnityEngine.Light level2TopLight;
	public UnityEngine.Light level2SpotLight;
	public UnityEngine.Light level3TopLight;
	public UnityEngine.Light level3SpotLight;
	public UnityEngine.Light level4TopLight;
	public UnityEngine.Light level4SpotLight;
	private float _topLightIntensity = 87.86f;
	private float _spotLightIntensity = 22.83f;
	private float _fadeInSpeed = 2f;
	private float _fadeOutSpeed = 0.5f;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetMouseButton(0))
			LocatePosition();
	}

	private void LocatePosition()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 1000))
		{
			Debug.Log("hit.gameObject: " + hit.collider.gameObject.name);
			string name = hit.collider.gameObject.name;
			Debug.Log("name: " + name);
			if (name == "Level 1")
			{
				fadeLights(level1TopLight, level1SpotLight, true);
				TurnOffOthers(1);
			}
			if (name == "Level 2")
			{
				fadeLights(level2TopLight, level2SpotLight, true);
				TurnOffOthers(2);
			}
			if (name == "Level 3")
			{
				fadeLights(level3TopLight, level3SpotLight, true);
				TurnOffOthers(3);
			}
			if (name == "Level 4")
			{
				fadeLights(level4TopLight, level4SpotLight, true);
				TurnOffOthers(4);
			}
		}
	}

	private void TurnOffOthers(int level)
	{
		if (level != 1)
		{
			level1TopLight.intensity = 0;
			level1SpotLight.intensity = 0;
		}
		if (level != 2)
		{
			level2TopLight.intensity = 0;
			level2SpotLight.intensity = 0;
		}
		if (level != 3)
		{
			level3TopLight.intensity = 0;
			level3SpotLight.intensity = 0;
		}
		if (level != 4)
		{
			level4TopLight.intensity = 0;
			level4SpotLight.intensity = 0;
		}
	}

	private void fadeLights(Light topLight, Light spotLight, bool fadeIn)
	{
		float fadeSpeed = fadeIn ? _fadeInSpeed : _fadeOutSpeed;
		StartCoroutine(fadeInAndOut(topLight, fadeIn, fadeSpeed, _topLightIntensity));
		StartCoroutine(fadeInAndOut(spotLight, fadeIn, fadeSpeed, _spotLightIntensity));
	}

	IEnumerator fadeInAndOut(Light lightToFade, bool fadeIn, float duration, float maxIntensity)
	{
		float minLuminosity = 0; // min intensity
		float maxLuminosity = maxIntensity; // max intensity
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
}


