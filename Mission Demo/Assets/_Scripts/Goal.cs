using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Goal : MonoBehaviour
{
   static public bool goalMet = false;

    private void OnTriggerEnter(Collider other)
    {
        Projectile proj = other.GetComponent<Projectile>();
        if (proj != null)
        {
            goalMet = true;
            Material mat = GetComponent<Renderer>().material;
            Color color = mat.color;
            color.a = 0.95f;
            mat.color = color;
        }
    }


}
