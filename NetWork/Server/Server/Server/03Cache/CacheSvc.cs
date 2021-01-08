using PEProtocol;
using System.Collections.Generic;



    public class CacheSvc
    {
    private static CacheSvc instance=null;
    private DBMgr dB;
    public static CacheSvc Instance
    {
        get
        {
            if (instance == null)
                instance = new CacheSvc();
            return instance;
        }
    }

    public void Init()
    {
        dB = DBMgr.Instance;
        PECommon.Log("CacheSvc Init Done");
    }

    //账号与连接缓存
    private Dictionary<string , ServerSession > onlineAcctsDic = new Dictionary<string , ServerSession >();

    //连接与数据缓存
    private Dictionary<ServerSession, PlayerData> onLineSessionDic = new Dictionary<ServerSession, PlayerData>();
    public bool IsAcctOnLine(string acct)
    {
        return onlineAcctsDic.ContainsKey(acct);
    }
    /// <summary>
    /// 添加账号缓存
    /// </summary>
    /// <param name="acct"></param>
    /// <param name="session"></param>
    /// <param name="playerData"></param>
    public void AcctOnline(string acct,ServerSession session,PlayerData playerData)
    {
        onlineAcctsDic.Add(acct, session);
        //账号数据缓存
        onLineSessionDic.Add(session, playerData);
    }

    /// <summary>
    /// 根据用户名密码返回用户数据 从数据库
    /// </summary>
    /// <param name="acct"></param>
    /// <param name="pass"></param>
    /// <returns></returns>
    public PlayerData GetPlayerData(string acct,string pass)
    {
        //TODO 
        //从数据库查找账号信心

        return dB.QueryPlayerData(acct,pass);
    }

    public bool IsNameExist(string name)
    {
        return DBMgr.Instance.IsNameExist(name);
    }

    public PlayerData GetPlayeDataBySession(ServerSession session)
    {
        if(onLineSessionDic.TryGetValue(session,out PlayerData playerData)){
            return playerData;
        }else
        {
            return null;
        }
    }

    public bool UpdatePlayerData(int id,PlayerData data)
    {
        bool isSuccess = true;
        //TODO


        return dB.UpdatePlayerData(id,data);

    }
}

