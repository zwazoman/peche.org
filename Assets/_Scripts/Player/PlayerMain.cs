using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerMain : MonoBehaviour
{
    [field : SerializeField]
    public PlayerMovement Movement { get;private set; }

    [field : SerializeField]
    public PlayerLook Look { get; private set; }

    private static PlayerMain instance = null;
    public static PlayerMain Instance => instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
    }
}
