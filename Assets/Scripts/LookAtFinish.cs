using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtFinish : MonoBehaviour
{
	private Finish _finish;

	private void Start()
	{
		_finish = FindObjectOfType<Finish>();
	}

	private void Update()
	{
		if (_finish)
		{
			transform.LookAt(_finish.transform);
		}
	}
}
