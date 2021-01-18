/****************************************************
    文件：Constants.cs
	作者：Rabbitcocoa
    邮箱: 1085750968@qq.com
    日期：2021/1/4 15:45:53
	功能：常量配置
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TxtColor
{
    yellow,
    red,
    green,
    blue
}
public class Constants
{
    private const string ColorRed = "<color=#ff0000ff>";
    private const string ColorDefault = "<color=#FFBB51>";
    private const string ColorBlue = "<color=#00B4FFFF>";
    private const string ColorGreen = "<color=#00FF00FF>";
    private const string ColorEnd = "</color>";
    public static string Color(string str,TxtColor c)
    {
        string result = "";
        switch (c)
        {
            case TxtColor.red:
                result = ColorRed + str + ColorEnd;
                break;
            case TxtColor.yellow:
                result = ColorDefault + str + ColorEnd;
                break;
            case TxtColor.blue:
                result = ColorBlue + str + ColorEnd;
                break;
            case TxtColor.green:
                result = ColorGreen + str + ColorEnd;
                break;
        }
        return result;
    }

    //场景名称
    public const string SceneLoginStr = "LoginScene";

    //主城名字
    public const string SceneMainCity = "SceneMainCity";
    
    //音乐路径
    public const string MusicPath = "ResAudio/";

    //音乐名
    public const string LoginBgm = "bgLogin";
    public const string MainCityBgm = "bgMainCity";
    //登录音效
    public const string LoginUi = "uiLoginBtn";
    public const string UIOpenPage = "uiOpenPage";
    

    //常规UI点击音效
    public const string UIClickBtn = "uiClickBtn";
    public const string UIExtentBtn = "uiExtenBtn";
    //屏幕标准宽高
    public const int ScreenStandardWidth = 1334;
    public const int ScreenStandardHeight = 734;

    //轮盘圆点限制距离
    public const int ScreenOPDis = 90;

    //角色移动速度
    public const int PlayerModeSpeed = 8;
    public const int MonsterMoveSpeed = 4;
    //运动平滑加速度
    public const float AcceleSpeed = 5;
    //混合参数
    public const float BlendIdle = 0f;
    public const float BlendWalk = 1f;

    public const int MainCityMapID = 10000;


    //AutoGuideNPC
    public const int NPCWiseMan = 0;
    public const int NPCGeneral = 1;
    public const int NPCArtisan = 2;
    public const int NPCTrader = 3;

}
