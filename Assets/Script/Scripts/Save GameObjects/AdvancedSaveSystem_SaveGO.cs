using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
using System;
using LitJson;
using System.Security.Cryptography;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

public class AdvancedSaveSystem_SaveGO : MonoBehaviour {
    public string specialPath = "%appdata%"; //Use : %SystemFolderName%
    public string folderName = "Advanced Save System";
	public string keyWord = "[Saveable]";
   
    public string[] gameObjectIDs;
    public GameObject[] gameObjectPrefabs;
    int tmp = 0;
    static string PasswordHash;
    static string SaltKey;
    static string VIKey;
    void Start()
    {
        PasswordHash = transform.GetComponent<AdvancedSaveSystem_EncryptDetails>().PasswordHash;
        SaltKey = transform.GetComponent<AdvancedSaveSystem_EncryptDetails>().SaltKey;
        VIKey = transform.GetComponent<AdvancedSaveSystem_EncryptDetails>().VIKey;
    }
	
    public void SaveGameObjects(int slot)
    {
        try
        {
            DatosGuardado MisDatos = new DatosGuardado(gameObject.transform.position.x.ToString(),
                                       gameObject.transform.position.y.ToString()
                                       , gameObject.transform.position.z.ToString(), gameObject.activeSelf.ToString());
            
            print(JsonMapper.ToJson(MisDatos));
            Debug.Log(Application.persistentDataPath);
            BinaryFormatter fb = new BinaryFormatter();
            string path = Application.persistentDataPath;
            FileStream expedient = File.Create(Application.persistentDataPath + "/Datos.json");
            //FileStream expedient = File.Create(Application.persistentDataPath + "/Datos.d");
            foreach (string x in gameObjectIDs)
            {
               if (Directory.Exists(path + "\\" + x))
               {
                   Directory.Delete(path + "\\" + x,true);
                   Directory.CreateDirectory(path + "\\" + x);
               }
               else
               {
                   Directory.CreateDirectory(path + "\\" + x);
               }
            }
            foreach (string x in gameObjectIDs)
            {
				object[] allObjects = FindObjectsOfType (typeof(GameObject));
				foreach (GameObject obj in allObjects)
				{
				if (obj.name.Contains (keyWord))
					{
						if (obj.GetComponent<GameObjectID>().ID == x)
						{
							tmp = UnityEngine.Random.Range(111111, 999999);
							string toSave = "";
                            toSave = MisDatos.ToString();
                            File.WriteAllText(path + "\\" + x + "\\item" + tmp.ToString(), Encrypt(toSave));
                            
                            AdvancedSaveSystem_FileEncryptor.EncryptFile(path + toSave, path + toSave+".nc");
                            fb.Serialize(expedient, JsonMapper.ToJson(MisDatos).ToString());
                            //File.WriteAllText(Application.persistentDataPath + "/Datos.json", JsonMapper.ToJson(toSave).ToString());
                            /*Datos.text.Add(toSave);
                            fb.Serialize(expedient, Datos);*/
                        }
					}
				}
            }
            
            AdvancedSaveSystem_MessageSystem.msg = "Gameobjects Saved!";
            expedient.Close();
        }
        catch { }
    }


    public static string Encrypt(string plainText)
    {
        byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
        

        byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
        var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
        var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

        byte[] cipherTextBytes;

        using (var memoryStream = new MemoryStream())
        {
            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
            {
                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                cryptoStream.FlushFinalBlock();
                cipherTextBytes = memoryStream.ToArray();
                cryptoStream.Close();
            }
            memoryStream.Close();
        }
        return Convert.ToBase64String(cipherTextBytes);
    }
    public static string Decrypt(string encryptedText)
    {
        byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
        byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
        var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

        var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
        var memoryStream = new MemoryStream(cipherTextBytes);
        var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
        byte[] plainTextBytes = new byte[cipherTextBytes.Length];

        int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
        memoryStream.Close();
        cryptoStream.Close();
        return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
    }
}

[System.Serializable]
public class DatosGuardado : System.Object
{

    public string x;
    public string y;
    public string z;
    public string activo;


    public DatosGuardado(string x1, string y1, string z1, string activo2)
    {
        x = x1;
        y = y1;
        z = z1;
        activo = activo2;
    }
}
