using System.Runtime.ConstrainedExecution;

using Pathfinding;

using Unity.PlasticSCM.Editor.WebApi;

using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Rigidbody2D), typeof(Seeker))]
public class CopScript : MonoBehaviour
{
	Rigidbody2D rb;
	Seeker seeker;

	[Tooltip("Der Spieler")]
	[SerializeField] private Transform player;

	[Tooltip("Die Punkte, zwischen denen der Cop patroulieren soll. Nur einen Spot für einen still stehenden Cop")]
	[SerializeField] private Transform[] patrolSpots;

	[Tooltip("Die Geschwindigkeit, mit der der Spieler verfolgt wird")]
	[SerializeField] private float followSpeed = 300f;

	[Tooltip("Die Geschwindigkeit, die der Cop beim patroulieren haben. Sollte weniger als die Verfolge-Geschwindigkeit sein")]
	[SerializeField] private float patrolSpeed = 100f;

	[Tooltip("Der transform mit dem tatsächlichen GFX")]
	[SerializeField] private Transform GFX;

	[Tooltip("Die Distanz, ab der der Spieler verfolgt wird.")]
	[SerializeField] private float detectDistance = 5;

	[Tooltip("Die Distanz, ab der der Spieler nicht mehr verfolgt wird. Sollte mehr als die Verfolgedistanz sein")]
	[SerializeField] private float leaveDistance = 10;

	[Tooltip("Gibt an, ob die Grafik gespiegelt werden soll, wenn der Cop nach rechts bzw. links läuft")]
	[SerializeField] private bool switchGFXDirection = true;

	[Tooltip("Die Distanz, bei dem im Pfad zum nächsten Wegpunkt gewechselt wird. Am besten so lassen")]
	[SerializeField] private float nextWaypointDistance = 3f;

	[Tooltip("Die Distanz, ab der der nächste Patroulier-Spot verfolgt wird. Am besten so lassen")]
	[SerializeField] private float finalPathSpotDistance = 1f;

	private float currentSpeed;
	private Transform currentTarget;
	private int patrolSpotIndex;
	private Path path;
	private int currentWaypoint = 0;
	private bool reachedEndOfPath = false;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		seeker = GetComponent<Seeker>();
		currentTarget = patrolSpots[0];
		currentSpeed = patrolSpeed;

		InvokeRepeating(nameof(UpdatePath), 0, 0.5f);
	}

	private void UpdatePath()
	{
		if (seeker.IsDone())
			seeker.StartPath(rb.position, currentTarget.position, OnPathComplete);
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
			Debug.Log(p.errorLog);
		}
	}

	private void FixedUpdate()
	{
		if (Vector2.Distance(rb.position, player.position) < detectDistance)
		{
			currentTarget = player;
			currentSpeed = followSpeed;
		}
		else if (Vector2.Distance(rb.position, player.position) > leaveDistance)
		{
			currentTarget = patrolSpots[patrolSpotIndex];
			currentSpeed = patrolSpeed;
		}

		if (path == null)
		{
			return;
		}
		if (currentWaypoint >= path.vectorPath.Count)
		{
			reachedEndOfPath = true;
			return;
		}
		else
		{
			reachedEndOfPath = false;
		}

		Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
		Vector2 force = direction * currentSpeed * Time.fixedDeltaTime;

		rb.AddForce(force);
		Debug.Log(force);

		float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
		if (distance < nextWaypointDistance && currentWaypoint != path.vectorPath.Count || distance < finalPathSpotDistance)
		{
			currentWaypoint++;
		}
		if (Vector2.Distance(rb.position, currentTarget.position) < finalPathSpotDistance)
		{
			patrolSpotIndex++;
			patrolSpotIndex %= patrolSpots.Length;
			currentTarget = patrolSpots[patrolSpotIndex];
		}
		if (switchGFXDirection)
		{
			if (rb.velocity.x > float.Epsilon)
			{
				GFX.localScale = new Vector3(-1, 1, 1);
			}
			else if (rb.velocity.x < float.Epsilon)
			{
				GFX.localScale = Vector3.one;
			}
		}
	}
}