using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Utility {
	public static T DeepClone<T>(T obj) {
		using (var ms = new MemoryStream()) {
			var formatter = new BinaryFormatter();
			formatter.Serialize(ms, obj);
			ms.Position = 0;

			return (T)formatter.Deserialize(ms);
		}
	}

}