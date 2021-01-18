/****************************************************
    文件：BaseData.cs
	作者：Rabbitcocoa
    邮箱: 1085750968@qq.com
    日期：2021/1/12 16:57:10
	功能：配置数据类
*****************************************************/

using UnityEngine;


public class AutoGuideCfg : BaseData<AutoGuideCfg>
{
    public int npcID; 
    public int actID; 
    public int coin; 
    public int exp;
    public string dilogArr;

}
public class MapCfg : BaseData<MapCfg>
{
    public string mapName;
    public string sceneName;
    public Vector3 mainCamPos;
    public Vector3 mainCamRote;
    public Vector3 playerBornPos;
    public Vector3 playerBornRote;

}
public class BaseData<T> 
{
    public int ID;
}