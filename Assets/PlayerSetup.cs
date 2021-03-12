using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.Networking;

//Copyright 2021 CB
public class PlayerSetup : NetworkBehaviour
{

    [SerializeField]
    private Behaviour[] componentsToDisable;

    void Start()
    {
        if (!isLocalPlayer)
        {
            foreach (Behaviour behaviour in componentsToDisable)
            {
                if(behaviour != null)
                    behaviour.enabled = false;
            }
        }
    }
}
