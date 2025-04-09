using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enity : MonoBehaviour
{
    public float starthealth = 100f;
    public float currenthealth = 100f;
    public float healthRegen = 0.1f;
    public float damage = 10f;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        currenthealth = starthealth;
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    public void HealthRegen()
    {
        if (currenthealth >= starthealth)
        {
            currenthealth = starthealth;
            return;
        }
        currenthealth += healthRegen * Time.deltaTime;
    }
    public void TakeDame(float damage)
    {
        currenthealth -= damage;
        if (currenthealth <= 0)
        {
            currenthealth = 0;
            animator.SetTrigger("Die");
        }
    }
    
}
