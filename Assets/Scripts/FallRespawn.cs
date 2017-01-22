using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallRespawn : MonoBehaviour
{
	#region Private
	private Transform _transform;
	private PlayerSpawner _spawn;

    [SerializeField]
    private AudioSource _audio;
	[SerializeField]
	private float _killY;
	#endregion

	private void Awake()
	{
		_transform = GetComponent<Transform>();
		_spawn = FindObjectOfType<PlayerSpawner>();
        _audio = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
	{
        if (_transform.position.y <= _killY)
		{
            if (_audio)
            {
                _audio.Play();
            }
            _spawn.Spawn();
        }
	}
}
