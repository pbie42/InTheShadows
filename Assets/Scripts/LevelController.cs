using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
	public GameObject currentLevel;
	public UnityEngine.UI.Text winText;
	public UnityEngine.UI.Text woahText;
	public UnityEngine.UI.Text adiosText;
	public UnityEngine.UI.Text controlsText;
	public GameObject woahButton;
	public GameObject adiosButton;
	public GuiController guiController;
	[HideInInspector] public bool isFocused = false;

	public void SetLevelActive(bool active, float time)
	{
		if (active)
		{
			currentLevel.SetActive(active);
			isFocused = true;
		}
		else
		{
			StartCoroutine(HideObjectAndScreen(time));
			isFocused = false;
		}
	}

	private IEnumerator HideObjectAndScreen(float time)
	{
		yield return new WaitForSeconds(time);
		currentLevel.SetActive(false);
	}

	public void PuzzleSolved(string objectName)
	{
		winText.text = "You solved the " + objectName + "!";
		woahText.text = "← Next Level";
		StartCoroutine(guiController.FadeTextToFullAlpha(1f, winText));
		ShowAdiosButton();
	}

	public bool IsWinTextActive()
	{
		if (winText.color.a <= 0)
			return false;
		return true;
	}

	public void ShowAdiosButton()
	{
		StartCoroutine(guiController.FadeAndDisplayButton(adiosText, adiosButton, 1f));
	}

	public void HideAdiosButton()
	{
		StartCoroutine(guiController.FadeAndHideButton(adiosText, adiosButton));
	}

	public void ShowWoahText(string text)
	{
		woahText.text = text;
		StartCoroutine(guiController.FadeAndDisplayButton(woahText, woahButton, 1f));
	}

	public void HideWoahText()
	{
		StartCoroutine(guiController.FadeAndHideButton(woahText, woahButton));
	}

	public void HideWinText()
	{
		StartCoroutine(guiController.FadeTextToZeroAlpha(1f, winText));
	}

	public void ShowWinText()
	{
		StartCoroutine(guiController.FadeTextToFullAlpha(1f, winText));
	}

	public void HideControlsText()
	{
		StartCoroutine(guiController.FadeTextToZeroAlpha(1f, controlsText));
	}

	public void ShowControlsText()
	{
		StartCoroutine(guiController.FadeTextToFullAlpha(1f, controlsText));
	}


}
