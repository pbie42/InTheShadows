using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
	public bool canHorizontal;
	public bool canVertical;
	public bool canMove;
	public int colliderPoints;
	private int _collidersHit = 0;
	Vector3 _mPrevPos = Vector3.zero;
	Vector3 _mPosDelta = Vector3.zero;
	Vector3 _parentUp;
	Vector3 _parentRight;

	private void Start()
	{
		_parentUp = transform.parent.transform.up;
		_parentRight = transform.parent.transform.right;
	}

	// Update is called once per frame
	void Update()
	{
		if (_collidersHit == colliderPoints)
			Debug.Log("YOU WON!!!");
		if (canVertical && Input.GetKey(KeyCode.LeftControl) && Input.GetMouseButton(0))
		{
			_mPosDelta = Input.mousePosition - _mPrevPos;
			transform.Rotate(_parentRight, Vector3.Dot(_mPosDelta, _parentUp), Space.World);
		}
		else if (canHorizontal && !Input.GetKey(KeyCode.LeftControl) && Input.GetMouseButton(0))
		{
			_mPosDelta = Input.mousePosition - _mPrevPos;
			transform.Rotate(_parentUp, -Vector3.Dot(_mPosDelta, _parentRight), Space.World);
		}

		_mPrevPos = Input.mousePosition;
	}

	public void colliderHit()
	{
		_collidersHit++;
	}

	public void colliderExited()
	{
		if (_collidersHit > 0)
			_collidersHit--;
	}
}
