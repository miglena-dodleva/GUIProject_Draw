using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draw.src.Model
{
    [Serializable]
    public class CircleShape : Shape
    {


		#region Constructor

		public CircleShape(RectangleF rect) : base(rect)
		{
		}

		public CircleShape(EllipseShape circle) : base(circle)
		{
		}

		#endregion

		/// <summary>
		/// Проверка за принадлежност на точка point към правоъгълника.
		/// В случая на правоъгълник този метод може да не бъде пренаписван, защото
		/// Реализацията съвпада с тази на абстрактния клас Shape, който проверява
		/// дали точката е в обхващащия правоъгълник на елемента (а той съвпада с
		/// елемента в този случай).
		/// </summary>
		public override bool Contains(PointF point)
		{
			if (base.Contains(point))
			{
				float a = Width / 2;
				float b = Height / 2;
				float X1 = Location.X + a;
				float Y1 = Location.Y + b;
				bool IsItOn = (Math.Pow((point.X - X1) / a, 2) + Math.Pow((point.Y - Y1) / b, 2) - 1) <= 0;

				return true;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// Частта, визуализираща конкретния примитив.
		/// </summary>
		public override void DrawSelf(Graphics grfx)
		{
			base.DrawSelf(grfx);

			//grfx.FillRectangle(new SolidBrush(Color.FromArgb(FillColor)), Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);
			//grfx.DrawRectangle(new Pen(BorderColor, Borderwidth), Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);
			grfx.FillEllipse(new SolidBrush(FillColor), new RectangleF(Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Width));
			grfx.DrawEllipse(new Pen(BorderColor, Borderwidth), new RectangleF(Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Width));

		}





	}
}
