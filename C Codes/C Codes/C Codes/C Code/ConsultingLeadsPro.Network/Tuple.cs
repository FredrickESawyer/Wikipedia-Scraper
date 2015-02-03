using System;
namespace ConsultingLeadsPro.Network
{
	internal class Tuple<K, V>
	{
		public K Key
		{
			get;
			set;
		}
		public V Value
		{
			get;
			set;
		}
		public Tuple(K key, V value)
		{
			this.Key = key;
			this.Value = value;
		}
	}
}
