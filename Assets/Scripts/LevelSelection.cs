using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelection : MonoBehaviour
{
	[HideInInspector] public bool testMode = false;
	private Dictionary<string, GameObject> _levels = new Dictionary<string, GameObject>();
	private Dictionary<string, bool> _unlockedLevels = new Dictionary<string, bool>();
	private Dictionary<string, GameObject> _levelsBottles = new Dictionary<string, GameObject>();
	private Dictionary<string, GameObject> _finishedLevels = new Dictionary<string, GameObject>();
	private Dictionary<string, string> _clues = new Dictionary<string, string>();
	private Dictionary<string, UnityEngine.Light> _spotLights = new Dictionary<string, UnityEngine.Light>();
	private Dictionary<string, UnityEngine.Light> _topLights = new Dictionary<string, UnityEngine.Light>();
	private float _fadeInSpeed = 2f;
	private float _gunShotSpeed = 1.5f;
	private float _bottleBreakSpeed = 0.3f;
	private string _howdyText = "Howdy, Partner !\nPick your poison";
	public BGMusicSelector bgMusic;
	public bool canSelect = false;
	public CameraController mainCamera;
	public GameObject adiosButton;
	public GameObject giddyUpButton;
	public GameObject[] levelsArray;
	public GameObject[] levelsBottlesArray;
	public GameObject[] finishedLevelsArray;
	public GameState gameState;
	public GuiController guiController;
	public LevelController levelController;
	public UnityEngine.Light[] spotLightsArray;
	public UnityEngine.Light[] topLightsArray;
	public UnityEngine.UI.Text adiosText;
	public UnityEngine.UI.Text clueText;
	public UnityEngine.UI.Text giddyupText;
	public UnityEngine.UI.Text normalText;
	public UnityEngine.UI.Text testText;
	public UnityEngine.UI.Text quitText;

	// Use this for initialization
	void Start()
	{
		SetupLevelSelection();
		// SaveSystem.DeleteSaves();
		bool loaded = gameState.LoadGame();
		if (loaded)
		{
			Debug.Log("Loaded!");
			HandleGameLoad();
		}
		else
			Debug.Log("Not Loaded!");

	}

	// Update is called once per frame
	void Update()
	{
		if (canSelect && Input.GetMouseButtonDown(0))
			LocatePosition();
	}

	private void OnApplicationQuit()
	{
		gameState.SaveGame();
	}

	private void LocatePosition()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 1000))
		{
			string name = hit.collider.gameObject.name;
			if (name == "Level 1" && (_unlockedLevels["Level 1"] || testMode))
				SelectLevel("Level 1");
			else if (name == "Level 2" && (_unlockedLevels["Level 2"] || testMode))
				SelectLevel("Level 2");
			else if (name == "Level 3" && (_unlockedLevels["Level 3"] || testMode))
				SelectLevel("Level 3");
			else if (name == "Level 4" && (_unlockedLevels["Level 4"] || testMode))
				SelectLevel("Level 4");
		}
	}

	private void SelectLevel(string level)
	{
		clueText.text = _clues[level];
		levelController.currentLevel = null;
		levelController.currentLevel = _levels[level];
		if (!giddyUpButton.activeSelf)
			StartCoroutine(guiController.FadeAndDisplayButton(giddyupText, giddyUpButton, 0f));
		if (clueText.color.a <= 0 && mainCamera.currentView == 1)
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
			_levels["Level 1"].SetActive(false);
		}
		if (level != "Level 2")
		{
			_topLights["Level 2"].intensity = 0;
			_spotLights["Level 2"].intensity = 0;
			_levels["Level 2"].SetActive(false);
		}
		if (level != "Level 3")
		{
			_topLights["Level 3"].intensity = 0;
			_spotLights["Level 3"].intensity = 0;
			_levels["Level 3"].SetActive(false);
		}
		if (level != "Level 4")
		{
			_topLights["Level 4"].intensity = 0;
			_spotLights["Level 4"].intensity = 0;
			_levels["Level 4"].SetActive(false);
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
		testMode = false;
		gameState.NormalModeSetup();
		StartCoroutine(guiController.FadeTextToFullAlpha(1.5f, clueText));
		StartCoroutine(guiController.FadeAndDisplayButton(adiosText, adiosButton, 2f));
		mainCamera.currentView = 1;
	}

	public void TestGame()
	{
		testMode = true;
		StartCoroutine(guiController.FadeTextToFullAlpha(1.5f, clueText));
		StartCoroutine(guiController.FadeAndDisplayButton(adiosText, adiosButton, 2f));
		mainCamera.currentView = 1;
	}

	public void QuitGame()
	{
		SaveGame();
		Application.Quit();
	}

	public void PrepareLevelSection()
	{
		levelController.HideWoahText();
		if (levelController.winText.color.a >= 1)
			levelController.HideWinText();
		levelController.HideControlsText();
		levelController.HideAdiosButton();
		if (!adiosButton.activeSelf)
			StartCoroutine(guiController.FadeAndDisplayButton(adiosText, adiosButton, 1f));
		if (!giddyUpButton.activeSelf)
			StartCoroutine(guiController.FadeAndDisplayButton(giddyupText, giddyUpButton, 1f));
		if (!testMode && (_unlockedLevels["Level 2"] || _unlockedLevels["Level 3"] || _unlockedLevels["Level 4"]))
			FinishedLevelAnimation(true);
	}

	public void BackToLevelSection()
	{
		PrepareLevelSection();
		mainCamera.currentView = 1;
	}

	public void BackToStartFromLevel()
	{
		PrepareLevelSection();
		BackToStart();
	}

	public void GoToLevel()
	{
		mainCamera.currentView = 2;
		levelController.ShowWoahText("← Woah There");
		levelController.ShowControlsText();
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
		if (!testMode)
		{
			if (levelFinished == "Teapot")
			{
				_unlockedLevels["Level 2"] = true;
				gameState.unlockedLevel2 = true;
			}
			if (levelFinished == "Elephant")
			{
				_unlockedLevels["Level 3"] = true;
				gameState.unlockedLevel3 = true;
			}
			if (levelFinished == "Globe")
			{
				_unlockedLevels["Level 4"] = true;
				gameState.unlockedLevel4 = true;
			}
			if (levelFinished == "42")
			{
				_unlockedLevels["Level 5"] = true;
				gameState.unlockedLevel5 = true;
			}
		}
	}

	private void FinishedLevelAnimation(bool playSound)
	{
		if (_unlockedLevels["Level 2"] && !_finishedLevels["Level 1"].activeSelf)
			StartCoroutine(FinishedAnimation("Level 1", playSound));
		if (_unlockedLevels["Level 3"] && !_finishedLevels["Level 2"].activeSelf)
			StartCoroutine(FinishedAnimation("Level 2", playSound));
		if (_unlockedLevels["Level 4"] && !_finishedLevels["Level 3"].activeSelf)
			StartCoroutine(FinishedAnimation("Level 3", playSound));
		if (_unlockedLevels["Level 5"] && !_finishedLevels["Level 4"].activeSelf)
			StartCoroutine(FinishedAnimation("Level 4", playSound));
	}

	IEnumerator FinishedAnimation(string level, bool playSound)
	{
		yield return new WaitForSeconds(_gunShotSpeed);
		if (playSound)
			bgMusic.PlayGunAndBottle();
		yield return new WaitForSeconds(_bottleBreakSpeed);
		_levelsBottles[level].GetComponent<Renderer>().enabled = false;
		_finishedLevels[level].SetActive(true);
		if (level != "Level 4")
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

	public void SaveGame()
	{
		if (!testMode)
		{
			gameState.SetPositionsAndRotations();
			gameState.SaveGame();
		}
	}

	private void SetupLevelSelection()
	{
		for (int i = 0; i < levelsArray.Length; i++)
		{
			_levels.Add("Level " + (i + 1), levelsArray[i]);
			_unlockedLevels.Add("Level " + (i + 1), false);
		}
		_unlockedLevels.Add("Level 5", false);
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
		StartCoroutine(guiController.ButtonFadeIns(normalText, testText, quitText));
	}

	private void HandleGameLoad()
	{
		_unlockedLevels["Level 1"] = gameState.unlockedLevel1;
		_unlockedLevels["Level 2"] = gameState.unlockedLevel2;
		_unlockedLevels["Level 3"] = gameState.unlockedLevel3;
		_unlockedLevels["Level 4"] = gameState.unlockedLevel4;
		_unlockedLevels["Level 5"] = gameState.unlockedLevel5;
		FinishedLevelAnimation(false);
	}
}


