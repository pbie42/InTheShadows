using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiController : MonoBehaviour
{
	private float _fadeInSpeed = 2f;
	private float _fadeOutSpeed = 0.5f;
	private float _spotLightIntensity = 22.83f;
	private float _topLightIntensity = 87.86f;
	private IEnumerator _spotCoroutine;
	private IEnumerator _topCoroutine;

	public IEnumerator FadeTextToFullAlpha(float t, UnityEngine.UI.Text i)
	{
		i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
		while (i.color.a < 1.0f)
		{
			i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
			yield return null;
		}
	}

	public IEnumerator FadeAndHideButton(UnityEngine.UI.Text text, GameObject button)
	{
		button.GetComponent<UnityEngine.UI.Button>().interactable = false;
		StartCoroutine(FadeTextToZeroAlpha(1.5f, text));
		yield return new WaitForSeconds(1.5f);
		button.SetActive(false);
	}

	public IEnumerator FadeTextToZeroAlpha(float t, UnityEngine.UI.Text i)
	{
		i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
		while (i.color.a > 0.0f)
		{
			i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
			yield return null;
		}
	}

	public IEnumerator FadeAndDisplayButton(UnityEngine.UI.Text text, GameObject button, float wait)
	{
		button.SetActive(true);
		button.GetComponent<UnityEngine.UI.Button>().interactable = true;
		yield return new WaitForSeconds(wait);
		StartCoroutine(FadeTextToFullAlpha(1.5f, text));
	}

	public IEnumerator fadeInAndOut(Light lightToFade, bool fadeIn, float duration, float maxIntensity)
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

	public void fadeLights(Light topLight, Light spotLight, bool fadeIn)
	{
		float fadeSpeed = fadeIn ? _fadeInSpeed : _fadeOutSpeed;
		_topCoroutine = fadeInAndOut(topLight, fadeIn, fadeSpeed, _topLightIntensity);
		_spotCoroutine = fadeInAndOut(spotLight, fadeIn, fadeSpeed, _spotLightIntensity);
		StartCoroutine(_topCoroutine);
		StartCoroutine(_spotCoroutine);
	}

	public void StopRoutines()
	{
		if (_topCoroutine != null)
			StopCoroutine(_topCoroutine);
		if (_spotCoroutine != null)
			StopCoroutine(_spotCoroutine);
	}

	public IEnumerator ButtonFadeIns(UnityEngine.UI.Text button1, UnityEngine.UI.Text button2, UnityEngine.UI.Text button3)
	{
		yield return new WaitForSeconds(2);
		StartCoroutine(FadeTextToFullAlpha(_fadeInSpeed, button1));
		StartCoroutine(FadeTextToFullAlpha(_fadeInSpeed, button2));
		StartCoroutine(FadeTextToFullAlpha(_fadeInSpeed, button3));
	}
}
