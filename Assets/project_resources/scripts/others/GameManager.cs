using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour 
{
	#region Static Members
	private static GameManager instance = null;
	#endregion

	#region Private Members
	private bool initialized;					// Is singleton instance initialized
	private int alreadyPlayed;					// Has player already played before
	private int coins;							// Player total coins amount
	private int bestScore;						// Best game score amount
	private int volume;							// Enabled volume current state
	private int[] ships;						// Purchased ships states
	private int currentShip;					// Current selected ship
	private int noAds;							// Purchased no advertising state
	private int totalTimes;						// Total times played

	#endregion

	#region Initialization Methods

	#region Initialization Internal Methods
	private void Initialize()
	{
		// Initialize values
		initialized = true;
		alreadyPlayed = 0;
		coins = 0;
		bestScore = 0;
		volume = 1;
		ships = new int[6];
		ships[0] = 1;
		noAds = 0;
		currentShip = 0;
		totalTimes = 0;

		LoadData();

		// Initialize platform manager

		#if DEBUG_INFO
		Debug.Log("GameManager: game manager initialized successfully");
		#endif
	}
	#endregion
	#endregion

	#region Data Methods
	public void SaveData()
	{
		// Update in game values to PlayerPrefs
		PlayerPrefs.SetInt("Saved", 0);
		PlayerPrefs.SetInt("AlreadyPlayed", alreadyPlayed);
		PlayerPrefs.SetInt("coins", coins);
		PlayerPrefs.SetInt("bestScore", bestScore);
		PlayerPrefs.SetInt("volume", volume);
		PlayerPrefs.SetInt("CurrentShip", currentShip);
		PlayerPrefs.SetInt("NoAds", noAds);
		PlayerPrefs.SetInt("TotalTimes", totalTimes);
		for (int i = 0; i < ships.Length; i++) PlayerPrefs.SetInt("Ship" + i, ships[i]);

		// Save all data in PlayerPrefs to disk
		PlayerPrefs.Save();

		#if DEBUG_INFO
		Debug.Log("GameManager: local data saved successfully");
		#endif
	}

	#region Data Internal Methods
	private void LoadData()
	{
		#if DEBUG_INFO
		Debug.Log("GameManager: attempting to load local data");
		#endif

		// Check if player has data already saved
		if (!PlayerPrefs.HasKey("Saved")) SaveData();
		else
		{
			// Update in game values from PlayerPrefs
			alreadyPlayed = PlayerPrefs.GetInt("AlreadyPlayed");
			coins = PlayerPrefs.GetInt("coins");
			bestScore = PlayerPrefs.GetInt("bestScore");
			volume = PlayerPrefs.GetInt("volume");
			currentShip = PlayerPrefs.GetInt("CurrentShip");
			noAds = PlayerPrefs.GetInt("NoAds");
			totalTimes = PlayerPrefs.GetInt("TotalTimes");
			for (int i = 0; i < ships.Length; i++) ships[i] = PlayerPrefs.GetInt("Ship" + i);

			// Initialize external members
			AudioListener.volume = volume;

			#if DEBUG_INFO
			Debug.Log("GameManager: local data loaded successfully");
			#endif
		}
	}
	#endregion
	#endregion

	#region Properties
	public static GameManager Instance
	{
		get 
		{
			if(!instance) 
			{
				instance = (GameManager)FindObjectOfType(typeof(GameManager));
				
				if(!instance)
				{
					instance = (new GameObject("GameManager")).AddComponent<GameManager>();

					#if DEBUG_INFO
					instance.gameObject.AddComponent<DebugConsole>();
					#endif

					if (!instance.initialized) instance.Initialize();
					DontDestroyOnLoad (instance.gameObject);
				}
			}

			return instance;
		}
	}

	public bool Initialized
	{
		get { return initialized; }
	}

	public int AlreadyPlayed
	{
		get { return alreadyPlayed; }
		set { alreadyPlayed = value; }
	}

	public int Coins
	{
		get { return coins; }
		set { coins = value; }
	}

	public int BestScore
	{
		get { return bestScore; }
		set { bestScore = value; }
	}

	public int Volume
	{
		get { return volume; }
		set { volume = value; }
	}

	public int[] Ships
	{
		get { return ships; }
		set { ships = value; }
	}

	public int CurrentShip
	{
		get { return currentShip; }
		set { currentShip = value; }
	}

	public int NoAds
	{
		get { return noAds; }
		set { noAds = value; } 
	}

	public int TotalTimes
	{
		get { return totalTimes; }
		set { totalTimes = value; }
	}

	public bool ShowingAd
	{
		get 
		{
			return true;
		}
	}

	#endregion
}
