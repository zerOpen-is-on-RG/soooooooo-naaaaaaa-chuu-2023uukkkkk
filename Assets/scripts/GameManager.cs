using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //enemy object
    public GameObject enemy;
    public GameObject GunEnemy;

    //spawned enemies
    [HideInInspector]
    public List<Enemy> enemies = new();

    //player spawn pos
    private Vector2 spawnPos = new(8, -3.18f);

    //enemy spawning delay
    private float spawningDelay = 0;

    public int score = 0;
    public int lv = 1;
    public int xp = 0;

    private Player player;

    public GameObject resetPanel;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        GameStart();
    }

    public void GameStart()
    {
        //remove all spawned enemies
        for (int i = 0; i < enemies.Count; i++) {
            var e = enemies[i];

            enemies.Remove(e);
            Destroy(e.gameObject);
        }

        //move player to spawn pos
        player.transform.position = new Vector2(-5, -3.3f);
        player._Start();
    }

    public void GamePause()
    {
        resetPanel.SetActive(true);
        Time.timeScale=0;
    }
    public void Cancel()
    {
        resetPanel.SetActive(false);
        Time.timeScale=1;
    }
    public void ResetAccount()
    {
        Cancel();
        lv=1;
        score=0;
        GameStart();
    }

    private void Update()
    {
        //if enemy count less then 12
        if (enemies.Count < 12)
        {
            //count spawning delay regardless of update cycle;
            spawningDelay += Time.deltaTime;

            float delay = 1;
            if (enemies.Count > 5) delay = 2.5f;

            if (spawningDelay > delay)
            {
                //copy enemy prefab to gameObject
                GameObject enem = Instantiate(enemy, new Vector2(spawnPos.x * Random.Range(-1, 1), spawnPos.y), Quaternion.identity);

                //add enemy at spawned enemies list
                enemies.Add(enem.GetComponent<Enemy>());

                spawningDelay = 0;
            }
        }
        if(Input.GetKeyDown(KeyCode.Q)&&player.isAlive)
        {
            GamePause();
        }
        //if player is dead
        if (!player.isAlive)
        {
            //detect key pressing (spacebar)
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameStart(); //start game
            }
        }
    }
}
