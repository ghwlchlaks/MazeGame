using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using System;

public class securityPrefs : MonoBehaviour {

	private void Start()
	{
	//	DontDestroyOnLoad (this);
	}
	private void Update()
	{
		
	}
	public static void SetString(string _key,string _value,byte[] _secret)
	{
		MD5 md5Hash = MD5.Create ();
		byte[] hashData = md5Hash.ComputeHash (System.Text.Encoding.UTF8.GetBytes (_key));
		string hashKey = System.Text.Encoding.UTF8.GetString (hashData);

		byte[] bytes = System.Text.Encoding.UTF8.GetBytes (_value);

		TripleDES des = new TripleDESCryptoServiceProvider ();
		des.Key = _secret;
		des.Mode = CipherMode.ECB;
		ICryptoTransform xform = des.CreateEncryptor ();
		byte[] encrypted = xform.TransformFinalBlock (bytes, 0, bytes.Length);

		string encrytedString = Convert.ToBase64String (encrypted);

		PlayerPrefs.SetString (hashKey, encrytedString);
		PlayerPrefs.Save ();

		Debug.Log ("Setting hashkey : "+ hashKey+"encrypted data : "+encrytedString);
	}

	public static string GetString(string _key,byte[] _secret)
	{
		MD5 md5Hash = MD5.Create ();
		byte[] hashData = md5Hash.ComputeHash (System.Text.Encoding.UTF8.GetBytes (_key));
		string hashkey = System.Text.Encoding.UTF8.GetString (hashData);

		string _value = PlayerPrefs.GetString (hashkey);
		byte[] bytes = Convert.FromBase64String (_value);

		TripleDES des = new TripleDESCryptoServiceProvider ();
		des.Key = _secret;
		des.Mode = CipherMode.ECB;
		ICryptoTransform xform = des.CreateDecryptor ();
		byte[] decrypted = xform.TransformFinalBlock (bytes, 0, bytes.Length);

		string decryptedString = System.Text.Encoding.UTF8.GetString (decrypted);

		Debug.Log ("Getstring hashKey: " + hashkey + " getData: " + _value + "decryted data : " + decryptedString);

		return decryptedString;
	}
	public static bool HasKey(string _key)
	{
		MD5 md5Hash = MD5.Create ();
		byte[] hashData = md5Hash.ComputeHash (System.Text.Encoding.UTF8.GetBytes (_key));
		string hashkey = System.Text.Encoding.UTF8.GetString (hashData);

		return PlayerPrefs.HasKey (hashkey);
	}
}
