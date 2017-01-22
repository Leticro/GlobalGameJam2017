using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public struct Score
{
	public string userName;
	public float score; // time

	public Score(string un, float score)
	{
		userName = un;
		this.score = score;
	}
}

public static class Leaderboards
{
	#region Private
	private static readonly string NAME_KEY_POSTFIX = "_name";
	private static readonly string VALUE_KEY_POSFIX = "_value";

	private static Dictionary<string, Dictionary<int, Score>> _scores;
	#endregion

	static Leaderboards()
	{
		_scores = new Dictionary<string, Dictionary<int, Score>>();

		var levelData = Resources.Load<LevelData>("LevelData");
		if (levelData)
		{
			foreach (var sceneName in levelData.levelNames)
			{
				for (var i = 0; i < 5; ++i)
				{
					if (PlayerPrefs.HasKey(sceneName + NAME_KEY_POSTFIX + i))
					{
						var userName = PlayerPrefs.GetString(sceneName + NAME_KEY_POSTFIX + i);
						var score = PlayerPrefs.GetFloat(sceneName + VALUE_KEY_POSFIX + i);

						if (!_scores.ContainsKey(sceneName))
						{
							_scores.Add(sceneName, new Dictionary<int, Score>());
						}

						_scores[sceneName].Add(i, new Score(userName, score));
					}
				}
			}
		}
	}

	public static bool IsOnScoreboard(float time, string levelName)
	{
		if (!_scores.ContainsKey(levelName))
		{
			return true;
		}

		var position = _scores[levelName].Count;
		foreach (var kvp in _scores[levelName])
		{
			if (kvp.Value.score > time && position > kvp.Key)
			{
				position = kvp.Key;
			}
		}

		return position < 5;
	}

	public static int AddScore(string userName, float time, string levelName)
	{
		if (!_scores.ContainsKey(levelName))
		{
			_scores.Add(levelName, new Dictionary<int, Score>());
		}

		// see if we have a new high score.
		var position = _scores[levelName].Count;
		foreach (var kvp in _scores[levelName])
		{
			if (kvp.Value.score > time && position > kvp.Key)
			{
				position = kvp.Key;
			}
		}

		if (_scores[levelName].Count <= 0)
		{
			position = 0;
		}

		// TODO: don't hard code the max leaderboard positions.
		if (position < 5)
		{
			var score = new Score(userName, time);
			var oldScores = new List<Score>();

			for (var i = position; i < _scores[levelName].Count; ++i)
			{
				if (_scores[levelName].ContainsKey(i))
				{
					oldScores.Add(_scores[levelName][i]);
				}
			}

			if (!_scores[levelName].ContainsKey(position))
			{
				_scores[levelName].Add(position, score);
			}
			else
			{
				_scores[levelName][position] = score;
			}

			for (var i = 0; i < oldScores.Count && i < 5; ++i)
			{
				if (!_scores[levelName].ContainsKey(position + 1 + i))
				{
					_scores[levelName].Add(position + 1 + i, oldScores[i]);
				}
				else
				{
					_scores[levelName][position + 1 + i] = oldScores[i];
				}
			}

			// for (var i = 0; i < 5 - (position + 1) && i < oldScores.Count; ++i)
			// {
			// 	if (!_scores[levelName].ContainsKey(position + 1 + i))
			// 	{
			// 		_scores[levelName].Add(position + 1 + i, oldScores[i]);
			// 	}
			// 	else
			// 	{
			// 		_scores[levelName][position + 1 + i] = oldScores[i];
			// 	}
			// }

			foreach (var kvp in _scores[levelName])
			{
				PlayerPrefs.SetString(levelName + NAME_KEY_POSTFIX + kvp.Key, kvp.Value.userName);
				PlayerPrefs.SetFloat(levelName + VALUE_KEY_POSFIX + kvp.Key, kvp.Value.score);
			}

			PlayerPrefs.Save();
		}

		return position;
	}

	public static List<Score> GetScores(string name)
	{
		if (_scores.ContainsKey(name))
		{
			return new List<Score>(_scores[name].Values);
		}
		return new List<Score>();
	}
}
