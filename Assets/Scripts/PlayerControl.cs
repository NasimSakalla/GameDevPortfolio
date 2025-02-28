using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerControl : NetworkBehaviour
{
    [SerializeField]
    NetworkVariable<float> horizontalPos = new NetworkVariable<float>();

    [SerializeField]
    NetworkVariable<float> verticalPos = new NetworkVariable<float>();

    [SerializeField]
    float moveSpeed;

    float oldHorizntalPos , oldVerticalPos;

    [SerializeField]
    GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3 (Random.Range(-4f,4f) , transform.position.y , Random.Range(-4f,4f));
        moveSpeed = 0.3f;


    }

    // Update is called once per frame
    void Update()
    {
        if (IsServer)
        {
            UpdateServer();
        }

        if (IsClient && IsOwner)
        {
            UpdateClient();
        }
    }

    void UpdateServer()
    {
        //moves our player.
        transform.position = new Vector3(transform.position.x + verticalPos.Value , transform.position.y , 
                                         transform.position.z + horizontalPos.Value);

        bullet.transform.position = new Vector3(bullet.transform.position.x , bullet.transform.position.y + 0.1f , bullet.transform.position.z);

    }

    void UpdateClient()
    {
        //takes movement inputed and sends it to our server

        float horizntal = 0;
        float vertical = 0;

        if (Input.GetKey(KeyCode.W))
        {
            horizntal += moveSpeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            horizntal -= moveSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            vertical += moveSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            vertical -= moveSpeed;
        }
        

        if (oldHorizntalPos != horizntal || oldVerticalPos != vertical)
        {
            oldVerticalPos = vertical;
            oldHorizntalPos = horizntal;

            UpdateClientPosServerRpc(horizntal, vertical);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootBulletServerRpc();
        }
    }

    [ServerRpc]
    public void UpdateClientPosServerRpc(float horiz , float vert)
    {
        verticalPos.Value = vert;
        horizontalPos.Value = horiz;
    }

    [ServerRpc]
    void ShootBulletServerRpc()
    {
        GameObject temp = Instantiate(bullet, transform.position , Quaternion.identity);
        temp.GetComponent<NetworkObject>().Spawn();
    }
}
