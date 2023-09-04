using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

public class GeneratorScript : MonoBehaviour
{
	private SpriteRenderer sr;

	[SerializeField] private Sprite GeneratorActiveSprite;
	[SerializeField] private Sprite GeneratorDisabeledSprite;

	[field: SerializeField] public bool GeneratorActive { get; private set; }

	private void Awake()
	{
		sr = GetComponent<SpriteRenderer>();
		GameManagerScript.Instance.AddGenerator(this);
	}

	public void DeactivateGenerator()
	{
		AudioScript.Instance.PlayFlashlightSound();
		GeneratorActive = false;
		Debug.Log("ChangeGeneratorSprite");
		sr.sprite = GeneratorDisabeledSprite;
		GameManagerScript.Instance.AlarmAllCops();
	}
}