using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretComponent : MonoBehaviour
{
    public float maxAngle = 45;
    public float health = 100;
    public Slider healthBar;
    public Vector3 vector;
    public Vector3 turretDirection;
    public Vector3 playerPos;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        turretDirection= transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = player.transform.position;

        var directionToOtherObject = playerPos - transform.position;
        var normalizedPlayerPosition = Vector3.Normalize(directionToOtherObject);

        var unitDirection = Vector3.Normalize(turretDirection);
        var differenceFromMyForwardDirection = Vector3.Dot(normalizedPlayerPosition, unitDirection);

        var angle = Mathf.Acos(differenceFromMyForwardDirection);
        var degree = angle * Mathf.Rad2Deg;

        if ((degree) > maxAngle)
        {
            Quaternion rotation = Quaternion.LookRotation(turretDirection, Vector3.up);
            transform.rotation = rotation;

        }
        else if ((degree) <= maxAngle)
        {   
            Quaternion rotation = Quaternion.LookRotation(directionToOtherObject, Vector3.up);
            transform.rotation = rotation;
        }

    }

    public void DecreaseHealth()
    {
        if(health > 0)
        {
            health -= 10;
            healthBar.value = health;
        }
    }
}
