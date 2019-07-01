using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
	public GameObject currentLevel;
	public UnityEngine.UI.Text winText;
	public UnityEngine.UI.Text woahText;
	public GameObject woahButton;
	public GuiController guiController;
	[HideInInspector] public bool isFocused = false;

	public void SetLevelActive(bool active)
	{
		if (active)
		{
			currentLevel.SetActive(active);
			isFocused = true;
		}
		else
		{
			StartCoroutine(HideObjectAndScreen());
			isFocused = false;
		}
	}

	private IEnumerator HideObjectAndScreen()
	{
		yield return new WaitForSeconds(2);
		currentLevel.SetActive(false);
	}

	public void PuzzleSolved(string objectName)
	{
		winText.text = "You solved the " + objectName + "!";
		woahText.text = "← Next Level";
		StartCoroutine(guiController.FadeTextToFullAlpha(1f, winText));
	}

	public bool IsWinTextActive()
	{
		Debug.Log("winText.color.a: " + winText.color.a);
		if (winText.color.a <= 0)
			return false;
		return true;
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


}
