using System;
using UnityEngine;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

public static class ManagerCenter
{
    public static PlatformType mPlatformType;
    //public static Dictionary<ManagerType, MonManager> mRegisteredManagers = new Dictionary<ManagerType, MonManager>();
    public static Dictionary<ManagerType, BaseManagerEventHandle> mRegisteredManagerEvent = new Dictionary<ManagerType, BaseManagerEventHandle>();


    /// <summary>
    /// 注册所有该平台的Manager事件处理
    /// </summary>
    /// <param name="platformType">平台类型</param>
    public static void RegisterAllManagerEventHandle(PlatformType platformType)
    {
        mPlatformType = platformType;
        RegisterManagerEventHandle<BaseManagerEventHandle>();
    }


    /// <summary>
    /// 取消注册的Manager事件
    /// </summary>
    public static void UnRegisterAllManagerEventHandle()
    {
        foreach (var item in mRegisteredManagerEvent.Values)
        {
            item.UnRegisterEventHandle();
        }
        mRegisteredManagerEvent.Clear();
    }

    /// <summary>
    /// 注册该平台的特定Manager事件处理
    /// </summary>
    /// <param name="platformType">平台类型</param>
    /// <param name="baseManagerEventHandle"></param>
    public static void RegisterPlatformTypeManagerEventHandle(PlatformType platformType, BaseManagerEventHandle baseManagerEventHandle)
    {
        //Debug.LogError($"stARview : ManagerCenter---RegisterManagerEventHandle---managerType:{baseManagerEventHandle.manager.managerType.ToString()}---platformType:{baseManagerEventHandle.platformType.ToString()}");
        if (baseManagerEventHandle.platformType.Equals(mPlatformType))
        {
            //Debug.LogError($"stARview : ManagerCenter---RegisterManagerEventHandle---success---managerType:{baseManagerEventHandle.manager.managerType.ToString()}---platformType:{baseManagerEventHandle.platformType.ToString()}");
            RegisterManagerEventHandle(baseManagerEventHandle);
        }
    }


    private static void RegisterManagerEventHandle<T>() where T : BaseManagerEventHandle
    {
        foreach (var item in GetType<T>())
        {
            RegisterPlatformTypeManagerEventHandle(mPlatformType, item);
        }
    }



    private static void RegisterManagerEventHandle(BaseManagerEventHandle baseManagerEventHandle)
    {
        if (mRegisteredManagerEvent.ContainsKey(baseManagerEventHandle.manager.managerType))
        {
            baseManagerEventHandle.UnRegisterEventHandle();
            mRegisteredManagerEvent[baseManagerEventHandle.manager.managerType] = baseManagerEventHandle;
        }
        else
        {
            mRegisteredManagerEvent.Add(baseManagerEventHandle.manager.managerType, baseManagerEventHandle);
        }
        baseManagerEventHandle.RegisterEventHandle();
    }


    private static void UnRegisterManagerEventHandle(BaseManagerEventHandle baseManagerEventHandle)
    {
        if (mRegisteredManagerEvent.ContainsKey(baseManagerEventHandle.manager.managerType))
        {
            baseManagerEventHandle.UnRegisterEventHandle();
            mRegisteredManagerEvent.Remove(baseManagerEventHandle.manager.managerType);
        }
    }


    public static List<T> GetType<T>() where T : BaseManagerEventHandle
    {
        var types = Assembly.GetExecutingAssembly().GetTypes();
        var cType = typeof(T);
        List<T> cList = new List<T>();

        foreach (var type in types)
        {
            var baseType = type.BaseType;  //获取基类
            while (baseType != null)  //获取所有基类
            {
               // Debug.Log(baseType.Name);
                if (baseType.Name == cType.Name)
                {
                    //Debug.Log(baseType.Name);
                    Type objtype = Type.GetType(type.FullName, true);
                    object obj;
                    try
                    {
                        obj = Activator.CreateInstance(objtype);
                    }
                    catch (Exception e)
                    {
                        //Debug.Log(e);
                        throw;
                    }

                    if (obj != null)
                    {
                        T info = obj as T;
                        cList.Add(info);
                       // Debug.Log(info.GetType().Name);
                    }
                    break;
                }
                else
                {
                    baseType = baseType.BaseType;
                }
            }
        }
        return cList;
    }


    ///// <summary>
    ///// 记录注册Manager
    ///// </summary>
    ///// <param name="manager"></param>
    ///// <returns></returns>
    //public static MonManager RegisterManagerFactory(MonManager manager)
    //{
    //    if (mRegisteredManagers.ContainsKey(manager.managerType))
    //    {
    //        mRegisteredManagers[manager.managerType] = manager;
    //    }
    //    else
    //    {
    //        mRegisteredManagers.Add(manager.managerType, manager);
    //    }
    //    return manager;
    //}
}
