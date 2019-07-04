using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
	public bool unlockedLevel1;
	public bool unlockedLevel2;
	public bool unlockedLevel3;
	public bool unlockedLevel4;
	public bool unlockedLevel5;
	public float[] rotTeapot;
	public float[] rotElephant;
	public float[] rotGlobe;
	public float[] rotStand;
	public float[] posFour;
	public float[] rotFour;
	public float[] posTwo;
	public float[] rotTwo;

	public GameData(GameState gameState)
	{
		unlockedLevel1 = gameState.unlockedLevel1;
		unlockedLevel2 = gameState.unlockedLevel2;
		unlockedLevel3 = gameState.unlockedLevel3;
		unlockedLevel4 = gameState.unlockedLevel4;
		unlockedLevel5 = gameState.unlockedLevel5;
		rotTeapot = new float[3];
		rotTeapot[0] = gameState.teapot.transform.localEulerAngles.x;
		rotTeapot[1] = gameState.teapot.transform.localEulerAngles.y;
		rotTeapot[2] = gameState.teapot.transform.localEulerAngles.z;
		rotElephant = new float[3];
		rotElephant[0] = gameState.elephant.transform.localEulerAngles.x;
		rotElephant[1] = gameState.elephant.transform.localEulerAngles.y;
		rotElephant[2] = gameState.elephant.transform.localEulerAngles.z;
		rotGlobe = new float[3];
		rotGlobe[0] = gameState.globe.transform.localEulerAngles.x;
		rotGlobe[1] = gameState.globe.transform.localEulerAngles.y;
		rotGlobe[2] = gameState.globe.transform.localEulerAngles.z;
		rotStand = new float[3];
		rotStand[0] = gameState.stand.transform.localEulerAngles.x;
		rotStand[1] = gameState.stand.transform.localEulerAngles.y;
		rotStand[2] = gameState.stand.transform.localEulerAngles.z;
		rotFour = new float[3];
		rotFour[0] = gameState.fourPiece.transform.localEulerAngles.x;
		rotFour[1] = gameState.fourPiece.transform.localEulerAngles.y;
		rotFour[2] = gameState.fourPiece.transform.localEulerAngles.z;
		posFour = new float[3];
		posFour[0] = gameState.fourContainer.transform.position.x;
		posFour[1] = gameState.fourContainer.transform.position.y;
		posFour[2] = gameState.fourContainer.transform.position.z;
		rotTwo = new float[3];
		rotTwo[0] = gameState.twoPiece.transform.localEulerAngles.x;
		rotTwo[1] = gameState.twoPiece.transform.localEulerAngles.y;
		rotTwo[2] = gameState.twoPiece.transform.localEulerAngles.z;
		posTwo = new float[3];
		posTwo[0] = gameState.twoContainer.transform.position.x;
		posTwo[1] = gameState.twoContainer.transform.position.y;
		posTwo[2] = gameState.twoContainer.transform.position.z;
	}
}
