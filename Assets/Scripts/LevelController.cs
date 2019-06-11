using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
	public GameObject currentLevel;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

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
}
