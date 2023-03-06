using Draw.src.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Draw
{
	/// <summary>
	/// Класът, който ще бъде използван при управляване на диалога.
	/// </summary>
	public class DialogProcessor : DisplayProcessor
	{
		#region Constructor
		
		public DialogProcessor()
		{
		}

		#endregion

		#region Properties

		/// <summary>
		/// Избран елемент.
		/// </summary>
		/// 

		private List<Shape> selection = new List<Shape>();
		public List<Shape> Selection
		{
			get { return selection; }
			set { selection = value; }
		}


		private List<Shape> copiedShapeList = new List<Shape>();
		public List<Shape> CopiedShapeList
		{
			get { return copiedShapeList; }
			set { copiedShapeList = value; }
		}

		/// <summary>
		/// Дали в момента диалога е в състояние на "влачене" на избрания елемент.
		/// </summary>
		private bool isDragging;
		public bool IsDragging {
			get { return isDragging; }
			set { isDragging = value; }
		}
		
		/// <summary>
		/// Последна позиция на мишката при "влачене".
		/// Използва се за определяне на вектора на транслация.
		/// </summary>
		private PointF lastLocation;
		public PointF LastLocation {
			get { return lastLocation; }
			set { lastLocation = value; }
		}

		#endregion

		/// <summary>
		/// Добавя примитив - правоъгълник на произволно място върху клиентската област.
		/// </summary>
		/// 

		public Color currentBorderColor = Color.Green;
		public Color currentFillColor = Color.Blue;
		public float currentWidth = 1;
		public Color currentSelectedColor = Color.Red;




		public void AddRandomRectangle()
		{
			Random rnd = new Random();
			int x = rnd.Next(100,1000);
			int y = rnd.Next(100,600);

			RectangleShape rect = new RectangleShape(new Rectangle(x, y, 100, 200));

			rect.FillColor = currentFillColor;
			rect.BorderColor = currentBorderColor;
			rect.Borderwidth = currentWidth;
			ShapeList.Add(rect);
		}

		public void AddRandomEllipse()
		{
			Random rnd = new Random();
			int x = rnd.Next(100, 1000);
			int y = rnd.Next(100, 600);


			EllipseShape ellipse = new EllipseShape(new Rectangle(x, y, 100, 200));

			ellipse.FillColor = currentFillColor;
			ellipse.BorderColor = currentBorderColor;
			ellipse.Borderwidth = currentWidth;
			ShapeList.Add(ellipse);

		}




		public void AddSimplePoint()
		{
			Random rnd = new Random();
			int x = rnd.Next(100, 1000);
			int y = rnd.Next(100, 600);
			PointShape point = new PointShape(new Rectangle(x, y, 3, 3));
			point.FillColor = currentFillColor;
			point.Borderwidth = currentWidth;
			ShapeList.Add(point);
		}

		public void AddSimpleLine()
		{
			Random rnd = new Random();
			int x1 = rnd.Next(100, 1000);
			int y1 = rnd.Next(100, 600);
			//int x2 = rnd.Next(100, 1000);
			//int y2 = rnd.Next(100, 600);
			//LineShape line = new LineShape(new Rectangle(x1, y1, x2, y2));
			//PointF p1 = new PointF(x1, y1);
			//PointF p2 = new PointF(x2, y2);
			LineShape line = new LineShape(new Rectangle(x1, y1, 300, 300));

			line.Borderwidth = currentWidth;
			line.FillColor = currentFillColor;
			ShapeList.Add(line);
		}



		public void AddSimpleCircle()
		{
			Random rnd = new Random();
			int x = rnd.Next(100, 1000);
			int y = rnd.Next(100, 600);

			CircleShape circle = new CircleShape(new Rectangle(x, y, 200, 200));

			circle.FillColor = currentFillColor;
			circle.BorderColor = currentBorderColor;
			circle.Borderwidth = currentWidth;
			ShapeList.Add(circle);
		}



		/* void ChangeGroupFillColor(Color color)
		{
			foreach (GroupShape shape in Selection)
			{
				shape.ChangeGroupFillColor(color);
			}
		}*/

		public void ChangeGroupBorderColor(Color color)
		{
			foreach (GroupShape shape in Selection)
			{
				shape.ChangeGroupBorderColor(color);
			}
		}

		public void ChangeGroupBorderWidth(float num)
		{
			foreach (GroupShape shape in Selection)
			{
				shape.ChangeGroupBorderWidth(num);
			}
		}



		//Serialization method
		/*	public void SerializeImage(object currentObject, string path = null)
			{

				Stream stream;
				IFormatter binaryFormatter = new BinaryFormatter();
				if (path == null)
				{
					stream = new FileStream("DrawFile.asd", FileMode.Create, FileAccess.Write, FileShare.None);
				}
				else
				{
					string preparePath = path + ".asd";
					stream = new FileStream(preparePath, FileMode.Create);

				}
				binaryFormatter.Serialize(stream, currentObject);
				stream.Close();
			}


			//Deserialization method
			public object DeSerializeImage(string path = null)
			{
				object currentObject;

				Stream stream;
				IFormatter binaryFormatter = new BinaryFormatter();
				if (path == null)
				{
					stream = new FileStream("DrawFile.asd", FileMode.Open);

				}
				else
				{
					stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None);
				}
				currentObject = binaryFormatter.Deserialize(stream);
				stream.Close();
				return currentObject;
			}
		*/

		public void SaveFile(object obj, string path = null)
		{
			Stream stream;
			IFormatter formatter = new BinaryFormatter();
			if (path == null)
			{
				stream = new FileStream("DrawFile.asd", FileMode.Create, FileAccess.Write, FileShare.None);
			}
			else
			{
				string preparePath = path + ".asd";
				stream = new FileStream(preparePath, FileMode.Create);

			}
			formatter.Serialize(stream, obj);
			stream.Close();
		}
		public object LoadFile(string path = null)
		{
			object obj;

			Stream stream;
			IFormatter binaryFormatter = new BinaryFormatter();
			if (path == null)
			{
				stream = new FileStream("DrawFile.asd", FileMode.Open);
			}
			else
			{
				stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None);
			}
			obj = binaryFormatter.Deserialize(stream);
			stream.Close();
			return obj;
		}





		//Delete Btn method
		public void DelSelectedShapes()
		{
			foreach (Shape shape in Selection)
			{
				ShapeList.Remove(shape);

			}
			Selection.Clear();
		}

		public void CopySelectedShapes()
		{
			CopiedShapeList.Clear();

			foreach (Shape shape in Selection)
			{
				CopiedShapeList.Add(shape);

			}
			//Selection.Clear();
		}

		public void PasteSelectedShapes()
		{
			foreach (Shape shape in CopiedShapeList.ToList())
			{
				var type = shape.GetType().Name.ToString();
				if (type.Equals("CircleShape"))
				{
					AddSimpleCircle();
				}
				else if (type.Equals("RectangleShape"))
				{
					AddRandomRectangle();
				}
				else if (type.Equals("EllipseShape"))
				{
					AddRandomEllipse();
				}
				else if (type.Equals("LineShape"))
				{
					AddSimpleLine();
				}
				else if (type.Equals("PointShape"))
				{
					AddSimplePoint();
				}
			}
		}

		public void RotateShape(float rotateAngle)
		{
			if (Selection.Count != 0)
			{
				foreach (var shape in Selection)
				{
					var type = shape.GetType().Name.ToString();
					if (type.Equals("GroupShape"))
					{
						shape.ChangeGroupRotate(rotateAngle);
					}
					else
					{
						shape.ShapeAngle = rotateAngle;
					}

				}
			}
		}

		public void SelectAllShapes()
		{
			foreach (Shape shape in ShapeList)
			{
				Selection.Add(shape);
			}
		}

		public void GroupSelectedShapes()
		{
			//checking if at least 2 shapes are selected
			if (Selection.Count < 2) return;

			float minimalX = 10000;
			float minimalY = 10000;
			float maximalX = -10000;
			float maximalY = -10000;
			foreach (var shape in Selection)
			{
				if (maximalX < shape.Location.X + shape.Width)
				{
					maximalX = shape.Location.X + shape.Width;
				}
				if (maximalY < shape.Location.Y + shape.Height)
				{
					maximalY = shape.Location.Y + shape.Height;
				}

				if (minimalX > shape.Location.X)
				{
					minimalX = shape.Location.X;
				}
				if (minimalY > shape.Location.Y)
				{
					minimalY = shape.Location.Y;
				}
			}

			var groupShape = new GroupShape(new RectangleF(minimalX, minimalY, maximalX - minimalX, maximalY - minimalY));
			groupShape.groupedShape = Selection;
			//Removing shapes from the ShapeList as they become one
			foreach (var shape in Selection)
			{
				ShapeList.Remove(shape);
			}

			Selection = new List<Shape>();
			ShapeList.Add(groupShape);
			Selection.Add(groupShape);

		}

		public void ResizeShape(float width, float height)
		{
			foreach (var item in Selection)

			{
				if (width != -1)
				{
					if (item.GetType().Equals(typeof(GroupShape)))
					{
						item.GroupResizeWidth(width);
					}
					else
					{
						item.Width = width;
					}
				}
				if (height != -1)
				{
					if (item.GetType().Equals(typeof(GroupShape)))
					{
						item.GroupResizeHeight(height);
					}
					else
					{
						item.Height = height;
					}
				}
			}
		}

		public void UnGroupSelectedShapes()
		{
			List<Shape> allShapesInGroup = new List<Shape>();
			foreach (GroupShape groupShape in Selection.ToList())
			{
				foreach (var shape in groupShape.groupedShape)
				{
					allShapesInGroup.Add(shape);
				}
				groupShape.groupedShape.Clear();
				ShapeList.Remove(groupShape);
				Selection.Remove(groupShape);
				foreach (var shape in allShapesInGroup)
				{
					Selection.Remove(shape);
					ShapeList.Add(shape);
				}
			}
		}







		/// <summary>
		/// Проверява дали дадена точка е в елемента.
		/// Обхожда в ред обратен на визуализацията с цел намиране на
		/// "най-горния" елемент т.е. този който виждаме под мишката.
		/// </summary>
		/// <param name="point">Указана точка</param>
		/// <returns>Елемента на изображението, на който принадлежи дадената точка.</returns>
		public Shape ContainsPoint(PointF point)
		{
			for(int i = ShapeList.Count - 1; i >= 0; i--){
				if (ShapeList[i].Contains(point)){
					ShapeList[i].FillColor = currentSelectedColor;

					return ShapeList[i];
				}	
			}
			return null;
		}
		
		/// <summary>
		/// Транслация на избраният елемент на вектор определен от <paramref name="p>p</paramref>
		/// </summary>
		/// <param name="p">Вектор на транслация.</param>
		public void TranslateTo(PointF p)
		{

			foreach (Shape shape in Selection)
			{
				var type = shape.GetType().Name.ToString();
				if (type.Equals("GroupShape"))
				{
					shape.MoveGroupedShape(p.X - lastLocation.X, p.Y - lastLocation.Y);
				}
				else
				{
					shape.Location = new PointF(shape.Location.X + p.X - lastLocation.X, shape.Location.Y + p.Y - lastLocation.Y);
				}


			}

			lastLocation = p;

		}


		public void AddRandomFig()
		{
			Random rnd = new Random();
			int x = rnd.Next(100, 1000);
			int y = rnd.Next(100, 600);

			CustomF four = new CustomF(new Rectangle(x, y, 100, 100));


			four.FillColor = Color.White;
			four.BorderColor = Color.Black;
			four.Borderwidth = 2;
			four.ShapeAngle = 90;

			ShapeList.Add(four);
		}



	}
}
