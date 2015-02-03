using System;
using System.Collections.Generic;
using System.Text;
namespace ConsultingLeadsPro.Helpers
{
	internal class CsvHelper
	{
		public static List<string> ParseLine(string s)
		{
			List<string> list = new List<string>();
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			bool flag2 = false;
			for (int i = 0; i < s.Length; i++)
			{
				if (!flag2 || s[i] != ',')
				{
					if (!flag && s[i] == ',')
					{
						list.Add(stringBuilder.ToString());
						stringBuilder.Remove(0, stringBuilder.Length);
					}
					else
					{
						if (!flag && s[i] == '"')
						{
							flag = true;
							flag2 = false;
						}
						else
						{
							if (flag && s[i] == '"')
							{
								if (i + 1 >= s.Length)
								{
									break;
								}
								if (s[i + 1] == '"')
								{
									stringBuilder.Append("\"");
									i++;
								}
								else
								{
									list.Add(stringBuilder.ToString());
									stringBuilder.Remove(0, stringBuilder.Length);
									flag = false;
									flag2 = true;
								}
							}
							else
							{
								stringBuilder.Append(s[i]);
							}
						}
					}
				}
			}
			if (stringBuilder.Length > 0)
			{
				list.Add(stringBuilder.ToString());
			}
			return list;
		}
	}
}
