using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phe_13 : Phenomenon, IFlashLight
{
    public void OnLighting()
    {
        Debug.Log("���� Ų ���·� �ٶ󺸰� �ֽ��ϴ�.(���ӿ���)");
        GameManager.instance.Died();
    }

    public void OnLighting_End()
    {
        
    }

    public void OnLighting_Start()
    {
        
    }

    // �þ߰� �����ϰ�
    // �÷��̾� �þ߰� �� ������Ʈ�� ������ �Լ� �����ؼ� GameOver��Ű�� �ɵ�?
    protected override void PhenomenonEnd()
    {
        
    }

    protected override void PhenomenonStart()
    {
        
    }

    protected override void Solution()
    {
        
    }
}
