/****************************************************
	文件：NetSvc.cs
	作者：rabbitcocoa
	邮箱: 1085750968@qq.com
	日期：2021/01/05 17:20   	
	功能：网路服务
*****************************************************/
using PEProtocol;
using PENet;
using System.Collections.Generic;

public class MsgPack
{
    public ServerSession session;
    public GameMsg msg;
}
public class NetSvc
{
    private static NetSvc instance = null;
    public static NetSvc Instance
    {
        get
        {
            if (instance == null)
                instance = new NetSvc();
            return instance;
        }
    }

    public void Init()
    {
        PESocket<ServerSession, GameMsg> server = new PESocket<ServerSession, GameMsg>();
        server.StartAsServer(SrvCfg.srvIP, SrvCfg.srvPort);

        PECommon.Log("NetSvc Init Done.");
    }

    public static readonly string obj = "lock";

    
    //消息队列
    private Queue<MsgPack> msgPackQue = new Queue<MsgPack>();
    public void AddMsgQue(ServerSession _session,GameMsg _msg)
    {
        lock (obj)
        {
            MsgPack pack = (new MsgPack
            {
                session = _session,
                msg = _msg
            });
            msgPackQue.Enqueue(pack);
        }
    }

  public void Update()
    {
        if (msgPackQue.Count > 0)
        {
            PECommon.Log("PackCount:" + msgPackQue.Count);
            lock (obj)
            {
                MsgPack pack = msgPackQue.Dequeue();
               //TODO可优化 锁不用锁完
                HandOutMsg(pack);
            }
        }
    }

    private void HandOutMsg(MsgPack pack)
    {
        switch ((CMD)pack.msg.cmd)
        {
            case CMD.ReqLogin:
                LoginSys.Instance.ReqLogin(pack);
                break;
            case CMD.ReName:
                LoginSys.Instance.Rename(pack);
                break;
        }
    }
}

