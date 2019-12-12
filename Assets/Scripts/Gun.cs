using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    private Gun gun;

    public float damage = 5f;
    public float fireRate = 5f;
    private bool held = false;

    public GameObject barrelExit;
    public ParticleSystem muzFlash;
    public GameObject impactFlash;

    public float knockback = 30f;

    private float fireDelay = 0.5f;

    public Text shotText;

    [SerializeField]
    public int shots;

    private void Awake()
    {
        gun = this;
    }

    void Update()
    {
        if(OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger) && Time.time >= fireDelay)
        {
            fireDelay = Time.time + 0.8f / fireRate;
            Shoot();
            shots++;

            shotText.text = "Shots: " + shots.ToString();
        }

        if (OVRInput.GetDown(OVRInput.RawButton.Y))
        {
            SaveGame();
        }
        if (OVRInput.GetDown(OVRInput.RawButton.X))
        {
            LoadGame();
        }
    }

    private void Shoot()
    {
        Debug.Log("Shot fired");
        muzFlash.Play();

        RaycastHit hit;
        if (Physics.Raycast(barrelExit.transform.position, barrelExit.transform.forward, out hit))
        {
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.Damage(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * knockback);
            }

            GameObject impactGO = Instantiate(impactFlash, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 1.5f);
        }

        
    }

    public void SaveGame()
    {
        Save save = CreateSaveGameObject();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);
        file.Close();
        Debug.Log("Game Saved");

        shots = 0;
    }

    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            shots = 0;

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();
            Debug.Log("Game Loaded");

            shots = save.shots;
        }

    }

    private Save CreateSaveGameObject()
    {
        Save save = new Save();

        save.shots = shots;

        return save;
    }

    public void SaveAsJSON()
    {
        Save save = CreateSaveGameObject();
        string json = JsonUtility.ToJson(save);

        Debug.Log("Saving as JSON: " + json);
    }
}
