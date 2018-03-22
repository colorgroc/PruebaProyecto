﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {
	//public Text playerID;
	public Text score;
	public Text kills;
	public Text survived;
    [SerializeField]
    private GameObject player;
    //Canvas player1, player2;
    // Use this for initialization
    private void Awake()
    {
        this.score.text = this.kills.text = this.survived.text = "0";
    }
    void Start () {
        //player = GetComponent<GameObject>();
        if (this.gameObject.name == "Player1HUD")
            this.player = GameObject.Find("Player 1");
        else if (this.gameObject.name == "Player2HUD")
            this.player = GameObject.Find("Player 2");
        else if (this.gameObject.name == "Player3HUD")
            this.player = GameObject.Find("Player 3");
        else if (this.gameObject.name == "Player4HUD")
            this.player = GameObject.Find("Player 4");

	}

	// Update is called once per frame
	void Update () {
        /*if (this.gameObject.name == "Player1HUD")
            this.player = GameObject.Find("Player 1");
        else if (this.gameObject.name == "Player2HUD")
            this.player = GameObject.Find("Player 2");
        else if (this.gameObject.name == "Player3HUD")
            this.player = GameObject.Find("Player 3");
        else if (this.gameObject.name == "Player4HUD")
            this.player = GameObject.Find("Player 4");*/

        this.score.text = this.player.GetComponent<PlayerControl> ().scoreGeneral.ToString();
		this.kills.text = this.player.GetComponent<PlayerControl> ().scoreKills.ToString();
		this.survived.text = this.player.GetComponent<PlayerControl> ().scoreWins.ToString();
	}
}
