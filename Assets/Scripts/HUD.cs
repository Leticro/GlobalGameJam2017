using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
	#region Private
	[SerializeField]
	private Follow _follow;
	[SerializeField]
	private Text _distanceText;
	[SerializeField]
	private Text _timeText;
	[SerializeField]
	private Text _countDownText;
	[SerializeField]
	private Text _speedText;
	[SerializeField]
	private Button _mainMenuButton;
	[SerializeField]
	private Button _nextLevelButton;

	private readonly string TIME_FORMAT = "Time: {0:0.00}";
	private readonly string DISTANCE_FORMAT = "Distance: {0:0.00} km";
	private readonly string SPEED_FORMAT = "Speed: {0:0.00} km/s";

	private GameObject _player;
	private float _startTime;
	private Finish _finish;
	private bool _hasStarted;
	private string _nextLevelString;
	private Rigidbody _playerRigidbody;
	#endregion

	public void StartGame()
	{
		_hasStarted = true;
		_startTime = Time.time;
	}

	public void StopGame()
	{
		_hasStarted = false;
	}

	public void SetCountDownText(string text)
	{
		_countDownText.text = text;
	}

	public void ShowButtons(string nextLevelName)
	{
		_mainMenuButton.gameObject.SetActive(true);
		_nextLevelButton.gameObject.SetActive(true);
		_nextLevelString = nextLevelName;
	}

	public void LoadMainMenu()
	{
		Time.timeScale = 1;
		Debug.LogWarning("TODO: once the main menu is implemented load that scene");
	}

	public void LoadNextLevel()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene(_nextLevelString);
	}

	private void Awake()
	{
		_follow.OnSetPlayer += OnSetPlayer;
		_startTime = Time.time;

		_mainMenuButton.gameObject.SetActive(false);
		_nextLevelButton.gameObject.SetActive(false);
	}

	private void Start()
	{
		_finish = FindObjectOfType<Finish>();
	}

	private void OnSetPlayer(GameObject player)
	{
		_player = player;
		_playerRigidbody = _player.GetComponent<Rigidbody>();
	}

	private void Update()
	{
		if (_player && _finish)
		{
			_distanceText.text = string.Format(DISTANCE_FORMAT, System.Math.Round(Vector3.Distance(_player.transform.position, _finish.transform.position), 2));
		}

		if (_hasStarted)
		{
			_timeText.text = string.Format(TIME_FORMAT, System.Math.Round(Time.time - _startTime, 2));
		}

		if (_playerRigidbody)
		{
			_speedText.text = string.Format(SPEED_FORMAT, _playerRigidbody.velocity.magnitude);
		}
	}
}
