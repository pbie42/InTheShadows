using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSpotlight : MonoBehaviour
{
	private float _rotX = 0.0f;
	private float _rotY = 0.0f;
	private float _rotZ = 0.0f;
	private bool _increaseX = true;
	private bool _increaseY = true;

	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		Debug.Log("transform.eulerAngles.x: " + transform.localEulerAngles.x);
		if (_rotX <= -7.0f)
			_increaseX = true;
		else if (_rotX >= 8.0f)
			_increaseX = false;
		if (_rotY <= -8.0f)
			_increaseY = true;
		else if (_rotY >= 8.0f)
			_increaseY = false;

		if (_increaseX)
			_rotX += 0.1f;
		else
			_rotX -= 0.1f;
		if (_increaseY)
			_rotY += 0.1f;
		else
			_rotY -= 0.1f;
		transform.Rotate(_rotX * Time.deltaTime, 0, 0);
		transform.Rotate(0, _rotY * Time.deltaTime, 0);
	}
}
