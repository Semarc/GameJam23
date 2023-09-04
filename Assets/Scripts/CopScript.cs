using System.Collections;

using Pathfinding;

using UnityEngine;
using UnityEngine.SceneManagement;

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

	[Tooltip("Die Zeit, ab der der Cop die Verfolgung abbrechen kann.")]
	[SerializeField] private float leaveTime = 10;

	[Tooltip("Die Zeit, die der Cop außerhalb der Sicherweite seien muss, um die Verfolgung abzubrechen")]
	[SerializeField] private float graceTime = 5;

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
	private float followTime;
	private float outOfSightTime;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		seeker = GetComponent<Seeker>();
		currentTarget = patrolSpots[0];
		currentSpeed = patrolSpeed;

		GameManagerScript.Instance.AddCop(this);

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

	IEnumerator CantLeavePlayerTimerCo()
	{
		while (true)
		{
			if (followTime > leaveTime)
			{
				break;
			}
			followTime += Time.deltaTime;
			yield return null;
		}
	}

	IEnumerator GraceTimerCo()
	{
		Debug.Log("grace timer startet");
		while (true)
		{
			if (outOfSightTime > graceTime)
			{
				break;
			}
			outOfSightTime += Time.deltaTime;
			yield return null;
		}
	}

	public void StartFollowPlayer()
	{
		currentTarget = player;
		currentSpeed = followSpeed;
		StartCoroutine(CantLeavePlayerTimerCo());
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject == player.gameObject)
		{
			Debug.Log("Hit Cop, game over");

			GameManagerScript.Instance.FadeToBlack();
			Invoke(nameof(GamoOverScene), 2);
			//Todo: Game over
		}
	}

	private void GamoOverScene()
	{
		SceneManager.LoadScene("Game Over");
	}

	private void FixedUpdate()
	{
		if (Vector2.Distance(rb.position, player.position) < detectDistance)
		{
			if (currentTarget != player)
				StartFollowPlayer();
		}
		else if (Vector2.Distance(rb.position, player.position) > leaveDistance && followTime > leaveTime)
		{
			if (outOfSightTime == 0)
			{
				StartCoroutine(GraceTimerCo());
			}
			else if (outOfSightTime > graceTime)
			{
				currentTarget = patrolSpots[patrolSpotIndex];
				currentSpeed = patrolSpeed;
				followTime = 0;
				outOfSightTime = 0;

			}
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
		Vector2 force = currentSpeed * Time.fixedDeltaTime * direction;

		rb.AddForce(force);
		//Debug.Log(force);

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