using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ColorSwatch
{
	/// <summary>
	/// App.xaml の相互作用ロジック
	/// </summary>
	public partial class App : Application
	{
		private void Application_Startup(object sender, StartupEventArgs e)
		{
			var system_drawing_colors = Colors.SystemDrawingColors.Colors.OrderBy(x => x.Name);
			var system_windows_media_colors = Colors.SystemWindowsMediaColors.Colors.OrderBy(x => x.Name);
			var npoi_indexed_colors = Colors.NPOIIndexedColors.Colors.OrderBy(x => x.Name);

			var color_infos = new[] {
				new { Name=nameof(system_drawing_colors), Colors=system_drawing_colors, },
				new { Name=nameof(system_windows_media_colors), Colors=system_windows_media_colors, },
				new { Name=nameof(npoi_indexed_colors), Colors=npoi_indexed_colors, },
			};
			foreach(var color_info in color_infos) {
				// めんどくさいから出力先は決め打ち。
				var dir = Environment.CurrentDirectory;
				var filepath = System.IO.Path.Combine(dir, color_info.Name + ".png");
				this.OutputColorSwatch(filepath, color_info.Colors);
			}

			this.Shutdown();
		}
		/// <summary>色見本画像をファイルに保存する
		/// </summary>
		/// <param name="filePath">出力先ファイル名</param>
		/// <param name="colors">色のリスト</param>
		private void OutputColorSwatch(string filePath, IEnumerable<Colors.NamedColor> colors)
		{
			// めんどくさいからマージンとかフォントとかは決め打ち。
			int image_margin = 5;
			int row_margin = 5;
			string font_family = "Consolas";
			float font_height = 10;
			var font = new System.Drawing.Font(font_family, font_height);

			var text_argb_size = this.OutputColorSwatch_GetMaxTextSize(font, new[] { "0x00000000" });
			var text_name_size = this.OutputColorSwatch_GetMaxTextSize(font, colors.Select(x => x.Name));
			int ct_row = colors.Count();

			int row_height = (int)(text_name_size.Height);
			int w = (int)(image_margin * 2 + (row_height + row_margin * 2 + text_argb_size.Width + text_name_size.Width) * 2 + row_margin);
			int h = (int)(image_margin * 2 + row_height * ct_row + row_margin * (ct_row - 1));

			var bmp = new System.Drawing.Bitmap(w, h);
			var g = System.Drawing.Graphics.FromImage(bmp);
			// 色のイメージをつけやすくするため、背景色を白と黒でそれぞれ用意しておく
			g.Clear(System.Drawing.Color.Black);
			g.FillRectangle(System.Drawing.Brushes.White, 0, 0, w / 2, h);

			for (int pos_x_base = image_margin ; pos_x_base < w ; pos_x_base += w / 2) {
				int pos_y_base = image_margin;
				foreach (var color in colors) {
					// 色見本
					var brush = new System.Drawing.SolidBrush(color.Color);
					g.FillRectangle(brush, pos_x_base, pos_y_base, row_height, row_height);

					if (color.Color.A < 0xff) {
						// 文字が半透明だと読めないので、不透明にしておく
						brush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(0xff, color.Color));
					}
					int pos_x = pos_x_base + row_height + row_margin;
					int pos_y = pos_y_base;
					// ARGBコード
					g.DrawString($"0x{color.Color.ToArgb():X8}", font, brush, pos_x, pos_y);
					pos_x += (int)(text_argb_size.Width + row_margin);
					// 色名
					g.DrawString($"{color.Name}", font, brush, pos_x, pos_y);
					pos_y_base += (int)(row_margin + row_height);
				}
			}
			bmp.Save(filePath);
		}
		/// <summary>文字列群について、描画時のサイズが一番大きいものを取得する
		/// </summary>
		/// <param name="font">フォントサイズ</param>
		/// <param name="texts">文字列群</param>
		/// <returns></returns>
		private System.Drawing.SizeF OutputColorSwatch_GetMaxTextSize(System.Drawing.Font font, IEnumerable<string> texts)
		{
			// ダミーのグラフィックスを作成し、それで文字列の描画時の幅を計測する
			var bmp_dummy = new System.Drawing.Bitmap(1, 1);
			var g = System.Drawing.Graphics.FromImage(bmp_dummy);
			var w = 0.0f;
			var h = 0.0f;
			foreach (var text in texts) {
				var size = g.MeasureString(text, font);
				w = Math.Max(w, size.Width);
				h = Math.Max(h, size.Height);
			}
			return new System.Drawing.SizeF(w, h);
		}
	}
}
