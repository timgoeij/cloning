using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float speed = 0.1f;

    private float timeSinceLastShot = 0;
    private float timeBetweenShots = 0.3f;

    private float recordTime = 5f;
    private float recordTimer = 0;

    private bool isRecording = false;
    private bool hasFireAShot = false;

    private GameObject bulletObject;
    private GameObject cloneObject;

    private RecordedClipMoment lastClipMoment;

    private List<RecordedClipMoment> clips = new List<RecordedClipMoment>();

    void Start()
    {
        bulletObject = Resources.Load("Bullet") as GameObject;
        cloneObject = Resources.Load("Clone") as GameObject;
    }

    void FixedUpdate()
    {
        Rotate();
        Move();
        Shoot();
        Record();
        hasFireAShot = false;
    }

    private void Move()
    {
        transform.position += transform.TransformDirection(Vector3.forward) * Input.GetAxis("Vertical") * speed;
    }

    private void Rotate()
    {
        Plane groundPlane = new Plane(Vector3.up, -transform.position.y);

        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitDistance;

        Quaternion targetRotation = new Quaternion();

        if (groundPlane.Raycast(mouseRay, out hitDistance))
        {
            var lookAtPosition = mouseRay.GetPoint(hitDistance);
            targetRotation = Quaternion.LookRotation(lookAtPosition - transform.position, Vector3.up);
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.1f);
    }

    private void Shoot()
    {
        if (timeSinceLastShot >= timeBetweenShots)
        {
            if (Input.GetAxis("Shoot") != 0f)
            {
                Instantiate(bulletObject, transform.position, transform.rotation);
                timeSinceLastShot = 0;
                hasFireAShot = true;
            }
        }
        
        timeSinceLastShot += Time.deltaTime;
    }

    private void Record()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isRecording)
                isRecording = true;
        }

        if (isRecording)
        {
            if (recordTimer >= recordTime)
            {
                recordTimer = 0;
                isRecording = false;
            }

            RecordedClipMoment clip = new RecordedClipMoment
            {
               bullet = bulletObject,
               position = transform.position,
               rotation = transform.eulerAngles.y,
               shot = hasFireAShot,
            };

            GetComponent<Renderer>().material.color = Color.green;
            
            CheckClips(clip);

            recordTimer += Time.deltaTime;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.blue;

            if (clips.Count > 0)
            {
                CreateClone();
            }
        }
    }

    private void CheckClips(RecordedClipMoment current)
    {
        if (clips.Count == 0)
        {
            lastClipMoment = current;
            clips.Add(current);
        }
        else
        {
            if (current.position != lastClipMoment.position || 
                current.rotation != lastClipMoment.rotation || 
                current.shot != lastClipMoment.shot)
            {
                lastClipMoment = current;
                clips.Add(current);
                hasFireAShot = false;
            }
        }
    }

    private void CreateClone()
    {
        GameObject clone = Instantiate(cloneObject);
        clone.GetComponent<Clone>().SetRecordClips(clips);
        clips.Clear();
    }
}