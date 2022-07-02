using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ReceiveDamage : NetworkBehaviour
{
    [SerializeField]
    private float maxHealth = 10.0f;
    [SyncVar]
    private float currentHealth;
    [SerializeField]
    private string objectTag; //tags of various GameObjects....[enemy,bullet,player]
    [SerializeField]
    private bool destroyOnDeath; //whether to destroy the object or respawn

    private Vector3 initialPos;
    private 

    // Start is called before the first frame update
    void Start()
    {
        initialPos = this.transform.position;
        currentHealth = maxHealth;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("bullet"))//if player collides with enemy , apply damage to player
        {                               //and destroy the enemy
            this.ApplyDamage(5);
        }
 
    }

    void ApplyDamage(int damage)
    {
        if (this.isServer)
        {
            this.currentHealth -= damage;

            if(this.currentHealth <= 0)
            {
                if (this.destroyOnDeath)
                {
                    Destroy(this.gameObject);
                }
                else
                {
                    this.currentHealth = this.maxHealth;
                    RpcRespawn();
                }
            }
        }
    }

    [ClientRpc]
    void RpcRespawn()
    {
        this.transform.position = initialPos;
    }
}
