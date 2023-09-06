using kcp2k;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputIp;
    [SerializeField] private TMP_InputField inputPort;
    private void Awake()
    {
        inputPort.text = 4444.ToString();
        inputIp.text = "127.0.0.1";
        DontDestroyOnLoad(gameObject);
       
    }


    public void StartAsHost()
    {
        KcpTransport transport = Transport.active as KcpTransport;
        if(transport != null)
        {
            transport.Port = ushort.Parse(inputPort.text);
            NetworkManager.singleton.networkAddress = inputIp.text;
            
        }
        NetworkManager.singleton.StartHost();
    }

    public void StartAsClient()
    {
        KcpTransport transport = Transport.active as KcpTransport;
        if (transport != null)
        {
            transport.Port = ushort.Parse(inputPort.text);
            NetworkManager.singleton.networkAddress = inputIp.text;
        }

        NetworkManager.singleton.StartClient();
       
    }

    public void ExitNetworkGame()
    {
        var userMode = NetworkManager.singleton.mode;

        if (userMode == NetworkManagerMode.Host)
        {
            NetworkManager.singleton.StopHost();
        }
        else if (userMode == NetworkManagerMode.ClientOnly)
        {
            NetworkManager.singleton.StopClient();
        }
    }

    private void Update()
    {
        Debug.Log(NetworkManager.singleton.networkAddress);
    }
}
