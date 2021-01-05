/****************************************************
	文件：NetSvc.cs
	作者：rabbitcocoa
	邮箱: 1085750968@qq.com
	日期：2021/01/05 17:20   	
	功能：网路服务
*****************************************************/
using PEProtocol;
using PENet;
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

        PETool.LogMsg("NetSvc Init Done.");
    }
}

