using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
	[HideInInspector] public bool unlockedLevel1 = true;
	[HideInInspector] public bool unlockedLevel2 = false;
	[HideInInspector] public bool unlockedLevel3 = false;
	[HideInInspector] public bool unlockedLevel4 = false;
	[HideInInspector] public bool unlockedLevel5 = false;
	[HideInInspector] public Quaternion rotStartElephant;
	[HideInInspector] public Quaternion rotStartFour;
	[HideInInspector] public Quaternion rotStartGlobe;
	[HideInInspector] public Quaternion rotStartStand;
	[HideInInspector] public Quaternion rotStartTeapot;
	[HideInInspector] public Quaternion rotStartTwo;
	[HideInInspector] public Vector3 posStartFour;
	[HideInInspector] public Vector3 posStartTwo;
	public GameObject elephant;
	public GameObject fourContainer;
	public GameObject fourPiece;
	public GameObject globe;
	public GameObject stand;
	public GameObject teapot;
	public GameObject twoContainer;
	public GameObject twoPiece;

	private void Start()
	{
		SetPositionsAndRotations();
	}

	public void SetPositionsAndRotations()
	{
		posStartFour = fourContainer.transform.position;
		posStartTwo = twoContainer.transform.position;
		rotStartElephant = elephant.transform.rotation;
		rotStartFour = fourPiece.transform.rotation;
		rotStartGlobe = globe.transform.rotation;
		rotStartStand = stand.transform.rotation;
		rotStartTeapot = teapot.transform.rotation;
		rotStartTwo = twoPiece.transform.rotation;
	}

	public void NormalModeSetup()
	{
		teapot.transform.rotation = rotStartTeapot;
		elephant.transform.rotation = rotStartElephant;
		globe.transform.rotation = rotStartGlobe;
		stand.transform.rotation = rotStartStand;
		fourPiece.transform.rotation = rotStartFour;
		fourContainer.transform.position = posStartFour;
		twoContainer.transform.position = posStartTwo;
		twoPiece.transform.rotation = rotStartTwo;
	}

	public void SaveGame()
	{
		SaveSystem.SaveGame(this);
	}

	public bool LoadGame()
	{
		GameData data = SaveSystem.LoadGame();
		if (data == null)
			return false;
		unlockedLevel1 = true;
		unlockedLevel2 = data.unlockedLevel2;
		unlockedLevel3 = data.unlockedLevel3;
		unlockedLevel4 = data.unlockedLevel4;
		unlockedLevel5 = data.unlockedLevel5;
		teapot.transform.localEulerAngles = new Vector3(data.rotTeapot[0], data.rotTeapot[1], data.rotTeapot[2]);
		elephant.transform.localEulerAngles = new Vector3(data.rotElephant[0], data.rotElephant[1], data.rotElephant[2]);
		globe.transform.localEulerAngles = new Vector3(data.rotGlobe[0], data.rotGlobe[1], data.rotGlobe[2]);
		stand.transform.localEulerAngles = new Vector3(data.rotStand[0], data.rotStand[1], data.rotStand[2]);
		fourPiece.transform.localEulerAngles = new Vector3(data.rotFour[0], data.rotFour[1], data.rotFour[2]);
		fourContainer.transform.position = new Vector3(data.posFour[0], data.posFour[1], data.posFour[2]);
		twoPiece.transform.localEulerAngles = new Vector3(data.rotTwo[0], data.rotTwo[1], data.rotTwo[2]);
		twoContainer.transform.position = new Vector3(data.posTwo[0], data.posTwo[1], data.posTwo[2]);
		SetPositionsAndRotations();
		return true;
	}
}
