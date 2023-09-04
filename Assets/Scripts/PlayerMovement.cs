using System.Collections;

using UnityEngine;
using UnityEngine.InputSystem;

using static UnityEngine.Rendering.DebugUI;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
	public Rigidbody2D rb { get; private set; }
	private Vector2 moveDirection;

	[SerializeField] private float speed = 400f;
	[SerializeField] private float disableDuration = 5f;

	private GeneratorScript GeneratorInRange;

	private bool cancelGeneratorDestruction = false;

	void Awake()
	{
		Debug.Log("Awake");
		rb = GetComponent<Rigidbody2D>();
	}

	private void OnMovement(InputValue movementValue)
	{
		Debug.Log("OnMovement");
		moveDirection = movementValue.Get<Vector2>();
	}
	private void OnInteract(InputValue interactValue)
	{
		bool keyPressed = true; //TODO
		if (GeneratorInRange != null)
		{
			if (keyPressed)
			{
				StartCoroutine(DestroyGeneratorCo(GeneratorInRange));
			}
			else
			{
				cancelGeneratorDestruction = true;
			}
		}
	}
	private void OnSwitchCharacter(InputValue value)
	{
		int ChoosenCharacter =  Mathf.FloorToInt(value.Get<float>());
		if (ChoosenCharacter > 0)
		{
			//Todo: Switch Character
		}
	}



	private void FixedUpdate()
	{
		rb.velocity = speed * Time.fixedDeltaTime * moveDirection;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent(out GeneratorScript generatorScript))
		{
			GeneratorInRange = generatorScript;
		}
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (GeneratorInRange != null && collision.gameObject == GeneratorInRange.gameObject)
		{
			GeneratorInRange = null;
		}
	}


	IEnumerator DestroyGeneratorCo(GeneratorScript generator)
	{
		float totalTime = 0;

		while (true)
		{
			if (cancelGeneratorDestruction)
			{
				//Todo: Reset grafical indicator
				break;
			}
			totalTime += Time.deltaTime;
			//Todo: grafical indicator für zerstörung
			yield return null;
			if (totalTime >= disableDuration)
			{
				generator.DeactivateGenerator();
				break;
			}
		}
	}
}