using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.R))
		{
			transform.rotation *= Quaternion.Euler(0, 90, 0);
		}
	}
}
