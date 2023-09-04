using UnityEngine;
using UnityEngine.InputSystem;



[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
	public Rigidbody2D rb { get; private set; }
	private Vector2 moveDirection;
	[SerializeField] Animator[] animator;
	[SerializeField] private float speed = 400f;

	void Awake()
	{
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
		moveDirection = movementValue.Get<Vector2>();
	}
	private void FixedUpdate()
	{
		rb.velocity = moveDirection * speed * Time.fixedDeltaTime;
	}
}