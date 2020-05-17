using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapSystem : MonoBehaviour
{

	public int no_checks;
	public int curr_check;
	public int no_laps;
	public int curr_lap;
	public GameObject go_ui;
	private bool isPaused = false;

	public void pauseGame()
	{
		if (isPaused)
		{
			Time.timeScale = 1;
			isPaused = false;
		}
		else {
			Time.timeScale = 0;
			isPaused = true;
		}
	}
	void Start()
	{

		no_checks = GameObject.Find("Checkpoints").transform.childCount;
		curr_check = 1;
		no_laps = 1;
		curr_lap = 1;
	}

	void Update()
	{

		if (curr_check > no_checks)
		{
			curr_lap++;
			curr_check = 1;
		}

		if (curr_lap > no_laps)
		{
			pauseGame();
			go_ui.SetActive(true);
		}
	}

	void OnTriggerEnter(Collider check_col)
	{
		if (check_col.name == curr_check.ToString())
		{
			curr_check++;
		}
	}
}
