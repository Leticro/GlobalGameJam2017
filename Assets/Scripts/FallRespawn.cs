using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallRespawn : MonoBehaviour
{
	#region Private
	private Transform _transform;
	private PlayerSpawner _spawn;
	[SerializeField]
	private float _killY;
	#endregion

	private void Awake()
	{
		_transform = GetComponent<Transform>();
		_spawn = FindObjectOfType<PlayerSpawner>();
	}

	private void FixedUpdate()
	{
		if (_transform.position.y <= _killY)
		{
			_spawn.Spawn();
		}
	}
}
