using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Needle : Bullet
{

    //protected  Vector3 Test = new Vector3(-42.5f, 30, 100);


    protected override void Awake()
    {
        base.Awake();
        
    }

    protected override void Start()
    {
        base.Start();
        
    }

    protected override void Update()
    {
        base.Update();
        this.Fan();
    }

    protected void Fan()
    {
        //this.transform.position += Vector3.forward * this.BulletSpeed * Time.deltaTime;
        this.transform.Translate(Vector3.forward * this.BulletSpeed * Time.deltaTime);
    }
    
}