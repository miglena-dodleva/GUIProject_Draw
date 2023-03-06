using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draw.src.Model
{
    [Serializable]
    public class GroupShape : Shape
    {

		#region Constructor
		public GroupShape(RectangleF rect) : base(rect)
		{
		}

		public GroupShape(RectangleShape rectangle) : base(rectangle)
		{
		}

		#endregion
		public List<Shape> groupedShape = new List<Shape>();
		public override bool Contains(PointF point)
		{
			if (base.Contains(point))
			{
				foreach (Shape item in groupedShape)
				{
					if (item.Contains(point)) return true;
				}
				return true;
			}

			else
			{
				return false;
			}
		}


		public override void MoveGroupedShape(float dx, float dy)
		{
			base.MoveGroupedShape(dx, dy);
			foreach (var shape in groupedShape)
			{
				shape.MoveGroupedShape(dx * 2, dy * 2);
			}
		}

		public override void ChangeGroupFillColor(Color color)
		{
			base.ChangeGroupFillColor(color);
			foreach (var shape in groupedShape)
			{
				shape.FillColor = color;
			}
		}
		public override void ChangeGroupBorderColor(Color color)
		{
			base.ChangeGroupBorderColor(color);
			foreach (var shape in groupedShape)
			{
				shape.BorderColor = color;
			}
		}
		public override void ChangeGroupBorderWidth(float borderWidthShape)
		{
			base.ChangeGroupBorderWidth(borderWidthShape);
			foreach (var shape in groupedShape)
			{
				shape.Borderwidth = borderWidthShape;
			}
		}

		public override void ChangeGroupRotate(float angle)
		{
			base.ChangeGroupRotate(angle);
			foreach (var shape in groupedShape)
			{
				shape.ShapeAngle = angle;
			}
		}


		public override void GroupResizeWidth(float width)
		{
			base.GroupResizeWidth(width);
			float maxX = float.NegativeInfinity;
			float minX = float.PositiveInfinity;
			foreach (var item in groupedShape)
			{
				item.Width = width;
				if (minX > item.Location.X)
				{
					minX = item.Location.X;
				}
				if (maxX < item.Location.X + item.Width)
				{
					maxX = item.Location.X + item.Width;
				}

			}
			this.Rectangle = new RectangleF(minX, this.Rectangle.Y, maxX - minX, this.Rectangle.Height);
		}
		public override void GroupResizeHeight(float height)
		{
			base.GroupResizeHeight(height);
			float maxY = float.NegativeInfinity;
			float minY = float.PositiveInfinity;
			foreach (var item in groupedShape)
			{
				item.Height = height;
				if (minY > item.Location.Y)
				{
					minY = item.Location.Y;
				}
				if (maxY < item.Location.Y + item.Height)
				{
					maxY = item.Location.Y + item.Height;
				}

			}

			this.Rectangle = new RectangleF(this.Rectangle.X, minY, this.Rectangle.Width, maxY - minY);
		}



		/// <summary>
		/// Частта, визуализираща конкретния примитив.
		/// </summary>
		public override void DrawSelf(Graphics grfx)
		{
			base.DrawSelf(grfx);

			foreach (Shape item in groupedShape)
			{
				item.DrawSelf(grfx);
			}

		}


	}
}
