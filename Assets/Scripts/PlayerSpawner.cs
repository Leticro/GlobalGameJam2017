using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
	#region Private
	[SerializeField]
	private GameObject _playerPrefab;
	[SerializeField]
	private GameObject _cameraPrefab;
	#endregion

	private void Start()
	{
		var player = Instantiate(_playerPrefab, transform.position, transform.rotation);
		var camera = Instantiate(_cameraPrefab, transform.position, _cameraPrefab.transform.rotation);
		var follow = camera.GetComponent<Follow>();
		if (follow)
		{
			follow.player = player;
		}
	}
}
