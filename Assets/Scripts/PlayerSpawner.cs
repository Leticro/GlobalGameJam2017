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

	private GameObject _player;
	private GameObject _camera;
	#endregion

	private void Start()
	{
		Spawn();
	}

	public void Spawn()
	{
		Destroy(_player);
		Destroy(_camera);

		_player = Instantiate(_playerPrefab, transform.position, transform.rotation);
		_camera = Instantiate(_cameraPrefab, transform.position, _cameraPrefab.transform.rotation);
		var follow = _camera.GetComponent<Follow>();
		if (follow)
		{
			follow.player = _player;
		}
	}
}
