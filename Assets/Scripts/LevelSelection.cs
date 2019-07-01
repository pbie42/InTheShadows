﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelection : MonoBehaviour
{
	private bool _testMode = false;
	private Dictionary<string, GameObject> _levels = new Dictionary<string, GameObject>();
	private Dictionary<string, bool> _unlockedLevels = new Dictionary<string, bool>();
	private Dictionary<string, GameObject> _levelsBottles = new Dictionary<string, GameObject>();
	private Dictionary<string, GameObject> _finishedLevels = new Dictionary<string, GameObject>();
	private Dictionary<string, string> _clues = new Dictionary<string, string>();
	private Dictionary<string, UnityEngine.Light> _spotLights = new Dictionary<string, UnityEngine.Light>();
	private Dictionary<string, UnityEngine.Light> _topLights = new Dictionary<string, UnityEngine.Light>();
	private float _fadeInSpeed = 2f;
	private string _howdyText = "Howdy, Partner !\nPick your poison";
	public BGMusicSelector bgMusic;
	public bool canSelect = false;
	public CameraController mainCamera;
	public GameObject adiosButton;
	public GameObject giddyUpButton;
	public GameObject[] levelsArray;
	public GameObject[] levelsBottlesArray;
	public GameObject[] finishedLevelsArray;
	public GuiController guiController;
	public LevelController levelController;
	public UnityEngine.Light[] spotLightsArray;
	public UnityEngine.Light[] topLightsArray;
	public UnityEngine.UI.Text adiosText;
	public UnityEngine.UI.Text clueText;
	public UnityEngine.UI.Text giddyupText;
	public UnityEngine.UI.Text normalText;
	public UnityEngine.UI.Text testText;

	// Use this for initialization
	void Start()
	{
		SetupLevelSelection();
	}

	// Update is called once per frame
	void Update()
	{
		if (canSelect && Input.GetMouseButtonDown(0))
			LocatePosition();
	}

	private void LocatePosition()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 1000))
		{
			string name = hit.collider.gameObject.name;
			if (name == "Level 1" && (_unlockedLevels["Level 1"] || _testMode))
				SelectLevel("Level 1");
			else if (name == "Level 2" && (_unlockedLevels["Level 2"] || _testMode))
				SelectLevel("Level 2");
			else if (name == "Level 3" && (_unlockedLevels["Level 3"] || _testMode))
				SelectLevel("Level 3");
			else if (name == "Level 4" && (_unlockedLevels["Level 4"] || _testMode))
				SelectLevel("Level 4");
		}
	}

	private void SelectLevel(string level)
	{
		clueText.text = _clues[level];
		levelController.currentLevel = _levels[level];
		StartCoroutine(guiController.FadeAndDisplayButton(giddyupText, giddyUpButton, 0f));
		StartCoroutine(guiController.FadeTextToFullAlpha(_fadeInSpeed, clueText));
		guiController.StopRoutines();
		TurnOffOthers(level);
		guiController.fadeLights(_topLights[level], _spotLights[level], true);
	}

	private void TurnOffOthers(string level)
	{
		if (level != "Level 1")
		{
			_topLights["Level 1"].intensity = 0;
			_spotLights["Level 1"].intensity = 0;
		}
		if (level != "Level 2")
		{
			_topLights["Level 2"].intensity = 0;
			_spotLights["Level 2"].intensity = 0;
		}
		if (level != "Level 3")
		{
			_topLights["Level 3"].intensity = 0;
			_spotLights["Level 3"].intensity = 0;
		}
		if (level != "Level 4")
		{
			_topLights["Level 4"].intensity = 0;
			_spotLights["Level 4"].intensity = 0;
		}
	}

	private void TurnOffAll()
	{
		TurnOffOthers("Level 1");
		TurnOffOthers("Level 2");
		TurnOffOthers("Level 3");
		TurnOffOthers("Level 4");
	}

	public void NormalGame()
	{
		_testMode = false;
		StartCoroutine(guiController.FadeTextToFullAlpha(1.5f, clueText));
		StartCoroutine(guiController.FadeAndDisplayButton(adiosText, adiosButton, 2f));
		mainCamera.currentView = 1;
	}

	public void TestGame()
	{
		_testMode = true;
		StartCoroutine(guiController.FadeTextToFullAlpha(1.5f, clueText));
		StartCoroutine(guiController.FadeAndDisplayButton(adiosText, adiosButton, 2f));
		mainCamera.currentView = 1;
	}

	public void BackToLevelSection()
	{
		levelController.HideWoahText();
		levelController.HideWinText();
		StartCoroutine(guiController.FadeAndDisplayButton(adiosText, adiosButton, 1f));
		StartCoroutine(guiController.FadeAndDisplayButton(giddyupText, giddyUpButton, 1f));
		if (!_testMode && (_unlockedLevels["Level 2"] || _unlockedLevels["Level 3"] || _unlockedLevels["Level 4"]))
			FinishedLevelAnimation();
		mainCamera.currentView = 1;
	}

	public void GoToLevel()
	{
		mainCamera.currentView = 2;
		levelController.ShowWoahText("← Woah There");
		StartCoroutine(guiController.FadeAndHideButton(giddyupText, giddyUpButton));
		StartCoroutine(guiController.FadeAndHideButton(adiosText, adiosButton));
	}

	public void BackToStart()
	{
		StartCoroutine(guiController.FadeAndHideButton(giddyupText, giddyUpButton));
		StartCoroutine(guiController.FadeAndHideButton(adiosText, adiosButton));
		StartCoroutine(guiController.FadeTextToZeroAlpha(1.5f, clueText));
		TurnOffAll();
		clueText.text = _howdyText;
		mainCamera.currentView = 0;
	}

	public void PuzzleSolved(string levelFinished)
	{
		if (levelFinished == "Teapot")
			_unlockedLevels["Level 2"] = true;
		if (levelFinished == "Elephant")
			_unlockedLevels["Level 3"] = true;
		if (levelFinished == "Globe")
			_unlockedLevels["Level 4"] = true;
	}

	private void FinishedLevelAnimation()
	{
		if (_unlockedLevels["Level 2"] && !_finishedLevels["Level 1"].activeSelf)
			StartCoroutine(FinishedAnimation("Level 1"));
		if (_unlockedLevels["Level 3"] && !_finishedLevels["Level 2"].activeSelf)
			StartCoroutine(FinishedAnimation("Level 2"));
		if (_unlockedLevels["Level 4"] && !_finishedLevels["Level 3"].activeSelf)
			StartCoroutine(FinishedAnimation("Level 3"));
	}

	IEnumerator FinishedAnimation(string level)
	{
		yield return new WaitForSeconds(1.5f);
		bgMusic.PlayGunAndBottle();
		yield return new WaitForSeconds(0.5f);
		_levelsBottles[level].GetComponent<Renderer>().enabled = false;
		_finishedLevels[level].SetActive(true);
		SelectNextLevel(level);
	}

	private void SelectNextLevel(string finishedLevel)
	{
		if (finishedLevel == "Level 1")
			SelectLevel("Level 2");
		else if (finishedLevel == "Level 2")
			SelectLevel("Level 3");
		else if (finishedLevel == "Level 3")
			SelectLevel("Level 4");
	}

	private void SetupLevelSelection()
	{
		for (int i = 0; i < levelsArray.Length; i++)
		{
			_levels.Add("Level " + (i + 1), levelsArray[i]);
			_unlockedLevels.Add("Level " + (i + 1), false);
		}
		_unlockedLevels["Level 1"] = true;
		for (int i = 0; i < spotLightsArray.Length; i++)
			_spotLights.Add("Level " + (i + 1), spotLightsArray[i]);
		for (int i = 0; i < topLightsArray.Length; i++)
			_topLights.Add("Level " + (i + 1), topLightsArray[i]);
		for (int i = 0; i < finishedLevelsArray.Length; i++)
			_finishedLevels.Add("Level " + (i + 1), finishedLevelsArray[i]);
		for (int i = 0; i < levelsBottlesArray.Length; i++)
			_levelsBottles.Add("Level " + (i + 1), levelsBottlesArray[i]);
		_clues.Add("Level 1", "Ain't nobody dope as me I'm just so short and stout");
		_clues.Add("Level 2", "Always Remembers, \nNever Forgets");
		_clues.Add("Level 3", "Give me a spin and I'll take you anywhere");
		_clues.Add("Level 4", "The answer to life, the universe, and everything");
		clueText.text = _howdyText;
		StartCoroutine(guiController.FadeTextToFullAlpha(_fadeInSpeed, clueText));
		StartCoroutine(guiController.ButtonFadeIns(normalText, testText));
	}
}


