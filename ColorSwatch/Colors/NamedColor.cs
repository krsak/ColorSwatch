using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorSwatch.Colors
{
	/// <summary>名前付き色
	/// </summary>
	public class NamedColor
	{
		/// <summary>色名
		/// </summary>
		public string Name { get; private set; }
		/// <summary>色
		/// </summary>
		public System.Drawing.Color Color { get; private set; }
		/// <summary>インデックス。インデックスがないものは -1
		/// </summary>
		public int Index { get; private set; } = -1;

		public NamedColor(string name, System.Drawing.Color color)
		{
			this.Name = name;
			this.Color = color;
		}
		public NamedColor(string name, int argb)
		{
			this.Name = name;
			this.Color = System.Drawing.Color.FromArgb(argb);
		}
		public NamedColor(string name, int red, int green, int blue)
		{
			this.Name = name;
			this.Color = System.Drawing.Color.FromArgb(red, green, blue);
		}
		public NamedColor(string name, int alpha, int red, int green, int blue)
		{
			this.Name = name;
			this.Color = System.Drawing.Color.FromArgb(alpha, red, green, blue);
		}
		public NamedColor(int index, string name, System.Drawing.Color color)
		{
			this.Index = index;
			this.Name = name;
			this.Color = color;
		}
		public NamedColor(int index, string name, int argb)
		{
			this.Index = index;
			this.Name = name;
			this.Color = System.Drawing.Color.FromArgb(argb);
		}
		public NamedColor(int index, string name, int red, int green, int blue)
		{
			this.Index = index;
			this.Name = name;
			this.Color = System.Drawing.Color.FromArgb(red, green, blue);
		}
		public NamedColor(int index, string name, int alpha, int red, int green, int blue)
		{
			this.Index = index;
			this.Name = name;
			this.Color = System.Drawing.Color.FromArgb(alpha, red, green, blue);
		}

		/// <summary>"色名 (0xAARRGGBB)" の書式
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return $"{this.Name} (0x{this.Color.ToArgb():X8})";
		}
		public override bool Equals(object obj)
		{
			if (obj is NamedColor) {
				var target = (NamedColor)obj;
				return this.Name.Equals(target.Name) && this.Color.Equals(target.Color);
			}
			return false;
		}
		public override int GetHashCode()
		{
			return this.Name.GetHashCode() + this.Color.GetHashCode();
		}
	}
}
