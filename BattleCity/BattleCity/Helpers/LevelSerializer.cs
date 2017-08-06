using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using BattleCity.Elements;

namespace BattleCity.Helpers
{
	public class LevelSerializer
	{
		private static LevelSerializer instance;

		private DataContractSerializer serializer;
		private LevelSerializer()
		{
			serializer = new DataContractSerializer(typeof(List<DrawableElement>), new List<Type> { typeof(Field) });
		}

		public static LevelSerializer GetInstance()
		{
			if(instance == null)
			{
				instance = new LevelSerializer();
			}
			return instance;
		}

		public void SerializeLevel(String path, List<DrawableElement> levelElements)
		{
			using (FileStream fileStream = new FileStream(path, FileMode.Create))
			{
				serializer.WriteObject(fileStream, levelElements);
			}
		}

		public List<DrawableElement> DeserializeLevel(String path)
		{
			using (FileStream fileStream = new FileStream(path, FileMode.Open))
			{
				return serializer.ReadObject(fileStream) as List<DrawableElement>;
			}
		}
	}
}
