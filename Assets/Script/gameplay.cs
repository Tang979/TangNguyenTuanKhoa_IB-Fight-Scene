using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameplay : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject player;
    [SerializeField] private Transform spawnPointEnemy;
    [SerializeField] private Transform spawnPointPlayer;
    [SerializeField] private int level = 1;
    [SerializeField] private GameObject enemy;
    
    public GameObject panel;
    public GameObject ui;
    public GameObject gameOverPanel;
    void Start()
    {
        Instantiate(enemy, spawnPointEnemy.position, Quaternion.identity);
        Instantiate(player, spawnPointPlayer.position, Quaternion.identity);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            panel.SetActive(true);
            ui.SetActive(false);
        }
        if (GameObject.FindGameObjectsWithTag("Player").Length == 0)
        {
            panel.SetActive(false);
            ui.SetActive(false);
            gameOverPanel.SetActive(true);
        }
    }

    public void NextLevel()
    {
        level++;
        panel.SetActive(false);
        ui.SetActive(true);
        for (int i = 0; i < level; i++)
        {
            Instantiate(enemy, spawnPointEnemy.position, Quaternion.identity);
        }
    }

    public void Dame()
    {
        Enity enityPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Enity>();
        enityPlayer.damage+= 10;
    }
    public void HealtRegen()
    {
        Enity enityPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Enity>();
        enityPlayer.healthRegen = enityPlayer.healthRegen*2;
    }
    public void healHealth()
    {
        Enity enityPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Enity>();
        enityPlayer.currenthealth += 50;
    }

    public void PlayAgain()
    {
        level = 0;
        gameOverPanel.SetActive(false);
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Enemy").Length; i++)
        {
            Destroy(GameObject.FindGameObjectsWithTag("Enemy")[i]);
        }
        Instantiate(player, spawnPointPlayer.position, Quaternion.identity);
        NextLevel();
    }

    
}
