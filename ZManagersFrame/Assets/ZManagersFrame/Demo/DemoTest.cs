using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoTest : MonoBehaviour
{
    public PlatformType mPlatformType;

    // Start is called before the first frame update
    void Start()
    {
        ManagerCenter.RegisterAllManagerEventHandle(mPlatformType);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
