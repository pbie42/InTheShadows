using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsController : MonoBehaviour
{
	public bool canHorizontal;
	public bool canVertical;
	public bool canMove;
	private bool _solved = false;
	public int colliderPoints;
	public LevelSelection levelSelection;
	public LevelController levelController;
	public GameObject piece1;
	public GameObject piece2;
	private GameObject _currentPiece;
	private int _collidersHit = 0;
	Vector3 _mPrevPos = Vector3.zero;
	Vector3 _mPosDelta = Vector3.zero;
	Vector3 _parentUp;
	Vector3 _parentRight;
	private float distance = 4.0f;

	private void Start()
	{
		_parentUp = transform.parent.transform.up;
		_parentRight = transform.parent.transform.right;
		_currentPiece = piece1;
	}

	// Update is called once per frame
	void Update()
	{

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Input.GetMouseButtonDown(0))
		{
			if (Physics.Raycast(ray, out hit, 1000))
			{
				string name = hit.collider.gameObject.name;
				if (GameObject.ReferenceEquals(piece1, hit.collider.gameObject))
					_currentPiece = piece1;
				else if (GameObject.ReferenceEquals(piece2, hit.collider.gameObject))
					_currentPiece = piece2;

			}
		}
		if (_collidersHit == colliderPoints && !_solved)
		{
			_solved = true;
			levelSelection.PuzzleSolved(piece2.name);
			levelController.PuzzleSolved(piece2.name);
		}
		if (canVertical && Input.GetKey(KeyCode.LeftControl) && Input.GetMouseButton(0))
		{
			_mPosDelta = Input.mousePosition - _mPrevPos;
			_currentPiece.transform.Rotate(_parentRight, Vector3.Dot(_mPosDelta, _parentUp), Space.World);
		}
		else if (canMove && !Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift) && Input.GetMouseButton(0))
		{
			Vector3 mousePos = Input.mousePosition;
			float _mYCoord = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, distance)).y;
			Vector3 parentPos = _currentPiece.transform.parent.position;
			if (_mYCoord >= 0.75 && _mYCoord <= 2.1)
				_currentPiece.transform.parent.position = new Vector3(parentPos.x, _mYCoord, parentPos.z);
		}
		else if (canHorizontal && !Input.GetKey(KeyCode.LeftControl) && Input.GetMouseButton(0))
		{
			_mPosDelta = Input.mousePosition - _mPrevPos;
			_currentPiece.transform.Rotate(_parentUp, -Vector3.Dot(_mPosDelta, _parentRight), Space.World);
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
