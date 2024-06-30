using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Pun.UtilityScripts;
public class Weapon : MonoBehaviour
{
    public Camera PlayerCamera;
    Animator _anim;
    public TextMeshProUGUI AmmoManagerDisplay;
    #region Audios
    private AudioSource source;
    #endregion
    #region Shooting
    //ate� etmek i�in olan de�i�kenler
    public bool IsShooting, ReadyToShoot;
    bool _allowReset = true;
    bool isAiming = false;
    public float ShootingDelay;
    public GameObject MuzzleEffect;
    #endregion
    #region Burst
    //Burst atmak i�in olan de�i�kenler
    public int BulletsPerBurst;
    public int BurstBulletLeft;
    #endregion
    #region Spread
    //Spray
    public float SpreadIntensity;
    #endregion
    #region Bullets
    //mermiler ve onlarla alakal� de�i�kenler
    public GameObject BulletsPrefab;
    public Transform BulletSpawn;
    public float BulletVelocity;
    public float BulletDamage;
    #endregion
    #region Reload
    public AnimationClip ReloadAnim;
    public float ReloadTime;//yeniden doldurma s�resi
    public int MagazineSize, BulletsLeft, MaxBulletsLeft;//�arj�r kapasitesi, kalan mermi say�s� ve silah�n alabilece�i max mermi
    public bool isReloading;
    #endregion
    public enum ShootingMode
    {
        Single,
        Burst,
        Auto
    }

    public ShootingMode CurrentShootingMode;

    private void Start()
    {
        ReadyToShoot = true;
        BurstBulletLeft = BulletsPerBurst;
        source = GetComponent<AudioSource>();
        _anim = GetComponent<Animator>();
        _anim.SetBool("isReloading", false);
        ReloadTime = ReloadAnim.length;
    }
    void Update()
    {

        //C ye bas�nca silah modunu de�i�tiriyor
        if (Input.GetKeyDown(KeyCode.C))
        {
            source.Play();
            if (CurrentShootingMode == ShootingMode.Auto)
            {
                CurrentShootingMode = ShootingMode.Single;
            }
            else
            {
                CurrentShootingMode = ShootingMode.Auto;
            }
        }
        if (CurrentShootingMode == ShootingMode.Auto)
        {
            //bas�l� tuttuk�a ate� et
            IsShooting = Input.GetMouseButton(0);
        }
        else if (CurrentShootingMode == ShootingMode.Single || CurrentShootingMode == ShootingMode.Burst)
        {
            //bir kere bas�nca ate� et
            IsShooting = Input.GetMouseButtonDown(0);
        }
        //R'ye bas�nca reload atacak
        if (Input.GetKeyDown(KeyCode.R) && isReloading == false && MagazineSize > 0 && BulletsLeft != 30)
        {
            Reload();
        }
        //otomatik reload atacak
        if (Input.GetMouseButton(0) && ReadyToShoot && isReloading == false && BulletsLeft <= 0 && MagazineSize > 0)
        {
            Reload();
        }
        //mermi yok ise bo�a ate� etme sesi �al��acak
        if (Input.GetMouseButton(0) && BulletsLeft <= 0 && isReloading == false)
        {
            //ses dosyas� ile bo� ses eklenecek ve �alm�yor ise oynat�lacak
            Debug.Log("Tik sesi gelecek");
        }
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Aim e girdi");
            if (isAiming)
            {
                BulletSpawn.transform.localPosition = new Vector3(0.123f, 1.539f, 0.991f);
                Debug.Log("Aim 1");
                isAiming = false;
                _anim.SetBool("Aim", false);
            }
            else
            {
                BulletSpawn.transform.localPosition = new Vector3(0f, 1.539f, 0.991f);
                Debug.Log("Aim 2");
                isAiming = true;
                _anim.SetBool("Aim", true);
            }
        }
        if (ReadyToShoot && IsShooting && BulletsLeft > 0 && isReloading == false)
        {
            BurstBulletLeft = BulletsPerBurst;
            _anim.SetBool("Shoot", true);
            FireWeapon();
        }
        else
        {
            _anim.SetBool("Shoot", false);
        }
        if (AmmoManagerDisplay != null)
        {
            AmmoManagerDisplay.text = $"{BulletsLeft}/{MagazineSize}";
        }

    }

    private void FireWeapon()
    {
        BulletsLeft -= 1;

        PlayMuzzle();

        ReadyToShoot = false;

        Vector3 shootingDirection = CalculateDirectionAndSpread().normalized;

        //mermiler spawnlan�yor
        var bullet = PhotonNetwork.Instantiate(BulletsPrefab.name, BulletSpawn.position, Quaternion.identity);

        bullet.GetComponent<Bullet>().damage = BulletDamage;

        bullet.GetComponent<Bullet>().parent = this;

        bullet.transform.forward = shootingDirection;

        //mermilere g�� ekleniyor
        bullet.GetComponent<Rigidbody>().AddForce(shootingDirection * BulletVelocity, ForceMode.Impulse);

        //ate� etmeyi b�rakt���m�z� kontrol ediyor
        if (_allowReset)
        {
            Invoke("ResetShot", ShootingDelay);
            _allowReset = false;
        }

        //BurstMode
        if (CurrentShootingMode == ShootingMode.Burst && BurstBulletLeft > 1)
        {
            BurstBulletLeft--;
            Invoke("FireWeapon", ShootingDelay);
        }

    }
    public void PlayMuzzle()
    {
        //MuzzleEffect.GetComponent<ParticleSystem>().Play();
    }
    public void Reload()//reload � ba�lat�r
    {
        isReloading = true;
        _anim.SetBool("isReloading", true);
        _anim.SetTrigger("Recharge");
        Invoke("ReloadComplated", ReloadTime);
    }

    private void ReloadComplated()//reload bitti�inde yap�lacak i�lemler
    {
        int missingBullet = MaxBulletsLeft - BulletsLeft; //azalan mermi say�s�n� al�yor
        if (MagazineSize >= MaxBulletsLeft)//�arj�rde olan mermi silah�n max kapasitesinden fazla ise
        {
            BulletsLeft = MaxBulletsLeft;//kalan mermi fullenir
            MagazineSize -= missingBullet;//i�lemlerden sonra �arj�rdeki mermi eksilen mermiden ��kar�larak yeni mermi say�s� bulunur
        }
        else
        {
            if (MagazineSize >= missingBullet)//�arj�r�m�zdeki mermi azalan mermiden fazla ise
            {
                MagazineSize -= missingBullet;//i�lemlerden sonra �arj�rdeki mermi eksilen mermiden ��kar�larak yeni mermi say�s� bulunur
                BulletsLeft = MaxBulletsLeft;//kalan mermi fullenir 
            }
            else//�arj�r�m�zdeki mermi azalan mermiden az ise �rnek 10/5 mermimiz kald� ise
            {
                BulletsLeft += MagazineSize;//kalan mermimimize �arj�r�m�zdeki say� eklenir 
                MagazineSize -= missingBullet;//i�lemlerden sonra �arj�rdeki mermi eksilen mermiden ��kar�larak yeni mermi say�s� bulunur
            }
        }
        if (MagazineSize <= 0)//- mermiye d��mememiz i�in 0 lan�r
        {
            MagazineSize = 0;
        }
        isReloading = false;
        _anim.SetBool("isReloading", false);
    }

    public void ResetShot()
    {
        ReadyToShoot = true;
        _allowReset = true;
    }

    private Vector3 CalculateDirectionAndSpread()
    {
        //Ekran�n ortas�ndan raycast ��kartarak nereyi ni�an ald���m�z� kontrol ediyor
        Ray ray = PlayerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
        {
            //bir �eyi vurduk
            targetPoint = hit.point;
        }
        else
        {
            //bo�lu�a s�kt�k
            targetPoint = ray.GetPoint(100);
        }

        Vector3 direction = targetPoint - BulletSpawn.position;

        float x = UnityEngine.Random.Range(-SpreadIntensity, SpreadIntensity);
        float y = UnityEngine.Random.Range(-SpreadIntensity, SpreadIntensity);

        return direction + new Vector3(x, y, 0);

    }
}
