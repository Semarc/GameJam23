using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
	public static GameManagerScript Instance { get; private set; }

	private List<GeneratorScript> generators = new List<GeneratorScript>();
	public int generatorCount { get { return generators.Count; } }
	public int disablededGeneratorCount { get { return generators.Where(x => x.GeneratorActive == false).Count(); } }

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
}