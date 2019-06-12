using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelection : MonoBehaviour
{
	[HideInInspector] public bool unlockedLevel1 = true;
	[HideInInspector] public bool unlockedLevel2 = false;
	[HideInInspector] public bool unlockedLevel3 = false;
	[HideInInspector] public bool unlockedLevel4 = false;
	private Dictionary<string, string> clues = new Dictionary<string, string>();
	private Dictionary<string, GameObject> levels = new Dictionary<string, GameObject>();
	private Dictionary<string, UnityEngine.Light> spotLights = new Dictionary<string, UnityEngine.Light>();
	private Dictionary<string, UnityEngine.Light> topLights = new Dictionary<string, UnityEngine.Light>();
	private float _fadeInSpeed = 2f;
	private float _fadeOutSpeed = 0.5f;
	private float _spotLightIntensity = 22.83f;
	private float _topLightIntensity = 87.86f;
	private IEnumerator _spotCoroutine;
	private IEnumerator _topCoroutine;
	private string _howdyText = "Howdy, Partner !\nPick your poison";
	public bool canSelect = false;
	public CameraController camera;
	public GameObject level1;
	public GameObject level2;
	public GameObject giddyUpButton;
	public GameObject adiosButton;
	public LevelController levelController;
	public UnityEngine.Light level1SpotLight;
	public UnityEngine.Light level1TopLight;
	public UnityEngine.Light level2SpotLight;
	public UnityEngine.Light level2TopLight;
	public UnityEngine.Light level3SpotLight;
	public UnityEngine.Light level3TopLight;
	public UnityEngine.Light level4SpotLight;
	public UnityEngine.Light level4TopLight;
	public UnityEngine.UI.Text clueText;
	public UnityEngine.UI.Text giddyupText;
	public UnityEngine.UI.Text adiosText;
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
		StartCoroutine(FadeAndDisplayButton(giddyupText, giddyUpButton, 0f));
		StartCoroutine(FadeTextToFullAlpha(_fadeInSpeed, clueText));
		StopRoutines();
		TurnOffOthers(level);
		fadeLights(topLights[level], spotLights[level], true);
	}

	private void TurnOffOthers(string level)
	{
		Debug.Log("level: " + level);
		if (level != "Level 1")
		{
			level1TopLight.intensity = 0;
			level1SpotLight.intensity = 0;
		}
		if (level != "Level 2")
		{
			level2TopLight.intensity = 0;
			level2SpotLight.intensity = 0;
		}
		if (level != "Level")
		{
			level3TopLight.intensity = 0;
			level3SpotLight.intensity = 0;
		}
		if (level != "Level 4")
		{
			level4TopLight.intensity = 0;
			level4SpotLight.intensity = 0;
		}
	}

	private void TurnOffAll()
	{
		TurnOffOthers("Level 1");
		TurnOffOthers("Level 2");
		TurnOffOthers("Level 3");
		TurnOffOthers("Level 4");
	}

	private void fadeLights(Light topLight, Light spotLight, bool fadeIn)
	{
		float fadeSpeed = fadeIn ? _fadeInSpeed : _fadeOutSpeed;
		_topCoroutine = fadeInAndOut(topLight, fadeIn, fadeSpeed, _topLightIntensity);
		_spotCoroutine = fadeInAndOut(spotLight, fadeIn, fadeSpeed, _spotLightIntensity);
		StartCoroutine(_topCoroutine);
		StartCoroutine(_spotCoroutine);
	}

	private void StopRoutines()
	{
		if (_topCoroutine != null)
			StopCoroutine(_topCoroutine);
		if (_spotCoroutine != null)
			StopCoroutine(_spotCoroutine);
	}

	private IEnumerator fadeInAndOut(Light lightToFade, bool fadeIn, float duration, float maxIntensity)
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

	private IEnumerator FadeTextToFullAlpha(float t, UnityEngine.UI.Text i)
	{
		i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
		while (i.color.a < 1.0f)
		{
			i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
			yield return null;
		}
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

	private void SetupLevelSelection()
	{
		levels.Add("Level 1", level1);
		levels.Add("Level 2", level2);
		spotLights.Add("Level 1", level1SpotLight);
		spotLights.Add("Level 2", level2SpotLight);
		spotLights.Add("Level 3", level3SpotLight);
		spotLights.Add("Level 4", level4SpotLight);
		topLights.Add("Level 1", level1TopLight);
		topLights.Add("Level 2", level2TopLight);
		topLights.Add("Level 3", level3TopLight);
		topLights.Add("Level 4", level4TopLight);
		clues.Add("Level 1", "Ain't nobody dope as me I'm just so short and stout");
		clues.Add("Level 2", "Always Remembers, \nNever Forgets");
		clues.Add("Level 3", "Test 3");
		clues.Add("Level 4", "The answer to life, the universe, and everything");
		clueText.text = _howdyText;
		StartCoroutine(FadeTextToFullAlpha(_fadeInSpeed, clueText));
		StartCoroutine(ButtonFadeIns());
	}

	IEnumerator ButtonFadeIns()
	{
		yield return new WaitForSeconds(2);
		StartCoroutine(FadeTextToFullAlpha(_fadeInSpeed, normalText));
		StartCoroutine(FadeTextToFullAlpha(_fadeInSpeed, testText));
	}

	public void NormalGame()
	{
		RelockLevels();
		StartCoroutine(FadeTextToFullAlpha(1.5f, clueText));
		StartCoroutine(FadeAndDisplayButton(adiosText, adiosButton, 2f));
		camera.currentView = 1;
	}

	public void BackToLevelSection()
	{
		levelController.HideWoahText();
		StartCoroutine(FadeAndDisplayButton(adiosText, adiosButton, 1f));
		StartCoroutine(FadeAndDisplayButton(giddyupText, giddyUpButton, 1f));
		camera.currentView = 1;
	}

	public void TestGame()
	{
		unlockedLevel1 = true;
		unlockedLevel2 = true;
		unlockedLevel3 = true;
		unlockedLevel4 = true;
		StartCoroutine(FadeTextToFullAlpha(1.5f, clueText));
		StartCoroutine(FadeAndDisplayButton(adiosText, adiosButton, 2f));
		camera.currentView = 1;
	}

	public void GoToLevel()
	{
		camera.currentView = 2;
		levelController.ShowWoahText("← Woah There");
		StartCoroutine(FadeAndHideButton(giddyupText, giddyUpButton));
		StartCoroutine(FadeAndHideButton(adiosText, adiosButton));
	}

	private IEnumerator FadeAndHideButton(UnityEngine.UI.Text text, GameObject button)
	{
		button.GetComponent<UnityEngine.UI.Button>().interactable = false;
		StartCoroutine(FadeTextToZeroAlpha(1.5f, text));
		yield return new WaitForSeconds(1.5f);
		button.SetActive(false);
	}

	private IEnumerator FadeAndDisplayButton(UnityEngine.UI.Text text, GameObject button, float wait)
	{
		button.SetActive(true);
		button.GetComponent<UnityEngine.UI.Button>().interactable = true;
		yield return new WaitForSeconds(wait);
		StartCoroutine(FadeTextToFullAlpha(1.5f, text));
	}

	public void BackToStart()
	{
		RelockLevels();
		StartCoroutine(FadeAndHideButton(giddyupText, giddyUpButton));
		StartCoroutine(FadeAndHideButton(adiosText, adiosButton));
		StartCoroutine(FadeTextToZeroAlpha(1.5f, clueText));
		TurnOffAll();
		clueText.text = _howdyText;
		camera.currentView = 0;
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
		if (levelFinished == "Level 4")
			unlockedLevel4 = true;
	}
}


