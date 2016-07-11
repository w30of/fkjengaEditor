using UnityEngine;
using System.Collections;
// Use for get dynamic lists reference within code
using System.Collections.Generic; 
// Use for serializations
using System.Runtime.Serialization.Formatters.Binary; 
// Use for access file stream
using System.IO;
// Use for get exception reference within code
using System;

public static class FileManager {
	
	public static void CreateFile(string filePath,string fileName,string contentData)
	{
		// Create/Load file
		StreamWriter sw;
		FileInfo file = new FileInfo(filePath+"//"+ fileName);
		if(!file.Exists)
			sw = file.CreateText();
		else
			sw = file.AppendText();

		// Write data
		sw.WriteLine(contentData);

		// Close
		sw.Close();
		sw.Dispose();
	}

	public static ArrayList LoadFile(string filePath,string fileName)
	{
		// Load file
		StreamReader sr =null;
		try {
			sr = File.OpenText(filePath+"//"+ fileName);
		}
		catch(Exception e)
		{
			// File not found
			Debug.Log(e.ToString());
			return null;
		}

		// Package data
		string line;
		ArrayList arrlist = new ArrayList();
		while ((line = sr.ReadLine()) != null)
		{
			arrlist.Add(line);
		}

		// Close reader and return value
		sr.Close();
		sr.Dispose();
		return arrlist;
	}

	public static void DeleteFile(string path,string name)
	{
		File.Delete(path+"//"+ name);
	}
}
