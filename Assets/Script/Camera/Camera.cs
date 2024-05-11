using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public PlayerController player;                            //Target a suivre
    [SerializeField] float DelaiVitesse = 0.144f;        //Cree le delai pour le mouvement de camera
    public Vector2 offset = new Vector2(0, 0);

    private float fixedZ = -10f;                        //Axe en Z pour la perspective

    //--------------------------------------------------------------------------------------------------------------------------------------------

    void Update()
    {
        if (player != null)
        {
            //Position 
            Vector2 Position2D = (Vector2)player.transform.position + offset;

            //Mouvement avec delai
            Vector2 delaiPosition2D = Vector2.Lerp((Vector2)transform.position, Position2D, DelaiVitesse);

            //Mouvement en applicant egalement la distance en Z
            transform.position = new Vector3(delaiPosition2D.x, delaiPosition2D.y, fixedZ);
        }
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

}
