using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button host;
    public Button client;
    
    void Start()
    {
        host.onClick.AddListener(StartHostLocal);
        client.onClick.AddListener(StartClientLocal);
    }

    public void StartHostLocal()
    {
        NetworkManager.Singleton.StartHost();

    }
    public void StartClientLocal()
    {
        NetworkManager.Singleton.StartClient();
    }
}
