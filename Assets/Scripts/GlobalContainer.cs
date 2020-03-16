﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GlobalContainer : MonoBehaviour {

    public static GlobalContainer global;
    [SerializeField] public GameObject floor;
    public List<List<int>> tiles;
    public bool playerMoved;
    public GameObject enemies;
    public GameObject player;
    public GameObject items;
    public GameObject enviro;

    public Canvas canvas;
    

    private void Awake() {
        global = this;
    }

    private void Start()
    {
        
    }

    private void Update()
    {

        //Upatae all enemies
        if (playerMoved)
        {
            foreach (var enemy in enemies.GetComponentsInChildren<EnemyMovement>())
            {
                enemy.move();
            }
            foreach (var obj in enviro.GetComponentsInChildren<SpikeInteracter>()) {
                obj.updateSpike();
            }

            playerMoved = false;

        }

        
        //Adjust sorting layer depending on y position
        //TODO: make this more efficient later
        foreach (Transform t in enemies.transform) { YOrder(t); }
        foreach (Transform t in enviro.transform) { YOrder(t); }
        foreach (Transform t in items.transform) { YOrder(t); }

        Transform ptrans = player.GetComponent<PlayerInfo>().trans;
        SpriteRenderer psr = player.GetComponent<SpriteRenderer>();
        psr.sortingOrder = (int)(ptrans.position.y * -100);


        //Update score
        TextMeshProUGUI tm = canvas.GetComponentInChildren<TextMeshProUGUI>();
        tm.text = player.GetComponent<PlayerInfo>().score.ToString();
        
    }

    private static void YOrder(Transform t) {
        SpriteRenderer sr = t.gameObject.GetComponent<SpriteRenderer>();
        if (sr) { //if not null
            sr.sortingOrder = (int)(sr.transform.position.y * -100);
        }
    }
}
