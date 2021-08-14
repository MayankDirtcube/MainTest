using UnityEngine;
using UnityEditor;
using System.Reflection;

public class FTG_AssemblyReferences : MonoBehaviour {

	public static bool IsClassActive(string className)
	{	
		System.Type oneSignal = FindClass(className);

		return oneSignal != null;
	}

	public static System.Type FindClass(string className, string nameSpace = null, string assemblyName = null)
	{
		Assembly[] assemblies = System.AppDomain.CurrentDomain.GetAssemblies ();

		foreach (Assembly asm in assemblies) {
			if (!string.IsNullOrEmpty (assemblyName) && !asm.GetName ().Name.Equals (assemblyName)) {
				continue;
			}

			System.Type[] types = asm.GetTypes ();
			foreach (System.Type t in types) {
				if (t.IsClass && t.Name.Equals (className) && (string.IsNullOrEmpty (nameSpace) || nameSpace.Equals (t.Namespace))) {
					return t;
				}
			}
		}
		return null;
	}

}
