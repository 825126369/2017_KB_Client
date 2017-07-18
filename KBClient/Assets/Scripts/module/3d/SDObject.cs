using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SDObjectType
{
    Player=1,
    NPC=2,
    Monster=3,
};

public class SDObject : MonoBehaviour
{
    protected ulong serverId;
    protected virtual void Awake()
    {

    }

    protected virtual void OnEnable()
    {

    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {

    }

    protected virtual void FixedUpdate()
    {

    }

    protected virtual void OnDisable()
    {

    }

    protected virtual void OnDestroy()
    {

    }

    public ulong ServerId{ get{return serverId; }set{ serverId = value;}}
}
