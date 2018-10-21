using UnityEngine;

public class Singleton<T> where T : class, new() 
{
    protected static Singleton<T> instance;
    public static Singleton<T> Instance
    {
        get
        {
            if (instance != null) { return instance; }

            instance = new Singleton<T>();

            return instance;
        }
    }

}