using System;
using System.Collections;
using HeroSquad.Characters.Player.Movement;
using HeroSquad.ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace HeroSquad
{
	public class GameManager : MonoBehaviour
	{
		[SerializeField] private FollowTarget followTarget;
		[SerializeField] private EnemySpawner enemySpawner;
		[SerializeField] private RoundsSettings settings;
		[SerializeField] private Button restartButton;

		private int _enemiesSpawnCount;
		private int _currentEnemiesCount;
		private int _followersCount;
		private AsyncOperation _restartScene;

		private void Awake()
		{
			_enemiesSpawnCount = settings.StartEnemiesCount;
			enemySpawner.Init(followTarget);
			restartButton.onClick.AddListener(() => _restartScene.allowSceneActivation = true);
			restartButton.gameObject.SetActive(false);
		}
		
		private void Start()
		{
			SpawnEnemies();
			followTarget.AddActionToAllFollowersDieEvent(SubtractFollowersCount);
			_followersCount = followTarget.GetFollowersCount();
		}

		private void SetRestartButtonActive()
		{
			restartButton.gameObject.SetActive(true);
			RestartLevelAsync();
		}
		
		private void RestartLevelAsync()
		{
			_restartScene = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
			_restartScene.allowSceneActivation = false;
		}

		private void SubtractEnemiesCount()
		{
			_currentEnemiesCount--;
			if (_currentEnemiesCount <= 0)
			{
				SpawnEnemies();
			}
		}
		
		private void SubtractFollowersCount()
		{
			_followersCount--;
			if (_followersCount <= 0)
			{
				SetRestartButtonActive();
			}
		}

		private void SpawnEnemies()
		{
			_currentEnemiesCount = _enemiesSpawnCount;
			var enemies = enemySpawner.SpawnEnemies(_enemiesSpawnCount);
			_enemiesSpawnCount = Mathf.Min(_enemiesSpawnCount + settings.AddByRoundEnemiesCount, settings.MaximumEnemiesCount);
			foreach (var enemy in enemies)
			{
				enemy.OnDieEvent.AddListener(SubtractEnemiesCount);
			}
		}
	}
}