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
	[SerializeField]
	private Text _scoreHeaderText;
	[SerializeField]
	private List<Text> _scoreFieldsText;
	[SerializeField]
	private GameObject _newScoreGO;
	[SerializeField]
	private InputField _inputField;
	[SerializeField]
	private Button _submitScoreButton;

	private readonly string TIME_FORMAT = "Time: {0:0.00}";
	private readonly string DISTANCE_FORMAT = "Distance: {0:0.00} m";
	private readonly string SPEED_FORMAT = "Speed: {0:0.00} m/s";
	private readonly string SCORE_FORMAT = "{0} \t\t {1}";

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

	/// AKA Show Score Screen.
	public void ShowButtons(string nextLevelName)
	{
		_nextLevelString = nextLevelName;
		
		if (Leaderboards.IsOnScoreboard(Time.time - _startTime, SceneManager.GetActiveScene().name))
		{
			_newScoreGO.SetActive(true);
			_inputField.gameObject.SetActive(true);
			_submitScoreButton.gameObject.SetActive(true);
		}
		else
		{
			_mainMenuButton.gameObject.SetActive(true);
			_nextLevelButton.gameObject.SetActive(true);

			ShowScores();
		}
	}

	public void SubmitScore()
	{
		var position = Leaderboards.AddScore(_inputField.text, Time.time - _startTime, SceneManager.GetActiveScene().name);

		ShowScores();

		if (position < _scoreFieldsText.Count)
		{
			_scoreFieldsText[position].color = new Color(1, 0.88671f, 0, 1);
		}

		_newScoreGO.SetActive(false);
		_inputField.gameObject.SetActive(false);
		_submitScoreButton.gameObject.SetActive(false);

		_mainMenuButton.gameObject.SetActive(true);
		_nextLevelButton.gameObject.SetActive(true);
	}

	public void LoadMainMenu()
	{
		Time.timeScale = 1;
		var spawner = FindObjectOfType<PlayerSpawner>();
		if (spawner)
		{
			spawner.Spawn();
		}
	}

	public void LoadNextLevel()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene(_nextLevelString);
	}

	private void ShowScores()
	{
		var scores = Leaderboards.GetScores(SceneManager.GetActiveScene().name);

		if (scores.Count > 0)
		{
			_scoreHeaderText.gameObject.SetActive(true);
			_scoreHeaderText.text = "Name \t\t Score";
		}
		
		for (var i = 0; i < scores.Count && i < _scoreFieldsText.Count; ++i)
		{
			_scoreFieldsText[i].gameObject.SetActive(true);
			_scoreFieldsText[i].text = string.Format(SCORE_FORMAT, scores[i].userName, System.Math.Round(scores[i].score, 2));
		}
	}

	private void Awake()
	{
		_follow.OnSetPlayer += OnSetPlayer;
		_startTime = Time.time;

		_mainMenuButton.gameObject.SetActive(false);
		_nextLevelButton.gameObject.SetActive(false);

		_scoreHeaderText.gameObject.SetActive(false);
		foreach(var sf in _scoreFieldsText)
		{
			sf.gameObject.SetActive(false);
		}

		_newScoreGO.SetActive(false);
		_inputField.gameObject.SetActive(false);
		_submitScoreButton.gameObject.SetActive(false);
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
