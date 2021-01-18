using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

public class CfgSvc
    {
    private static CfgSvc instance = null;
    public static CfgSvc Instance
    {
        get
        {
            if (instance == null)
                instance = new CfgSvc();
            return instance;
        }
    }

    public void Init()
    {
        PECommon.Log("CfgSvc Init Done.");
        InitGuideCfg();
    }
    #region 自动引导配置
    private Dictionary<int, AutoGuideCfg> autoGuideCfgDic = new Dictionary<int, AutoGuideCfg>();
    private void InitGuideCfg()
    {
        
            XmlDocument doc = new XmlDocument();
            doc.Load(@"F:/DarkGodStudy/DarkDestroyGod/Assets/Resources/ResCfgs/guide.xml");

            XmlNodeList nodLst = doc.SelectSingleNode("root").ChildNodes;
            for (int i = 0; i < nodLst.Count; i++)
            {
                XmlElement ele = nodLst[i] as XmlElement;
                //如果ID不存在 读下一条
                if (ele.GetAttributeNode("ID") == null)
                {
                    continue;
                }
                int id = Convert.ToInt32(ele.GetAttributeNode("ID").InnerText);
                AutoGuideCfg mc = new AutoGuideCfg
                {
                    ID = id
                };
                foreach (XmlElement e in nodLst[i].ChildNodes)
                {
                    switch (e.Name)
                    {
          
                        case "coin":
                            mc.coin = int.Parse(e.InnerText);
                            break;
                        case "exp":
                            mc.exp = int.Parse(e.InnerText);
                            break;
               
                    }
                }
                autoGuideCfgDic.Add(id, mc);
            }
        }
    
    public AutoGuideCfg GetGuideCfg(int id)
    {
        AutoGuideCfg cfg = null;
        autoGuideCfgDic.TryGetValue(id, out cfg);

        return cfg;
    }
    #endregion
}

