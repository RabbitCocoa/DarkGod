﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    public class ServerRoot
    {
    private static ServerRoot instance = null;
    public static ServerRoot Instance
    {
        get
        {
            if (instance == null)
                instance = new ServerRoot();
            return instance;
        }
    }

    public void Init()
    {
        //数据库层TODO 
        DBMgr.Instance.Init();

        //服务层
        NetSvc.Instance.Init();
        CacheSvc.Instance.Init();
        //业务系统层
        LoginSys.Instance.Init();


    }

    public void Update()
    {
        NetSvc.Instance.Update();
    }
   }

