using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ColorSwatch.Colors
{
	/// <summary>System.Drawing.Color の定義済み色
	/// </summary>
	public static class SystemDrawingColors
	{
		public static NamedColor[] Colors
		{
			get
			{
				if (Colors_ == null) {
					Colors_ = GenerateColors();
				}
				return Colors_;
			}
		}
		private static NamedColor[] Colors_ = null;
		private static NamedColor[] GenerateColors()
		{
			// System.Drawing.Color 以下の静的メンバを列挙する
			var target_type = typeof(System.Drawing.Color);
			var ret = target_type.GetProperties(BindingFlags.Static | BindingFlags.Public)
				.Where(x => x.PropertyType == typeof(System.Drawing.Color))
				.Select(x => new NamedColor(x.Name, (System.Drawing.Color)x.GetValue(target_type)));
			return ret.ToArray();
		}
	}
}
