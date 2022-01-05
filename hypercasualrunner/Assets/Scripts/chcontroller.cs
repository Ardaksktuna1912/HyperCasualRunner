using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chcontroller : MonoBehaviour
{
    public static chcontroller current;
    public float limitX;
    //public float limitx2;
    public float runningspeed;
    public float xspeed;
    private float _currentrunningspeed;

    public GameObject ridingcylenderprefab;
    public List<Ridingcylinder> cylinders;
    private bool _spawnbridge;
    public GameObject bridgepieceprefab;
    private Bridgespawner _bridgespawner;
    private float _bridgespawntime;
    void Start()
    {
        current = this;
        _currentrunningspeed = runningspeed;

    }


    void Update()
    {
        float newX = 0;
        float touchXDelta = 0;
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            touchXDelta = Input.GetTouch(0).deltaPosition.x / Screen.width;
        }
        else if (Input.GetMouseButton(0))
        {
            touchXDelta = Input.GetAxis("Mouse X");
        }
        newX = transform.position.x + xspeed * touchXDelta * Time.deltaTime;
        //newX = Mathf.Clamp(newX, 1f, 3f);

        Vector3 mov = new Vector3(newX, transform.position.y, transform.position.z + _currentrunningspeed * Time.deltaTime);
        transform.position = mov;
        if (_spawnbridge)
        {
            _bridgespawntime -= Time.deltaTime;
            if (_bridgespawntime < 0)
            {
                _bridgespawntime = 0.1f;
                Incrementvolume(-0.01f);
                GameObject piecebridge = Instantiate(bridgepieceprefab);
                Vector3 direction = _bridgespawner.endref.transform.position - _bridgespawner.startref.transform.position;
                float distance = direction.magnitude;
                direction = direction.normalized;

                piecebridge.transform.forward = direction;
                float characterdistance = transform.position.z - _bridgespawner.startref.transform.position.z;
                characterdistance = Mathf.Clamp(characterdistance, 0, distance);
                Vector3 newpieceedition = _bridgespawner.startref.transform.position + direction * characterdistance;
                newpieceedition.x = transform.position.x+9.89f;
                piecebridge.transform.position = newpieceedition;

               
            }
        }
        

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Cylinder")
        {
            Incrementvolume(0.1f);
            Destroy(other.gameObject);
        }
        else if (other.tag == "SpawnBridge")
        {
            startbridgespawn(other.transform.parent.GetComponent<Bridgespawner>());
        }
        else if (other.tag == "StopSpawnBridge")
        {
            stopbridgespawn();
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Tuzak")
        {
            Incrementvolume(-Time.fixedDeltaTime);
        }
    }
    public void Incrementvolume(float value)
    {
        if (cylinders.Count == 0)
        {
            if (value > 0)
            {
                CreateCylinder(value);
            }
            else
            {
               //gameover
            }


        }
        else
        {
            cylinders[cylinders.Count - 1].CylinderControl(value);
        }

    }

    public void CreateCylinder(float value)
    {
        Ridingcylinder createdcylinder = Instantiate(ridingcylenderprefab, transform).GetComponent<Ridingcylinder>();
        cylinders.Add(createdcylinder);
        createdcylinder.CylinderControl(value);
    }

    public void destroCylinder(Ridingcylinder cylinder)
    {
        cylinders.Remove(cylinder);
        Destroy(cylinder.gameObject);
    }

    public void startbridgespawn(Bridgespawner spawnerbridge)
    {
        _bridgespawner = spawnerbridge;
        _spawnbridge = true;
    }
    public void stopbridgespawn()
    {
        _spawnbridge = false;
    }
}
