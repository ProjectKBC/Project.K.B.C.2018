using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : Bullet {
	
	protected float ElapsedTime { get; private set; }
	
	[SerializeField]
	private float indicationTime;

	protected override void Awake()
	{
		base.Awake();
		this.ElapsedTime = 0.0f;
	}

	protected override void Start()
	{
		base.Start();
        
	}

	protected override void Update()
	{
		ElapsedTime += Time.deltaTime;
		//float nowPass = Mathf.Floor(this.ElapsedTime * 10) / 10;
		base.Update();
		//this.Fan();
		if (ElapsedTime > this.indicationTime)
		{
			this.gameObject.SetActive(true);
		}
	}

	/*
	protected void Beam()
	{
		//this.transform.position += Vector3.forward * this.BulletSpeed * Time.deltaTime;
		this.transform.Translate(Vector3.forward * this.BulletSpeed * Time.deltaTime);
	}
	*/
	
}
