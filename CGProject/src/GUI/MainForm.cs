using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Draw
{
	/// <summary>
	/// Върху главната форма е поставен потребителски контрол,
	/// в който се осъществява визуализацията
	/// </summary>
	public partial class MainForm : Form
	{
		/// <summary>
		/// Агрегирания диалогов процесор във формата улеснява манипулацията на модела.
		/// </summary>
		private DialogProcessor dialogProcessor = new DialogProcessor();

		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();

			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}

		/// <summary>
		/// Изход от програмата. Затваря главната форма, а с това и програмата.
		/// </summary>
		void ExitToolStripMenuItemClick(object sender, EventArgs e)
		{
			Close();
		}

		/// <summary>
		/// Събитието, което се прихваща, за да се превизуализира при изменение на модела.
		/// </summary>
		void ViewPortPaint(object sender, PaintEventArgs e)
		{
			dialogProcessor.ReDraw(sender, e);
		}

		/// <summary>
		/// Бутон, който поставя на произволно място правоъгълник със зададените размери.
		/// Променя се лентата със състоянието и се инвалидира контрола, в който визуализираме.
		/// </summary>
		void DrawRectangleSpeedButtonClick(object sender, EventArgs e)
		{
			dialogProcessor.AddRandomRectangle();

			statusBar.Items[0].Text = "Последно действие: Рисуване на правоъгълник";

			viewPort.Invalidate();
		}

		/// <summary>
		/// Прихващане на координатите при натискането на бутон на мишката и проверка (в обратен ред) дали не е
		/// щракнато върху елемент. Ако е така то той се отбелязва като селектиран и започва процес на "влачене".
		/// Промяна на статуса и инвалидиране на контрола, в който визуализираме.
		/// Реализацията се диалогът с потребителя, при който се избира "най-горния" елемент от екрана.
		/// </summary>
		void ViewPortMouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (pickUpSpeedButton.Checked)
			{
				Shape selec = dialogProcessor.ContainsPoint(e.Location);
				if (selec != null)
				{
					if (dialogProcessor.Selection.Contains(selec))
					{
						selec.FillColor = dialogProcessor.currentFillColor;
						dialogProcessor.Selection.Remove(selec);
					}
					else
					{
						dialogProcessor.Selection.Add(selec);
					}

				}
				statusBar.Items[0].Text = "Селеция на примитив.";
				dialogProcessor.IsDragging = true;
				dialogProcessor.LastLocation = e.Location;
				viewPort.Invalidate();
			}
		}

		/// <summary>
		/// Прихващане на преместването на мишката.
		/// Ако сме в режм на "влачене", то избрания елемент се транслира.
		/// </summary>
		void ViewPortMouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (dialogProcessor.IsDragging)
			{
				if (dialogProcessor.Selection != null) statusBar.Items[0].Text = "Последно действие: Влачене";
				dialogProcessor.TranslateTo(e.Location);
				viewPort.Invalidate();
			}
		}

		/// <summary>
		/// Прихващане на отпускането на бутона на мишката.
		/// Излизаме от режим "влачене".
		/// </summary>
		void ViewPortMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			dialogProcessor.IsDragging = false;
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			dialogProcessor.AddRandomEllipse();

			statusBar.Items[0].Text = "Последно действие: Рисуване на елипса";

			viewPort.Invalidate();
		}

		private void Point_Click(object sender, EventArgs e)
		{
			dialogProcessor.AddSimplePoint();

			statusBar.Items[0].Text = "Последно действие: Рисуване на точка.";

			viewPort.Invalidate();
		}

		private void Line_Click(object sender, EventArgs e)
		{
			dialogProcessor.AddSimpleLine();

			statusBar.Items[0].Text = "Последно действие: Рисуване на права.";

			viewPort.Invalidate();
		}

		private void Circle_Click(object sender, EventArgs e)
		{
			dialogProcessor.AddSimpleCircle();

			statusBar.Items[0].Text = "Последно действие: Рисуване на кръг.";

			viewPort.Invalidate();
		}

		private void BorderColor_Click(object sender, EventArgs e)
		{

			ColorDialog colorSelectDialog = new ColorDialog();
			if (colorSelectDialog.ShowDialog() == DialogResult.OK)
			{

				foreach (Shape item in dialogProcessor.Selection)
				{
					item.BorderColor = colorSelectDialog.Color;
					viewPort.Invalidate();
				}
				dialogProcessor.currentBorderColor = colorSelectDialog.Color;
				//dialogProcessor.ChangeGroupBorderColor(colorSelectDialog.Color);
				statusBar.Items[0].Text = "Последно действие: Смяна на цвят на контура.";


			}

		}

		private void FillColor_Click(object sender, EventArgs e)
		{

			ColorDialog fillShapesColorDialog = new ColorDialog();

			if (fillShapesColorDialog.ShowDialog() == DialogResult.OK)
			{

				foreach (Shape item in dialogProcessor.Selection)
				{
					item.FillColor = fillShapesColorDialog.Color;
					viewPort.Invalidate();
				}
				dialogProcessor.currentFillColor = fillShapesColorDialog.Color;
				//dialogProcessor.ChangeGroupFillColor(fillShapesColorDialog.Color);
				statusBar.Items[0].Text = "Последно действие: Смяна на цвета на запълване.";

			}
		}

		private void SelectedSapeColor_Click(object sender, EventArgs e)
		{
			ColorDialog selectedShapesColorDialog = new ColorDialog();

			if (selectedShapesColorDialog.ShowDialog() == DialogResult.OK)
			{

				foreach (Shape item in dialogProcessor.Selection)
				{
					item.FillColor = selectedShapesColorDialog.Color;
					viewPort.Invalidate();
				}
				dialogProcessor.currentSelectedColor = selectedShapesColorDialog.Color;

				statusBar.Items[0].Text = "Последно действие: Смяна на цвета на запълване при селекция.";

			}
		}



		public Button enterBTN;
		public TextBox textBox1width;
		public Label label1text;
		public Form form1border;


		private void BorderWidth_Click(object sender, EventArgs e)
		{
			// Show testDialog as a modal dialog and determine if DialogResult = OK.
			form1border = new Form();

			form1border.Text = "Enter border width";
			enterBTN = new Button();
			Button cancelBtn = new Button();
			textBox1width = new TextBox();
			label1text = new Label();
			label1text.Text = "Width(1-20): ";
			enterBTN.Text = "Set Border Width";
			cancelBtn.Text = "Cancel";
			label1text.Location = new Point(90, 80);
			textBox1width.Location = new Point(label1text.Left, label1text.Height + label1text.Top + 10);
			form1border.Controls.Add(label1text);
			form1border.Controls.Add(textBox1width);
			enterBTN.Location = new Point(textBox1width.Left, textBox1width.Height + textBox1width.Top + 10);
			cancelBtn.Location = new Point(enterBTN.Left, enterBTN.Height + enterBTN.Top + 10);
			// Set the accept button of the form to button1.
			form1border.AcceptButton = enterBTN;

			// Set the cancel button of the form to button2.
			form1border.CancelButton = cancelBtn;
			// Add enterBtn to the form.
			form1border.Controls.Add(enterBTN);
			enterBTN.DialogResult = System.Windows.Forms.DialogResult.OK;
			// Add cancelBtn to the form.
			form1border.Controls.Add(cancelBtn);
			form1border.StartPosition = FormStartPosition.CenterScreen;
			form1border.ShowDialog();

			EnterBTN_Click(sender, e);

		}

		private void EnterBTN_Click(object sender, EventArgs e)
		{

			try
			{
				if (textBox1width.Text == "")
				{
					form1border.Close();
				}
				else if ((float.Parse(textBox1width.Text) < 0) || (float.Parse(textBox1width.Text) > 20))
				{
					string message = "Enter appropriate value(1-20)!";
					string caption = "Error Detected in Input";
					MessageBoxButtons button = MessageBoxButtons.OK;
					DialogResult result;

					// Displays the MessageBox.
					result = MessageBox.Show(message, caption, button);
					if (result == System.Windows.Forms.DialogResult.OK)
					{

					}
				}
				else
				{

					dialogProcessor.currentWidth = float.Parse(textBox1width.Text);
					dialogProcessor.ChangeGroupBorderWidth(float.Parse(textBox1width.Text));
					statusBar.Items[0].Text = "Последно действие: Задаване на дебелина на контура около формата.";
					viewPort.Invalidate();
				}
			}
			catch
			{
				form1border.Close();
			}


		}

		private void deleteSelectedShapesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			dialogProcessor.DelSelectedShapes();
			statusBar.Items[0].Text = "Последно действие: Изтриване на селектираните фигури.";
			viewPort.Invalidate();
		}

		private void groupSelectedShapesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			dialogProcessor.GroupSelectedShapes();
			statusBar.Items[0].Text = "Последно действие: Групиране на избраните фигури.";
			viewPort.Invalidate();
		}

		private void unGroupSelectedShapesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			dialogProcessor.UnGroupSelectedShapes();
			statusBar.Items[0].Text = "Последно действие: Разгрупиране на избраните фигури.";
			viewPort.Invalidate();
		}

		private void SaveFile_Click(object sender, EventArgs e)
		{

			if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				dialogProcessor.SaveFile((List<Shape>)dialogProcessor.ShapeList, saveFileDialog1.FileName);
			}
			statusBar.Items[0].Text = "Последно действие: Записване на файл.";
		}

		private void saveFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
		{

		}

		private void LoadFile_Click(object sender, EventArgs e)
		{

			if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				dialogProcessor.ShapeList = (List<Shape>)dialogProcessor.LoadFile(openFileDialog1.FileName);
				viewPort.Invalidate();
			}
			statusBar.Items[0].Text = "Последно действие: Отваряне на файл.";
		}

		private void copySelectToolStripMenuItem_Click(object sender, EventArgs e)
		{
			dialogProcessor.CopySelectedShapes();
			statusBar.Items[0].Text = "Последно действие: Копиране на селектираните фигури.";
			viewPort.Invalidate();
		}

		private void pastSelectToolStripMenuItem_Click(object sender, EventArgs e)
		{
			dialogProcessor.PasteSelectedShapes();
			statusBar.Items[0].Text = "Последно действие: Поставяне на копирани фигури.";
			viewPort.Invalidate();
		}



		public Button entrRotateBtn;
		public TextBox RotatTextBox;
		public Label TextRotat;
		public Form RotatForm;

		public void RotateFormMethod(object sender, EventArgs e)
		{
			RotatForm = new Form();

			RotatForm.Text = "Enter rotate degree: ";
			entrRotateBtn = new Button();
			Button cancelRotateBtn = new Button();
			RotatTextBox = new TextBox();
			TextRotat = new Label();
			TextRotat.Text = "Degree(1-1000): ";
			entrRotateBtn.Text = "Set Rotate Radius";
			cancelRotateBtn.Text = "Cancel";
			TextRotat.Location = new Point(90, 80);
			RotatTextBox.Location = new Point(TextRotat.Left, TextRotat.Height + TextRotat.Top + 10);
			RotatForm.Controls.Add(TextRotat);
			RotatForm.Controls.Add(RotatTextBox);
			entrRotateBtn.Location = new Point(RotatTextBox.Left, RotatTextBox.Height + RotatTextBox.Top + 10);
			cancelRotateBtn.Location = new Point(entrRotateBtn.Left, entrRotateBtn.Height + entrRotateBtn.Top + 10);
			// Set the accept button of the form to button1.
			RotatForm.AcceptButton = entrRotateBtn;

			// Set the cancel button of the form to button2.
			RotatForm.CancelButton = cancelRotateBtn;
			// Add enterBtn to the form.
			RotatForm.Controls.Add(entrRotateBtn);
			entrRotateBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			// Add cancelBtn to the form.
			RotatForm.Controls.Add(cancelRotateBtn);
			RotatForm.StartPosition = FormStartPosition.CenterScreen;
			RotatForm.ShowDialog();

			enterRotateBtn_Click(sender, e);
		}


		/* private void toolStripButton2_Click(object sender, EventArgs e)
        {
		   RotateFormMethod(sender, e);
		}
		*/

		private void enterRotateBtn_Click(object sender, EventArgs e)
		{
			try
			{
				if (RotatTextBox.Text == "")
				{
					RotatForm.Close();
				}
				else if ((float.Parse(RotatTextBox.Text) < 1) || (float.Parse(RotatTextBox.Text) > 1000))
				{
					string message = "Enter appropriate value(1-1000)!";
					string caption = "Error Detected in Input";
					MessageBoxButtons button = MessageBoxButtons.OK;
					DialogResult result;

					// Displays the MessageBox.
					result = MessageBox.Show(message, caption, button);
					if (result == System.Windows.Forms.DialogResult.OK)
					{

					}
				}
				else
				{

					dialogProcessor.RotateShape(float.Parse(RotatTextBox.Text));
					statusBar.Items[0].Text = "Последно действие: Завъртане на фигура/фигури.";
					viewPort.Invalidate();
				}
			}
			catch
			{
				RotatForm.Close();
			}

		}

		private void rotateToolStripMenuItem_Click(object sender, EventArgs e)
		{
			RotateFormMethod(sender, e);
		}

		private void toolStripButton2_Click_1(object sender, EventArgs e)
		{
			RotateFormMethod(sender, e);
		}

		private void resizeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ResizeShapeMethod(sender, e);
		}

		public Button ENTRResizeBtn;
		public TextBox WidthBOX;
		public Label widthLABEL;
		public TextBox heightBOX;
		public Label heightLABEL;
		public Form resizeFORM;
		private void ResizeShapeMethod(object sender, EventArgs e)
		{
			resizeFORM = new Form();

			resizeFORM.Text = "Enter width and height: ";
			ENTRResizeBtn = new Button();
			Button cancelResizeBtn = new Button();
			WidthBOX = new TextBox();
			heightBOX = new TextBox();
			widthLABEL = new Label();
			heightLABEL = new Label();
			widthLABEL.Text = "Width(5-800): ";
			heightLABEL.Text = "Height(5-800): ";

			ENTRResizeBtn.Text = "Resize Shapes";
			cancelResizeBtn.Text = "Cancel";
			widthLABEL.Location = new Point(25, 80);
			heightLABEL.Location = new Point(widthLABEL.Left, widthLABEL.Height + widthLABEL.Top + 10);
			WidthBOX.Location = new Point(widthLABEL.Left + widthLABEL.Width + 10, widthLABEL.Top);
			heightBOX.Location = new Point(heightLABEL.Left + heightLABEL.Width + 10, heightLABEL.Top);
			resizeFORM.Controls.Add(widthLABEL);
			resizeFORM.Controls.Add(heightLABEL);
			resizeFORM.Controls.Add(WidthBOX);
			resizeFORM.Controls.Add(heightBOX);

			ENTRResizeBtn.Location = new Point(heightLABEL.Left + 80, heightBOX.Height + heightBOX.Top + 10);
			cancelResizeBtn.Location = new Point(ENTRResizeBtn.Left, ENTRResizeBtn.Height + ENTRResizeBtn.Top + 10);
			// Set the accept button of the form to button1.
			resizeFORM.AcceptButton = ENTRResizeBtn;

			// Set the cancel button of the form to button2.
			resizeFORM.CancelButton = cancelResizeBtn;
			// Add enterBtn to the form.
			resizeFORM.Controls.Add(ENTRResizeBtn);
			ENTRResizeBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			// Add cancelBtn to the form.
			resizeFORM.Controls.Add(cancelResizeBtn);
			resizeFORM.StartPosition = FormStartPosition.CenterScreen;
			resizeFORM.ShowDialog();

			enterResizeBtn_Click(sender, e);
		}

		private void enterResizeBtn_Click(object sender, EventArgs e)
		{
			try
			{
				if (WidthBOX.Text == "" || heightBOX.Text == "")
				{
					resizeFORM.Close();
				}
				else if ((float.Parse(WidthBOX.Text) < 5) || (float.Parse(WidthBOX.Text) > 800) || (float.Parse(heightBOX.Text) < 5) || (float.Parse(heightBOX.Text) > 800))
				{
					string message = "Enter appropriate values for width and height(5-800)!";
					string caption = "Error Detected in Input";
					MessageBoxButtons button = MessageBoxButtons.OK;
					DialogResult result;

					// Displays the MessageBox.
					result = MessageBox.Show(message, caption, button);
					if (result == System.Windows.Forms.DialogResult.OK)
					{

					}
				}
				else
				{

					dialogProcessor.ResizeShape(float.Parse(WidthBOX.Text), float.Parse(heightBOX.Text));
					statusBar.Items[0].Text = "Последно действие: Преоразмеряване на фигура/фигури.";
					viewPort.Invalidate();
				}
			}
			catch
			{
				resizeFORM.Close();
			}
		}

        private void CustomF_Click(object sender, EventArgs e)
        {
			dialogProcessor.AddRandomFig();

			statusBar.Items[0].Text = "Последно действие: Рисуване на правоъгълник";

			viewPort.Invalidate();
		}

        private void pickUpSpeedButton_Click(object sender, EventArgs e)
        {

        }
    }
}
