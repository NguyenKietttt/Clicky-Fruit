using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXData
{
    private GameObject vfx;
    private Vector3 position;
    private float lifeTime;


    #region Properties

    public GameObject Vfx { get => vfx; set => vfx = value; }
    public Vector3 Position { get => position; set => position = value; }
    public float LifeTime { get => lifeTime; set => lifeTime = value; }

    #endregion


    public VFXData(GameObject vfx, Vector3 position, float lifeTime)
    {
        this.vfx = vfx;
        this.position = position;
        this.lifeTime = lifeTime;
    }
}

