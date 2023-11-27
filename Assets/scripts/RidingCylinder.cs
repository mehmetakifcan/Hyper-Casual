using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RidingCylinder : MonoBehaviour
{
    private bool _filled;
    private float _value;
    public void IncrementCylinderVolume(float value)
    {
        _value += value;
        if(_value > 1)
        {
            float letfvalue = _value - 1;
            int cylindercount = karakterkontrol.Current.cylinders.Count;
            transform.localPosition = new Vector3(transform.localPosition.x,-0.5f *(cylindercount-1)-0.25f ,transform.localPosition.z);
            transform.localScale = new Vector3(0.5f,transform.localScale.y,0.5f);
            karakterkontrol.Current.CreatCylinder(letfvalue);


        }else if (_value<0)
        {
            karakterkontrol.Current.DestroyCylinder(this);
        }
        else
        {
            int cylindercount = karakterkontrol.Current.cylinders.Count;
            transform.localPosition = new Vector3(transform.localPosition.x, -0.5f * (cylindercount - 1) - 0.25f * _value, transform.localPosition.z);
            transform.localScale = new Vector3(0.5f*_value, transform.localScale.y, 0.5f*_value);
        }
    }
}
