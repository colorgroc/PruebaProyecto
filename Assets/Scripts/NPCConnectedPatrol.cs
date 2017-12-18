﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCConnectedPatrol : MonoBehaviour {

	[SerializeField]
	bool _patrolWaiting;

	[SerializeField]
	float _totalWaitTime = 3f;

	[SerializeField]
	float switchProbability = 0.2f;

	NavMeshAgent _navMeshAgent;
	ConnectedWaypoint _currentWaypoint;
	ConnectedWaypoint _previousWaypoint;
	public GameObject[] allWaypoints;

	public GameObject playerTarget;

	bool _travelling;
	bool waiting;
	public bool playerOnFieldView;

	float _waitTimer;
	int _waypointsVisited;
	Player player;

	public bool isDead;
	public float count;


	// Use this for initialization
	public void Start () {

		_navMeshAgent = this.GetComponent<NavMeshAgent> ();

		if (_navMeshAgent == null) Debug.LogError ("The nav mesh agent component is not attached to " + gameObject.name);
		else {
			if (_currentWaypoint == null) {
				allWaypoints = GameObject.FindGameObjectsWithTag ("Waypoint");

				if (allWaypoints.Length > 0) {
					while (_currentWaypoint == null) {
						int random = UnityEngine.Random.Range (0, allWaypoints.Length);
						ConnectedWaypoint startingWaypoint = allWaypoints [random].GetComponent<ConnectedWaypoint> ();

						if (startingWaypoint != null) _currentWaypoint = startingWaypoint;
					}
				} else Debug.LogError ("Failed to find any waypoints for use in the scene");		
			}
		}
		SetDestination ();
	}
	
	// Update is called once per frame
	public void Update () {
		/*if (isDead) {
			//this.enabled = false;
			count += Time.deltaTime;
			if (count >= 3) {
				player.gameObject.SetActive (true);
			}
			//StartCoroutine (Respawn (random, 3));
			//Invoke ("Respawn", 3);
		}*/
		/*if (isDead) {
			count += Time.deltaTime;
			if (count >= 3) {
				count = 0;
				isDead = false;

			}
		}*/

		if (_travelling && _navMeshAgent.remainingDistance <= 1.0f) {
			_travelling = false;
			_waypointsVisited++;

			if (_patrolWaiting) {
				waiting = true;
				_waitTimer = 0f;
			} else
				SetDestination ();
		}
		if (waiting) {
			_waitTimer += Time.deltaTime;
			if (_waitTimer >= _totalWaitTime) {
				waiting = false;
				SetDestination ();
			}
		}
		if (playerOnFieldView) {
			ChacePlayer (playerTarget.transform.position);
			//Debug.Log ("chacing");
		}
	}

	public void SetDestination(){
		if (_waypointsVisited > 0) {
			ConnectedWaypoint nextWaypoint = _currentWaypoint.NextWaypoint (_previousWaypoint);
			_previousWaypoint = _currentWaypoint;
			_currentWaypoint = nextWaypoint;
		} 
		Vector3 targetVector = _currentWaypoint.transform.position;
		_navMeshAgent.SetDestination (targetVector);
		_travelling = true;
	}

	public void ChacePlayer(Vector3 targetVector){
		_navMeshAgent.SetDestination (targetVector);
	}
	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.tag.Equals("Guard") || collision.gameObject.tag.Equals("Killer Guards")) {
			//_waypointsVisited++;
			SetDestination ();
		}
		if (this.gameObject.tag.Equals("Killer Guards") && collision.gameObject.layer == 8) {
			//collision.gameObject.GetComponent<Player> ().isDead = true;
			//player = collision.gameObject.GetComponent<Player> ();
			collision.gameObject.SetActive (false);
			FieldOfView.alive = false;
			//isDead = collision.gameObject.GetComponent<Player> ().isDead;
			collision.gameObject.GetComponent<Player> ().Respawn (collision.gameObject);
			//this.playerOnFieldView = false;

			//Invoke ("collision.gameObject.GetComponent<Player> ().Respawn", 3);
			//_waypointsVisited++;
			//collision.gameObject.GetComponent<Respawn>()
		}
	}

}
