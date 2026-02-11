using UnityEngine;

public class DrunkCameraSway : MonoBehaviour
{
    [Header("Intensity")]
    [Range(0f, 1f)] public float drunkAmount = 0.5f;

    [Header("Motion")]
    public float positionAmplitude = 0.05f;
    public float rotationAmplitude = 2f;
    public float speed = 1.2f;

    private Vector3 startPos;
    private Quaternion startRot;

    private float time;

    void Awake()
    {
        startPos = transform.localPosition;
        startRot = transform.localRotation;
    }

    void Update()
    {
        time += Time.deltaTime * speed;

        // smooth sine wobble
        float x = Mathf.Sin(time * 0.9f);
        float y = Mathf.Cos(time * 1.1f);

        Vector3 offset =
            new Vector3(x, y * 0.5f, 0f) * positionAmplitude * drunkAmount;

        Vector3 rot =
            new Vector3(y, 0f, x) * rotationAmplitude * drunkAmount;

        transform.localPosition = startPos + offset;

        transform.localRotation =
            startRot * Quaternion.Euler(rot);
    }
}
