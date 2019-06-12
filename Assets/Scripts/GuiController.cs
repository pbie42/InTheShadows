using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiController : MonoBehaviour
{

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
}
