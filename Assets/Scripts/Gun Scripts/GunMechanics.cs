using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField]
    AudioSource shootingSound;
    [SerializeField]
    AudioSource noAmmoSound;
    [SerializeField]
    AudioSource reloadingSound;
    [SerializeField]
    private int ammo;
    public int maxAmmo = 30;
    [SerializeField]
    public Text ammoText;

    

    private void Start()
    {
      

        ammo = maxAmmo;
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

        if(Input.GetKeyDown(KeyCode.R))
        {
            reloadingSound.Play();
            ammo = maxAmmo;
            ammoText.text = ammo.ToString();
            ammoText.color = new Color(0x38 / 255f, 0x17 / 255f, 0xD4 / 255f);

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
                if (ammo <= 0)
                {
                    noAmmoSound.Play();
                }
                else
                {
                    Vector3 targetPosition = hit.point;

                    GameObject bullet = Instantiate(BulletPrefab, BulletHolder.transform.position, transform.rotation);
                    shootingSound.Play();
                    ammo--;
                    UpdateAmmoText(ammo);
                    Vector3 shootingDirection = (targetPosition - BulletHolder.transform.position).normalized;

                    bullet.GetComponent<Rigidbody>().AddForce(shootingDirection * BulletSpeed);

                    Destroy(bullet, RemoveBulletInterval);
                }
            }
            
            

            yield return new WaitForSeconds(BulletSpawnInterval);
        }
    }

    private void UpdateAmmoText(int a)
    {
        if(a > 10)
        {
            ammoText.text = a.ToString();
        }
        else if(a >0)
        {
            ammoText.text = a.ToString();
            ammoText.color = Color.red;
        }
        else
        {
            ammoText.text = "RELOAD";

        }
    }
}
