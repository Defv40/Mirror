using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : NetworkBehaviour
{
    private bool doorOpened;
    [SerializeField] public int playersOnLevel = 2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool isKey = collision.TryGetComponent<Key>(out Key doorKey);

        if (isKey)
        {
            doorOpened = true;
            
            Destroy(doorKey.gameObject);

           
        }


        if (!doorOpened) return;

        if (collision.TryGetComponent<ControllerPlayer>(out  ControllerPlayer player) )
        {
            Destroy(player.gameObject);
            playersOnLevel--;

            if (playersOnLevel <= 0)
            {
                CmdSwitchLevel();
            }
        }

       
    }

    [Command(requiresAuthority = false)]
    private void CmdSwitchLevel()
    {
        NetworkManager.singleton.ServerChangeScene("Level 1");
    }
}

