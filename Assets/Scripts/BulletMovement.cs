using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class BulletMovement : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsServer)
        {
            MoveUpServerRpc();
        }
    }

    [ServerRpc]
    void MoveUpServerRpc()
    {
        transform.Translate(Vector3.up * 0.2f);
    }
}
