using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
	private bool _solved = false;
	private int _collidersHit = 0;
	public bool canHorizontal;
	public bool canMove;
	public bool canVertical;
	public int colliderPoints;
	public LevelController levelController;
	public LevelSelection levelSelection;
	Vector3 _mPosDelta = Vector3.zero;
	Vector3 _mPrevPos = Vector3.zero;
	Vector3 _parentRight;
	Vector3 _parentUp;

	private void Start()
	{
		_parentUp = transform.parent.transform.up;
		_parentRight = transform.parent.transform.right;
	}

	// Update is called once per frame
	void Update()
	{
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
		if (_solved && !levelController.IsWinTextActive() && levelController.isFocused)
			levelController.PuzzleSolved(gameObject.name);

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
