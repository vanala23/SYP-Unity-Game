using UnityEngine;

public class CameraBehavior: MonoBehaviour{
    public Transform target;
    public float speed = 8f;

    private void LateUpdate(){
        if(!target) return;

        Vector3 desiredPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, speed * Time.deltaTime);
    }
}