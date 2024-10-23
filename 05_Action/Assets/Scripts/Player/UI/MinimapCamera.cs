using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    public float damping = 3.0f;
    public float movingSize = 10.0f;
    public float zoomSpeed = 3.5f;

    const float cameraHeight = 30.0f;
    const float defaultSize = 20.0f;

    Player player;
    Camera cam;

    Vector3 offset;

    float cameraSize;

    private void Start()
    {
        player = GameManager.Instance.Player;

        cam = GetComponent<Camera>();
        cam.orthographicSize = defaultSize;

        transform.position = player.transform.position + Vector3.up * cameraHeight;
        offset = transform.position - player.transform.position;
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = player.transform.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, damping * Time.deltaTime);

        if ((targetPosition - transform.position).sqrMagnitude > 0.05f)
        {
            cameraSize = movingSize;
        }
        else
        {
            cameraSize = defaultSize;
        }

        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, cameraSize, zoomSpeed * Time.deltaTime);
    }


}
