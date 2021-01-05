/****************************************************
	文件：LoginSys.cs
	作者：rabbitcocoa
	邮箱: 1085750968@qq.com
	日期：2021/01/05 17:21   	
	功能：登录业务
*****************************************************/

using PENet;

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
    public void Init()
    {
        PETool.LogMsg("LoginSyc Init Done");
    }
}

