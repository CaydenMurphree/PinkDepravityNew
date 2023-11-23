using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMechanics : MonoBehaviour
{
    [SerializeField]
    public GameObject BulletPrefab;
    [SerializeField]
    public Transform BulletHolder;
    [SerializeField]
    public float BulletSpeed = 5000;
    public float BulletSpawnInterval = 0.3f;
    public float RemoveBulletInterval = 3.0f;
    private bool isShooting = false;

    // Offset values to position the gun in relation to the camera
    [SerializeField]
    public Vector3 gunPositionOffset = new Vector3(0.5f, -0.5f, .5f);
    [SerializeField]
    public Vector3 gunRotationOffset = new Vector3(0f, 180f, 0f);
    [SerializeField]
    public float positionLerpSpeed = 20f; 
    [SerializeField]
    public float rotationLerpSpeed = 20f;

    AudioSource shootingSound;

    private void Start()
    {
        shootingSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isShooting = true;
            StartCoroutine(ShootBullet());
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isShooting = false;
        }


        // Make the gun follow the camera's position and rotation
        Vector3 targetPosition = Camera.main.transform.position + Camera.main.transform.TransformDirection(gunPositionOffset);
        Quaternion targetRotation = Camera.main.transform.rotation * Quaternion.Euler(gunRotationOffset);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * positionLerpSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationLerpSpeed);
       
    }

    IEnumerator ShootBullet()
    {
        while (isShooting)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Vector3 targetPosition = hit.point;

                GameObject bullet = Instantiate(BulletPrefab, BulletHolder.transform.position, transform.rotation);
                shootingSound.Play();

                Vector3 shootingDirection = (targetPosition - BulletHolder.transform.position).normalized;

                bullet.GetComponent<Rigidbody>().AddForce(shootingDirection * BulletSpeed);

                Destroy(bullet, RemoveBulletInterval);
            }

            yield return new WaitForSeconds(BulletSpawnInterval);
        }
    }
}
