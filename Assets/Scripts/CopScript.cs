using Pathfinding;

using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Seeker))]
public class CopScript : MonoBehaviour
{
	Rigidbody2D rb;
	Seeker seeker;

	[SerializeField] private Transform target;
	[SerializeField] private float CopSpeed = 300f;
	[SerializeField] private float nextWaypointDistance = 3f;
	[SerializeField] private Transform GFXs;

	private Path path;
	private int currentWaypoint = 0;
	private bool reachedEndOfPath = false;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		seeker = GetComponent<Seeker>();

		InvokeRepeating(nameof(UpdatePath), 0, 1f);
	}

	private void UpdatePath()
	{
		if (seeker.IsDone())
			seeker.StartPath(rb.position, target.position, OnPathComplete);
	}

	private void OnPathComplete(Path p)
	{
		if (!p.error)
		{
			path = p;
			currentWaypoint = 0;
		}
		else
		{
			Debug.Log("Error");
		}
	}

	private void FixedUpdate()
	{
		Debug.Log("Fixed");
		if (path == null)
		{
			Debug.Log("No Path");
			return;
		}
		if (currentWaypoint >= path.vectorPath.Count)
		{
			Debug.Log("EndOfPath");
			reachedEndOfPath = true;
			return;
		}
		else
		{
			reachedEndOfPath = false;
		}

		Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
		Vector2 force = direction * CopSpeed * Time.fixedDeltaTime;

		rb.AddForce(force);
		Debug.Log("Force added");

		float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
		if (distance < nextWaypointDistance)
		{
			currentWaypoint++;
		}

		if (rb.velocity.x > float.Epsilon)
		{
			GFXs.localScale = new Vector3(-1, 1, 1);
		}
		else if (rb.velocity.x < float.Epsilon)
		{
			GFXs.localScale = Vector3.one;
		}
	}
}