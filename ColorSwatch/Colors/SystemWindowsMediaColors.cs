using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ColorSwatch.Colors
{
	/// <summary>System.Windows.Media.Colors の定義済み色
	/// </summary>
	public static class SystemWindowsMediaColors
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
			// System.Windows.Media.Colors 以下の静的メンバを列挙する
			var target_type = typeof(System.Windows.Media.Colors);
			var ret = target_type.GetProperties(BindingFlags.Static | BindingFlags.Public)
				.Where(x => x.PropertyType == typeof(System.Windows.Media.Color))
				.Select(x =>
				{
					var color = (System.Windows.Media.Color)x.GetValue(target_type);
					return new NamedColor(x.Name, color.A, color.R, color.G, color.B);
				});
			return ret.ToArray();
		}
	}
}
