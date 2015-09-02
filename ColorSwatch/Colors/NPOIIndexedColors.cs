using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ColorSwatch.Colors
{
	/// <summary>NPOI の定義済み色
	/// </summary>
	public static class NPOIIndexedColors
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
			// NPOI.SS.UserModel.IndexedColors 以下の静的メンバを列挙する
			var target_type = typeof(NPOI.SS.UserModel.IndexedColors);
			var ret = target_type.GetProperties(BindingFlags.Static | BindingFlags.Public)
				.Where(x => x.PropertyType == typeof(NPOI.SS.UserModel.IndexedColors))
				.Select(x =>
				{
					var color = (NPOI.SS.UserModel.IndexedColors)x.GetValue(target_type);
					return new NamedColor(color.Index, x.Name, color.RGB[0], color.RGB[1], color.RGB[2]);
				});
			if (!ret.Any()) {
				ret = target_type.GetFields(BindingFlags.Static | BindingFlags.Public)
					.Where(x => x.FieldType == typeof(NPOI.SS.UserModel.IndexedColors))
					.Select(x =>
					{
						var color = (NPOI.SS.UserModel.IndexedColors)x.GetValue(target_type);
						return new NamedColor(color.Index, x.Name, color.RGB[0], color.RGB[1], color.RGB[2]);
					});
			}
			return ret.ToArray();
		}
	}
}
