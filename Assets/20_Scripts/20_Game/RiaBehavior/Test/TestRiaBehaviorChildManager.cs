using System.Collections;
using UnityEngine;

public class TestRiaBehaviorChildManager : RiaBehaviorChildManager<TestRiaBehavior>
{
    private float shotElapsedTime = 0;

    protected override void OnAwake()
    {
    }

    protected override void OnUpdate()
    {
        shotElapsedTime += Time.deltaTime;

        ShotBehabior(0.05f/3);
    }

    private void ShotBehabior(float _span)
    {
        if (_span <= shotElapsedTime)
        {
            OneShot();
            //ShotBehabiorAll();

            shotElapsedTime = 0;
        }
    }

    private void OneShot()
    {
        for (int i = 0; i < Behaviors.Length; ++i)
        {
            if (!Behaviors[i].Alive)
            {
                Behaviors[i].WakeUp(Vector3.zero, Quaternion.identity);
                break;
            }
        }
    }

    private void ShotBehabiorAll()
    {
        for (int i = 0; i < Behaviors.Length; ++i)
        {
            if (!Behaviors[i].Alive)
            {
                Behaviors[i].WakeUp(Vector3.zero, Quaternion.identity);
            }
        }
    }

}