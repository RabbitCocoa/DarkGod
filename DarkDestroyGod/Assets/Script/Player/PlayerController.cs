/****************************************************
    文件：PlayerController.cs
	作者：Rabbitcocoa
    邮箱: 1085750968@qq.com
    日期：2021/1/12 15:59:14
	功能：城市角色控制
*****************************************************/

using UnityEngine;

public class PlayerController : MonoBehaviour 
{
    public Animator ator;
    public CharacterController ctl;

    private Vector2 dir = Vector2.zero;
    public Vector2 Dir
    {
        get
        {
            return dir;
        }
        set
        {
            if (value == Vector2.zero)
                isMove = false;
            else
            {
                isMove = true;
            }
            dir = value;
        }
    }

    private Transform camTrans;

    private bool isMove = false;
    private Vector3 camOffset;


    public void Init()
    {
        camTrans = Camera.main.transform;
        camOffset = transform.position - camTrans.position;
    }
    private void Update()
    {

        #region Input
       /* float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector2 _dir = new Vector2(h, v).normalized;
        if (_dir != Vector2.zero)
        {
            Dir = _dir;
            SetBlend(Constants.BlendWalk);
        }
        else
        {
            Dir = Vector2.zero;
            SetBlend(Constants.BlendIdle);
        }*/
        #endregion
        if(currentBlend!=targetBlend)
           UpdateMixBlend();
        if (isMove)
        {
            //设置方向
            SetDirection();
            //移动
            SetMove();
            //相机跟随
            SetCamera();
        }
    }
    private void SetDirection()
    {
        float angle = Vector2.SignedAngle(Dir, new Vector2(0, 1))+camTrans.eulerAngles.y;
         Vector3 eulerAngels = new Vector3(0, angle, 0);
         transform.localEulerAngles = eulerAngels;
    }

    private void SetMove()
    {
        ctl.Move(transform.forward * Time.deltaTime*Constants.PlayerModeSpeed);
        
    }
    
    public void SetCamera()
    {
        if (camTrans != null)
        {
            camTrans.position = transform.position - camOffset;
        }
    }
   

    private float targetBlend;
    private float currentBlend;

    public void SetBlend(float blend)
    {
        targetBlend = blend;
    }
    private void UpdateMixBlend()
    {
        if (Mathf.Abs(currentBlend - targetBlend) < Constants.AcceleSpeed * Time.deltaTime)
        {
            currentBlend = targetBlend;
        }else if(currentBlend>targetBlend)
        {
            currentBlend -= Constants.AcceleSpeed * Time.deltaTime;
        }else
        {
            currentBlend += Constants.AcceleSpeed * Time.deltaTime;
        }
        ator.SetFloat("Blend", currentBlend);
    }
}