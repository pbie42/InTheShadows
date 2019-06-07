using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSpotlight : MonoBehaviour
{
  private float _rotX = 0.0f;
  private float _rotY = 0.0f;
  private float _rotZ = 0.0f;
  private bool _increase = true;

  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    Debug.Log("transform.eulerAngles.x: " + transform.localEulerAngles.x);
    if (transform.localEulerAngles.x <= -7.0f)
      _increase = true;
    else if (transform.localEulerAngles.x >= 7.0f)
      _increase = false;

    if (_increase)
      _rotX += 0.1f;
    else
      _rotX -= 0.1f;
    transform.rotation = Quaternion.Euler(_rotX * Time.deltaTime, 90, 0);
  }
}
