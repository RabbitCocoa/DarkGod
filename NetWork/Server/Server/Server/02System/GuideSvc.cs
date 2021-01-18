using PEProtocol;
using System;

   public  class GuideSvc
    {
    private static GuideSvc instance = null;
    public static GuideSvc Instance
    {
        get
        {
            if (instance == null)
                instance = new GuideSvc();

            return instance;
        }
    }

    private CacheSvc cacheSvc = null;
    public void Init()
    {
        cacheSvc = CacheSvc.Instance;
    
    }

    public void     ReqGuide(MsgPack msgs)
    {
        ReqGuide data = msgs.msg.reqGuide;
        GameMsg msg = new GameMsg
        {
            cmd = (int)CMD.ResGuied
        };
        PlayerData pd = cacheSvc.GetPlayeDataBySession(msgs.session);
        AutoGuideCfg cfg=  CfgSvc.Instance.GetGuideCfg(data.guideId);
        //更新引导ID
        if (pd.guideid == data.guideId)
        {
            pd.guideid++;
            //更新玩家数据
            pd.coin += cfg.coin;
            CalcExp(pd, cfg.exp);
            if (!cacheSvc.UpdatePlayerData(pd.id,pd))
                msg.err = (int)ErrorCode.UpdateDbError;
            else
            {
                 msg.resGuide = new ResGuide
                {
                    guideId = pd.guideid,
                    coin = pd.coin,
                    exp = pd.exp,
                    lv = pd.lv
                };
            }
        }
        else {
            msg.err = (int)ErrorCode.ServerDataError;
        }



        msgs.session.SendMsg(msg);
    }

    private void CalcExp(PlayerData pd,int addExp)
    {
        int curExp = pd.exp;
        int addRestExp = addExp;
        int curtLv = pd.lv;
        while (addRestExp > 0)
        {
            int totalExp = PECommon.GetExpUpVaByLv(curtLv);
            int needExp = totalExp - curExp;

            if (needExp <= addRestExp)
            {
                curtLv++;
                curExp = 0;
                addRestExp -= needExp;
            }else
            {
                curExp += addRestExp;
                addRestExp -= addRestExp;
            }
        }
        pd.lv = curtLv;
        pd.exp = curExp;

    }
}

