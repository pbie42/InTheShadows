using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
	public bool canHorizontal;
	public bool canVertical;
	public bool canMove;
	private bool _solved = false;
	public int colliderPoints;
	public LevelSelection levelSelection;
	public LevelController levelController;
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
		Debug.Log("gameObject.name: " + gameObject.name);
		if (_collidersHit == colliderPoints && !_solved)
		{
			_solved = true;
			levelSelection.PuzzleSolved(gameObject.name);
			levelController.PuzzleSolved(gameObject.name);
		}
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
