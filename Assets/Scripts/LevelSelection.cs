using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelection : MonoBehaviour
{
	[HideInInspector] public bool unlockedLevel1 = true;
	[HideInInspector] public bool unlockedLevel2 = false;
	[HideInInspector] public bool unlockedLevel3 = false;
	[HideInInspector] public bool unlockedLevel4 = false;
	private bool _testMode = false;
	private Dictionary<string, GameObject> levels = new Dictionary<string, GameObject>();
	private Dictionary<string, GameObject> levelsBottles = new Dictionary<string, GameObject>();
	private Dictionary<string, GameObject> finishedLevels = new Dictionary<string, GameObject>();
	private Dictionary<string, string> clues = new Dictionary<string, string>();
	private Dictionary<string, UnityEngine.Light> spotLights = new Dictionary<string, UnityEngine.Light>();
	private Dictionary<string, UnityEngine.Light> topLights = new Dictionary<string, UnityEngine.Light>();
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
			if (name == "Level 1" && unlockedLevel1)
				SelectLevel("Level 1");
			else if (name == "Level 2" && unlockedLevel2)
				SelectLevel("Level 2");
			else if (name == "Level 3" && unlockedLevel3)
				SelectLevel("Level 3");
			else if (name == "Level 4" && unlockedLevel4)
				SelectLevel("Level 4");
		}
	}

	private void SelectLevel(string level)
	{
		clueText.text = clues[level];
		levelController.currentLevel = levels[level];
		StartCoroutine(guiController.FadeAndDisplayButton(giddyupText, giddyUpButton, 0f));
		StartCoroutine(guiController.FadeTextToFullAlpha(_fadeInSpeed, clueText));
		guiController.StopRoutines();
		TurnOffOthers(level);
		guiController.fadeLights(topLights[level], spotLights[level], true);
	}

	private void TurnOffOthers(string level)
	{
		if (level != "Level 1")
		{
			topLights["Level 1"].intensity = 0;
			spotLights["Level 1"].intensity = 0;
		}
		if (level != "Level 2")
		{
			topLights["Level 2"].intensity = 0;
			spotLights["Level 2"].intensity = 0;
		}
		if (level != "Level 3")
		{
			topLights["Level 3"].intensity = 0;
			spotLights["Level 3"].intensity = 0;
		}
		if (level != "Level 4")
		{
			topLights["Level 4"].intensity = 0;
			spotLights["Level 4"].intensity = 0;
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
		RelockLevels();
		StartCoroutine(guiController.FadeTextToFullAlpha(1.5f, clueText));
		StartCoroutine(guiController.FadeAndDisplayButton(adiosText, adiosButton, 2f));
		mainCamera.currentView = 1;
	}

	public void TestGame()
	{
		_testMode = true;
		unlockedLevel1 = true;
		unlockedLevel2 = true;
		unlockedLevel3 = true;
		unlockedLevel4 = true;
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
		if (!_testMode && (unlockedLevel2 || unlockedLevel3 || unlockedLevel4))
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
		RelockLevels();
		StartCoroutine(guiController.FadeAndHideButton(giddyupText, giddyUpButton));
		StartCoroutine(guiController.FadeAndHideButton(adiosText, adiosButton));
		StartCoroutine(guiController.FadeTextToZeroAlpha(1.5f, clueText));
		TurnOffAll();
		clueText.text = _howdyText;
		mainCamera.currentView = 0;
	}

	private void RelockLevels()
	{
		unlockedLevel1 = true;
		unlockedLevel2 = false;
		unlockedLevel3 = false;
		unlockedLevel4 = false;
	}

	public void PuzzleSolved(string levelFinished)
	{
		if (levelFinished == "Teapot")
			unlockedLevel2 = true;
		if (levelFinished == "Elephant")
			unlockedLevel3 = true;
		if (levelFinished == "Globe")
			unlockedLevel4 = true;
	}

	private void FinishedLevelAnimation()
	{
		if (unlockedLevel2 && !finishedLevels["Level 1"].activeSelf)
			StartCoroutine(FinishedAnimation("Level 1"));
		if (unlockedLevel3 && !finishedLevels["Level 2"].activeSelf)
			StartCoroutine(FinishedAnimation("Level 2"));
		if (unlockedLevel4 && !finishedLevels["Level 3"].activeSelf)
			StartCoroutine(FinishedAnimation("Level 3"));
	}

	IEnumerator FinishedAnimation(string level)
	{
		yield return new WaitForSeconds(1.5f);
		bgMusic.PlayGunAndBottle();
		yield return new WaitForSeconds(0.5f);
		levelsBottles[level].GetComponent<Renderer>().enabled = false;
		finishedLevels[level].SetActive(true);
	}

	private void SetupLevelSelection()
	{
		for (int i = 0; i < levelsArray.Length; i++)
			levels.Add("Level " + (i + 1), levelsArray[i]);
		for (int i = 0; i < spotLightsArray.Length; i++)
			spotLights.Add("Level " + (i + 1), spotLightsArray[i]);
		for (int i = 0; i < topLightsArray.Length; i++)
			topLights.Add("Level " + (i + 1), topLightsArray[i]);
		for (int i = 0; i < finishedLevelsArray.Length; i++)
			finishedLevels.Add("Level " + (i + 1), finishedLevelsArray[i]);
		for (int i = 0; i < levelsBottlesArray.Length; i++)
			levelsBottles.Add("Level " + (i + 1), levelsBottlesArray[i]);
		clues.Add("Level 1", "Ain't nobody dope as me I'm just so short and stout");
		clues.Add("Level 2", "Always Remembers, \nNever Forgets");
		clues.Add("Level 3", "Give me a spin and I'll take you anywhere");
		clues.Add("Level 4", "The answer to life, the universe, and everything");
		clueText.text = _howdyText;
		StartCoroutine(guiController.FadeTextToFullAlpha(_fadeInSpeed, clueText));
		StartCoroutine(guiController.ButtonFadeIns(normalText, testText));
	}
}


