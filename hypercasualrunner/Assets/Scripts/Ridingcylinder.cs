using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ridingcylinder : MonoBehaviour
{
    private bool _filled;
    private float _value;

    public void CylinderControl(float value)
    {
        _value += value;
        if (_value > 1)
        {
            //silindirin boyutunu 1 yap
            //1'den ne kadar buyukse o buyuklukte bir silindir yarat
            int cylindercount = chcontroller.current.cylinders.Count;
            transform.localPosition=new Vector3(transform.localPosition.x,-0.5f*(cylindercount-1)-0.30f,transform.localPosition.z);
            transform.localScale = new Vector3(0.5f,transform.localScale.y,0.5f);
            float leftvalue = _value - 1;
            chcontroller.current.CreateCylinder(leftvalue);
        }
        else if (_value < 0)
        {
            chcontroller.current.destroCylinder(this);
            //Yok et silindiri 
        }
        else
        {
            int cylindercount = chcontroller.current.cylinders.Count;
            transform.localPosition = new Vector3(transform.localPosition.x, -0.5f * (cylindercount - 1) -0.25f * _value, transform.localPosition.z);
            transform.localScale = new Vector3(0.5f*_value, transform.localScale.y, 0.5f*_value);
            //silindirin boyutunu guncelle
        }




    }

}
