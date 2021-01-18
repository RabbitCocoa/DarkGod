/****************************************************
   文件：NetSvc.cs
   作者：Rabbitcocoa
   邮箱: 1085750968@qq.com
   日期：2021/1/6 14:17:4
   功能：Nothing
*****************************************************/
using PEProtocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetSvc : MonoBehaviour
{
    //单例
    public static NetSvc instance = null;
    private PENet.PESocket<ClientSession, GameMsg> client = null;


    private Queue<GameMsg> msgQueue = new Queue<GameMsg>();

    ///服务初始化 
    public void InitSvc()
    {
        instance = this;

        client = new PENet.PESocket<ClientSession, GameMsg>();
        //日志输出设置
        client.SetLog(true, (string msg, int lv) =>
         {
             switch (lv)
             {
                 case 0:
                     msg = "Log:" + msg;
                     Debug.Log(msg);
                     break;
                 case 1:
                     msg = "Warn:" + msg;
                     Debug.LogWarning(msg);
                     break; 
                 case 2:
                     msg = "Error:" + msg;
                     Debug.LogError(msg);
                     break;
                 case 3:
                     msg = "Info:" + msg;
                     Debug.Log(msg);
                     break;
             }
           

         });

        client.StartAsClient(SrvCfg.srvIP, SrvCfg.srvPort);
    }


    private readonly string obj = "lock";
    //添加网络消息
    public void AddMsg(GameMsg msg)
    {
        lock (obj)
        {
            msgQueue.Enqueue(msg);
        }
    }

    private void Update()
    {
        if (msgQueue.Count > 0)
        {
            lock (obj)
            {
                GameMsg msg = msgQueue.Dequeue();
                ProcessMsg(msg);
            }
        }
    }
    //分发消息
    private void ProcessMsg(GameMsg msg)
    {
        if (msg.err != (int)ErrorCode.None)
        {
            switch ((ErrorCode)msg.err)
            {
                case ErrorCode.AcctIsOnline:
                    GameRoot.AddTips("账号已在线");
                    break;
                case ErrorCode.WrongPass:
                    GameRoot.AddTips("密码错误");
                    break;
                case ErrorCode.NameRepeat:
                    GameRoot.AddTips("用户名已存在");
                    break;
                case ErrorCode.UpdateDbError:
                    GameRoot.AddTips("网络连接异常");
                    PECommon.Log("数据库更新日常", LogType.Error);
                    break;
                case ErrorCode.ServerDataError:
                    GameRoot.AddTips("数据异常");
                    PECommon.Log("服务器数据不一致", LogType.Error);
                    break;
            }
        }else
        {
            switch ((CMD)msg.cmd)
            {
                case CMD.RspLogin:
                    LoginSyc.Instance.RspLogin (msg);
                    break;

                case CMD.ReName:
                    LoginSyc.Instance.ReName(msg);
                    break;
                case CMD.ResGuied:
                   MainCitySyc.Instance.ResGuide(msg);
                    break;

                case CMD.PshChat:
                    MainCitySyc.Instance.PshChat(msg);
                    break;

            }
        }

    }

    //发送消息
    public void SendMsg(GameMsg msg)
    {
        if (client.session != null)
        {
            client.session.SendMsg(msg);
        }
        else
        {
            GameRoot.AddTips("服务器未连接");
            InitSvc();
        }
    }
}
