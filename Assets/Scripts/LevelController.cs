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

	public void SetLevelActive(bool active)
	{
		if (active)
			currentLevel.SetActive(active);
		else
			StartCoroutine(HideObjectAndScreen());
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
