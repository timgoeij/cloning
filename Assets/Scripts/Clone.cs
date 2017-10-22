using System;
using System.Collections.Generic;
using UnityEngine;

public class Clone : MonoBehaviour
{
    
    private List<RecordedClipMoment> clips = new List<RecordedClipMoment>();
    private int recordingIndex = 0;
    private float speed = 0.1f;

    // Use this for initialization
    void Start()
    {

    }

    public void SetRecordClips(List<RecordedClipMoment> recordClips)
    {
        clips = recordClips;
        Debug.Log(clips.Count);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (recordingIndex > clips.Count)
        {
            Destroy(gameObject);
            return;
        }

        if (clips.Count > 0)
        {
            if (recordingIndex == 0)
            {
                transform.position = clips[recordingIndex].position;
                transform.rotation = Quaternion.Euler(0, clips[recordingIndex].rotation, 0);
            }
            else
            {
                transform.position = Vector3.Slerp(transform.position, clips[recordingIndex].position, speed);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, clips[recordingIndex].rotation, 0), speed);

                if (clips[recordingIndex].shot)
                {
                    Shoot();
                }
            }

            recordingIndex++;
        }
    }

    private void Shoot()
    {
        Instantiate(clips[recordingIndex].bullet, transform.position, transform.rotation);
    }
}

