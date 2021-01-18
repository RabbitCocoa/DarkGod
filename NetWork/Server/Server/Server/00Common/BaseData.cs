using System;

public class AutoGuideCfg : BaseData<AutoGuideCfg>
{
    public int npcID;
    public int actID;
    public int coin;
    public int exp;
    public string dilogArr;

}
public class BaseData<T>
{
    public int ID;
}