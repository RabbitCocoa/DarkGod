/****************************************************
	文件：LoginSys.cs
	作者：rabbitcocoa
	邮箱: 1085750968@qq.com
	日期：2021/01/05 17:21   	
	功能：登录业务
*****************************************************/


using PEProtocol;

class LoginSys
{
    private static LoginSys instance = null;
    public static LoginSys Instance
    {
        get
        {
            if (instance == null)
                instance = new LoginSys();

            return instance;
        }
    }
    private CacheSvc cacheSvc = null;
    public void Init()
    {
        PECommon.Log("LoginSyc Init Done.");
        cacheSvc = CacheSvc.Instance;
    }

    public void ReqLogin(MsgPack pack)
    {
        ReqLogin data = pack.msg.reqLogin;


        GameMsg msg = new GameMsg
        {
            cmd = (int)CMD.RspLogin,
            rspLogin = new RspLogin { }
        };
        //当前账号是否已上线
        //已上线,返回错误信息
        if (cacheSvc.IsAcctOnLine(data.acct))
        {
            msg.err = (int)ErrorCode.AcctIsOnline;
        }
        else
        {
            //未上线
            PlayerData _playerData = cacheSvc.GetPlayerData(data.acct, data.pass);
            //账号是否存在
            if (_playerData == null)
            {
                //存在 密码错误
                msg.err = (int)ErrorCode.WrongPass;
            }
            else
            {
                msg.rspLogin = new RspLogin
                {
                    playerData = _playerData
                };
                //添加缓存
                cacheSvc.AcctOnline(data.acct, pack.session, _playerData);
            }
        }
        //回应客户端

        pack.session.SendMsg(msg);
    }

    public void Rename(MsgPack pack)
    {
        Rename name = pack.msg.rename;
        GameMsg msg = new GameMsg
        {
            cmd = (int)CMD.ReName
        };

        //当前名字是否存在
        if (CacheSvc.Instance.IsNameExist(pack.msg.rename.name))
        {
            //存在  发送错误码
            msg.err =(int) ErrorCode.NameRepeat;
        }
        else
        {
            //更新缓存 以及数据库
            PlayerData playerData = cacheSvc.GetPlayeDataBySession(pack.session);
            playerData.name = name.name;

            if (cacheSvc.UpdatePlayerData(playerData.id, playerData))
            {
                //更新成功
                msg.rename = new Rename
                {
                    name = name.name
                };
            }else
            {
                //更新失败
                msg.err = (int)ErrorCode.UpdateDbError;
            }
        }
        pack.session.SendMsg(msg);

    }

    public void ClearOffData(ServerSession session)
    {
        cacheSvc.AcctOffLine(session);
    }
}

