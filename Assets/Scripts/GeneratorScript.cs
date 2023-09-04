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

	public bool GeneratorActive { get; private set; }

	private void Awake()
	{
		sr = GetComponent<SpriteRenderer>();
	}

	public void DeactivateGenerator()
	{
		GeneratorActive = false;
		sr.sprite = GeneratorDisabeledSprite;
	}
}
