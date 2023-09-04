using System.Collections;

using UnityEngine;
using UnityEngine.InputSystem;



[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
	public Rigidbody2D rb { get; private set; }
	private Vector2 moveDirection;
	[SerializeField] Animator[] animator;
	[SerializeField] private float speed = 400f;
	[SerializeField] private float disableDuration = 5f;

	private GeneratorScript GeneratorInRange;

	private bool cancelGeneratorDestruction = false;

	void Awake()
	{
		Debug.Log("Awake");
		rb = GetComponent<Rigidbody2D>();
	}

		// Update is called once per frame
	void Update()
	{
		float movementX = moveDirection.x;
		float movementY = moveDirection.y;
		animator[0].SetFloat("Horizontal", movementX);
		animator[0].SetFloat("Vertical", movementY);
		animator[0].SetFloat("Speed", moveDirection.magnitude);
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
			//Todo: grafical indicator f�r zerst�rung
			yield return null;
			if (totalTime >= disableDuration)
			{
				generator.DeactivateGenerator();
				break;
			}
		}
	}
}