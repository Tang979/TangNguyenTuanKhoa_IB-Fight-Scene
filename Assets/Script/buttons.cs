using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttons : MonoBehaviour
{
    public PlayerController playerController;
    // Start is called before the first frame update
    void Update()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    public void Attack()
    {
        playerController.EnableAttack();
    }
    public void Doge()
    {
        playerController.EnableDoge();
    }
}
