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
	private HUD _hud;
	private WaveMaker _waveMaker;
    private AudioSource _audio;
	#endregion

	private void Start()
	{
		Spawn();
	}

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
        if (_audio)
        {
            _audio.Play();
        }
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
			follow.SetPlayer(_player);
		}

		_hud = _camera.GetComponentInChildren<HUD>();
		_waveMaker = _camera.GetComponentInChildren<WaveMaker>();

		StartCoroutine(CountDown());
	}

	private IEnumerator CountDown()
	{
		_hud.SetCountDownText("3");
		yield return new WaitForSeconds(1);
		_hud.SetCountDownText("2");
		yield return new WaitForSeconds(1);
		_hud.SetCountDownText("1");
		yield return new WaitForSeconds(1);
		_hud.SetCountDownText("GO!");
		_waveMaker.StartGame();
		_hud.StartGame();
		yield return new WaitForSeconds(1);
		_hud.SetCountDownText("");
	}
}
