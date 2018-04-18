using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[System.Serializable]
public class IntEvent : UnityEvent<int> { }

public class GameManager : MonoBehaviour
{
	static GameManager _instance;
	static GameManager Instance
	{
		get
		{
			if (_instance) return _instance;
			_instance = FindObjectOfType<GameManager>();

			if (_instance) return _instance;
			_instance = new GameObject("GameManager").AddComponent<GameManager>();

			return _instance;
		}
	}

	[SerializeField]
	float maxCastleHealth;

	[SerializeField]
	float currentCastleHealth;

	[SerializeField]
	UnityEvent OnGameover;

	[SerializeField]
	IntEvent OnCastleTakeDamage;

	[SerializeField]
	Text castleHealthText;

	bool gameover;

	private void Awake()
	{
		Time.timeScale = 1;
	}

	private void Update()
	{
		if (gameover && Input.anyKeyDown)
			ReloadLevel();
	}

	public static void CastleTakeDamage(float damage)
	{
		Instance.currentCastleHealth -= damage;
		Instance.castleHealthText.text = Instance.currentCastleHealth.ToString();
		if (Instance.currentCastleHealth <= 0)
			Gameover();
	}

	public static void Gameover()
	{
		Time.timeScale = 0;
		Instance.gameover = true;
		Instance.OnGameover.Invoke();
	}

	public static void ReloadLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
