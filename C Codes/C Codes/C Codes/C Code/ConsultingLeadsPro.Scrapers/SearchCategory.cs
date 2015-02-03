using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
namespace ConsultingLeadsPro.Scrapers
{
	[Serializable]
	public class SearchCategory
	{
		protected List<SearchCategory> subCategroies = new List<SearchCategory>();
		[NonSerialized]
		public bool IsEmbedded = true;
		public string Name
		{
			get;
			set;
		}
		public string Url
		{
			get;
			set;
		}
		public string Meta
		{
			get;
			set;
		}
		public List<SearchCategory> Categories
		{
			get
			{
				return this.subCategroies;
			}
		}
		protected static void CollectGdb(List<SearchCategory> scs, StreamReader sr)
		{
			Stack<List<SearchCategory>> stack = new Stack<List<SearchCategory>>();
			int num = 0;
			List<SearchCategory> list = scs;
			while (!sr.EndOfStream)
			{
				string text = sr.ReadLine();
				int num2 = text.Length;
				text = text.TrimStart(new char[0]);
				num2 -= text.Length;
				if (num2 > num)
				{
					SearchCategory searchCategory = list.Last<SearchCategory>();
					if (searchCategory != null)
					{
						stack.Push(list);
						list = searchCategory.Categories;
					}
					num = num2;
				}
				else
				{
					if (num2 < num)
					{
						for (int i = 0; i < num - num2; i++)
						{
							if (stack.Count > 0)
							{
								list = stack.Pop();
							}
						}
						num = num2;
					}
				}
				string[] array = text.Split(new char[]
				{
					'�'
				});
				SearchCategory searchCategory2 = new SearchCategory();
				searchCategory2.Name = array[0];
				if (array.Length > 1)
				{
					searchCategory2.Url = array[1];
				}
				if (array.Length > 2)
				{
					searchCategory2.Meta = array[2];
				}
				list.Add(searchCategory2);
			}
		}
		public static List<SearchCategory> LoadGdb(Stream stream)
		{
			StreamReader sr = new StreamReader(stream);
			List<SearchCategory> list = new List<SearchCategory>();
			SearchCategory.CollectGdb(list, sr);
			return list;
		}
		public static List<SearchCategory> Load(string fileName)
		{
			List<SearchCategory> list = null;
			try
			{
				using (Stream stream = File.Open(fileName, FileMode.OpenOrCreate, FileAccess.Read))
				{
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					list = (binaryFormatter.Deserialize(stream) as List<SearchCategory>);
					stream.Close();
				}
				foreach (SearchCategory current in list)
				{
					current.IsEmbedded = false;
				}
			}
			catch
			{
				list = new List<SearchCategory>();
			}
			return list;
		}
		public static bool Save(string fileName, List<SearchCategory> data)
		{
			bool result;
			try
			{
				using (Stream stream = File.Open(fileName, FileMode.Create, FileAccess.Write))
				{
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					binaryFormatter.Serialize(stream, data);
					stream.Close();
				}
			}
			catch
			{
				result = false;
				return result;
			}
			return true;
			return result;
		}
	}
}
