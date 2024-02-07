using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public GameObject arrow;
    public Transform Target;
    public Transform arrowPoint;

    // Update is called once per frame
    void Update()
    {
        Vector2 direction =  transform.position - Target.transform.position;
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, -angle+75);
    }

    public void FireWeapon()
    {
        Instantiate(arrow, arrowPoint.position, arrowPoint.rotation);
    }
}
