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
	private Button _mainMenuButton;
	[SerializeField]
	private Button _nextLevelButton;

	private readonly string TIME_FORMAT = "Time: {0}";
	private readonly string DISTANCE_FORMAT = "Distance: {0:0.00} km";

	private GameObject _player;
	private float _startTime;
	private Finish _finish;
	private bool _hasStarted;
	private string _nextLevelString;
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
		Debug.Log("Set player");
		_player = player;
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
	}
}
