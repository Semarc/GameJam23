using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Cinemachine;

using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
	public static GameManagerScript Instance { get; private set; }

	private List<GeneratorScript> generators = new List<GeneratorScript>();
	private List<CopScript> cops = new List<CopScript>();
	public int generatorCount { get { return generators.Count; } }
	public int disablededGeneratorCount { get { return generators.Where(x => x.GeneratorActive == false).Count(); } }

	[SerializeField] private CinemachineVirtualCamera virtualCamera;
	[SerializeField] private CinemachineVirtualCamera fadeToBlackCamera;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Debug.Log("DuplicateGameManager");
		}
	}

	private void Update()
	{
		if (disablededGeneratorCount > generatorCount)
		{
			//Todo: Hier kommt der Spiel-Ende-Code rein
		}
	}


	public void AddGenerator(GeneratorScript generator)
	{
		generators.Add(generator);
	}
	public void AddCop(CopScript cop)
	{
		cops.Add(cop);
	}
	public void AlarmAllCops()
	{
		foreach (var cops in cops)
		{
			cops.StartFollowPlayer();
		}
	}

	public void FadeToBlack()
	{
		virtualCamera.Priority = 0;
		fadeToBlackCamera.Priority = 1;
	}
}