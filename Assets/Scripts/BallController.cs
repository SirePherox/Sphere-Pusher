using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BallController : NetworkBehaviour
{
    [SerializeField]
    private float ballSpeed = 0.05f;

    private Vector3 playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isLocalPlayer)
        {
            playerMovement = new Vector3(Input.GetAxis("Horizontal") * ballSpeed, 0, Input.GetAxis("Vertical") * ballSpeed);
            transform.position += playerMovement;
        }
;
    }
}
