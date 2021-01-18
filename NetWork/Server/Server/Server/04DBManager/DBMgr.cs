/****************************************************
	文件：DBMgr.cs
	作者：rabbitcocoa
	邮箱: 1085750968@qq.com
	日期：2021/01/07 14:32   	
	功能：数据库管理层
*****************************************************/

using System;
using MySql.Data.MySqlClient;
using PEProtocol;

public class DBMgr
    {
    private static DBMgr instance = null;
    public static DBMgr Instance
    {
        get
        {
            if (instance == null)
                instance = new DBMgr();
            return instance;
        }
    }

    private MySqlConnection conct=null;

    public void Init()
    {
        conct = new MySqlConnection("server=localhost;User Id=root;password=123456;Database=darkgod;Charset=utf8mb4");
        conct.Open();

        PECommon.Log("DBMgr Init Done");
    }

    /// <summary>
    /// 根据用户名和密码返回玩家数据
    /// </summary>
    /// <param name="acct"></param>
    /// <param name="pass"></param>
    /// <returns></returns>
    public PlayerData QueryPlayerData(string acct,string pass)
    {
        PlayerData playerData = null;
        MySqlDataReader reader = null;
        bool isNew = true;
        try
        {
            MySqlCommand cmd = new MySqlCommand("select * from account where acct=@acct", conct);
            cmd.Parameters.AddWithValue("acct", acct);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                isNew = false;
                string _pass = reader.GetString("pass");
                if (_pass.Equals(pass))
                {
                    //密码正确 
                    playerData = new PlayerData
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        lv = reader.GetInt32("lv"),
                        exp = reader.GetInt32("exp"),
                        power = reader.GetInt32("power"),
                        coin = reader.GetInt32("coin"),
                        diamond = reader.GetInt32("diamond"),

                        hp = reader.GetInt32("hp"),
                        ad = reader.GetInt32("ad"),
                        addef = reader.GetInt32("addef"),
                        ap = reader.GetInt32("ap"),
                        apdef = reader.GetInt32("apdef"),
                        pierce = reader.GetInt32("pierce"),
                        critical = reader.GetInt32("critical"),
                        dodge = reader.GetInt32("dodge"),
                        //@TOADD
                        guideid = reader.GetInt32("guideid")
                    };
                }
            }
        }
        catch(Exception e)
        {
            PECommon.Log("Query PlayerData By Acct&Pass Error" + e, LogType.Error);
        }
        finally
        {
            if (isNew)
            {
                
                //新账号,创建新的默认账号,并返回
                playerData = new PlayerData
                {
                    lv=1,
                    exp=0,
                    coin=5000,
                    diamond=100,
                    name="",
                    power=150,

                    hp=2000,
                    ad=275,
                    ap=265,
                    addef=67,
                    apdef=43,
                    dodge=7,
                    pierce=5,
                    critical=2,
                    guideid=1001
                };
            }
            if(reader!=null)
                reader.Close();
        }
        if(isNew)
           playerData.id= InsertNewAcctData(acct, pass, playerData);

        return playerData;
    }

    /// <summary>
    /// 插入玩家数据
    /// </summary>
    /// <param name="acct"></param>
    /// <param name="passwd"></param>
    /// <param name="_data"></param>
    /// <returns></returns>
    private int InsertNewAcctData(string acct,string passwd,PlayerData _data)
    {
        if (_data == null)
            return -1;
        int id=0; 
        try
        {
            MySqlCommand cmd = new MySqlCommand
                ("insert into account set acct=@acct,pass=@pass,name=@name,lv=@lv,exp=@exp,power=@power," +
                "coin=@coin,diamond=@diamond"+
                ",hp=@hp,ad=@ad,ap=@ap,addef=@addef,apdef=@apdef,dodge=@dodge,pierce=@pierce,critical=@critical"
                +",guideid=@guideid"
                , conct);

            cmd.Parameters.AddWithValue("acct", acct);
            cmd.Parameters.AddWithValue("pass", passwd);
            cmd.Parameters.AddWithValue("name", _data.name);
            cmd.Parameters.AddWithValue("lv", _data.lv);
            cmd.Parameters.AddWithValue("exp", _data.exp);
            cmd.Parameters.AddWithValue("power", _data.power);
            cmd.Parameters.AddWithValue("coin", _data.coin);
            cmd.Parameters.AddWithValue("diamond", _data.diamond);
            //TOADD
            cmd.Parameters.AddWithValue("hp", _data.hp);
            cmd.Parameters.AddWithValue("ad", _data.ad);
            cmd.Parameters.AddWithValue("addef", _data.addef);
            cmd.Parameters.AddWithValue("ap", _data.ap);
            cmd.Parameters.AddWithValue("apdef", _data.apdef);
            cmd.Parameters.AddWithValue("dodge", _data.dodge);
            cmd.Parameters.AddWithValue("pierce", _data.pierce);
            cmd.Parameters.AddWithValue("critical", _data.critical);
            cmd.Parameters.AddWithValue("guideid", _data.guideid);


            cmd.ExecuteNonQuery();
            id = (int)cmd.LastInsertedId;
        }
        catch(Exception e)
        {
            PECommon.Log("Insert PlayerData error:"+e, LogType.Error);
        }
        return id;
    }

    public bool IsNameExist(string name)
    {
        bool isExist = false;
        MySqlDataReader reader = null;
        try
        {
            MySqlCommand cmd = new MySqlCommand("select * from account where name=@name", conct);
            cmd.Parameters.AddWithValue("name", name);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                isExist = true;
            }
        }
        catch (Exception e)
        {
            PECommon.Log("Queery Name State Error" + e, LogType.Error);
        }
        finally
        {
            if (reader!=null)
            {
                reader.Close();
            }
        }

        return isExist;
    }

    /// <summary>
    /// 更新单个玩家数据
    /// </summary>
    /// <param name="id"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public bool UpdatePlayerData(int id, PlayerData data)
    {
        bool isSuccess = true;
        MySqlDataReader reader = null;
        try
        {
            MySqlCommand cmd = new MySqlCommand
                ("update account set name=@name,lv=@lv,exp=@exp,power=@power,coin=@coin,diamond=@diamond " +
                ",hp=@hp,ad=@ad,ap=@ap,addef=@addef,apdef=@apdef,dodge=@dodge,pierce=@pierce,critical=@critical " +
                ",guideid=@guideid  "+
                "where id=@id ", conct);

            cmd.Parameters.AddWithValue("name",data.name);
            cmd.Parameters.AddWithValue("lv",data.lv);
            cmd.Parameters.AddWithValue("exp",data.exp);
            cmd.Parameters.AddWithValue("power",data.power);
            cmd.Parameters.AddWithValue("coin",data.coin);
            cmd.Parameters.AddWithValue("diamond",data.diamond);
            cmd.Parameters.AddWithValue("id",data.id);

            cmd.Parameters.AddWithValue("hp", data.hp);
            cmd.Parameters.AddWithValue("ad", data.ad);
            cmd.Parameters.AddWithValue("addef", data.addef);
            cmd.Parameters.AddWithValue("ap", data.ap);
            cmd.Parameters.AddWithValue("apdef", data.apdef);
            cmd.Parameters.AddWithValue("dodge", data.dodge);
            cmd.Parameters.AddWithValue("pierce", data.pierce);
            cmd.Parameters.AddWithValue("critical", data.critical);

            cmd.Parameters.AddWithValue("guideid", data.guideid);

            reader = cmd.ExecuteReader();
           
        }
        catch (Exception e)
        {
            PECommon.Log("Update Data  Error" + e, LogType.Error);
            isSuccess = false;
        }
        finally
        {
            if (reader != null)
            {
                reader.Close();
            }
        }

        return isSuccess;
    }
}

