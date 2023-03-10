using System;
using System.Drawing;

namespace Draw
{
	/// <summary>
	/// Класът правоъгълник е основен примитив, който е наследник на базовия Shape.
	/// </summary>
	/// 

	[Serializable]
	public class EllipseShape : Shape
	{
		#region Constructor

		public EllipseShape(RectangleF rect) : base(rect)
		{
		}

		public EllipseShape(RectangleShape rectangle) : base(rectangle)
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
				float x1 = Location.X + a;
				float y1 = Location.Y + b;
				bool isItOn = (Math.Pow((point.X - x1) / a, 2) + Math.Pow((point.Y - y1) / b, 2) - 1) <= 0;
				return isItOn;
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

			//GraphicsState state = grfx.Save();

			//Matrix M = grfx.Transform.Clone();
			//M.Multiply(TransformationMatrixM); 

			//grfx.Transform = M;

			

			//grfx.FillRectangle(new SolidBrush(FillColor), Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);

			grfx.FillEllipse(new SolidBrush(FillColor), new RectangleF(Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height));
			grfx.DrawEllipse(new Pen(BorderColor, Borderwidth), new RectangleF(Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height));
            
			base.RotateShape(grfx);
			grfx.ResetTransform();

		}
	}
}
