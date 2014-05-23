namespace BNTU_project
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.параметрыРасчетаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.параметрыМашиныToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.параметрыШестерниToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.параметрыДифференциалаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.параметрыРаздаточнойКоробкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выборКинематическоСхемыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.проверочныйРасчетToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.помощьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.нагрузочныйРежимToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(253, 288);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(232, 50);
            this.button1.TabIndex = 0;
            this.button1.Text = "Посчитать";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.параметрыРасчетаToolStripMenuItem,
            this.проверочныйРасчетToolStripMenuItem,
            this.помощьToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(754, 28);
            this.menuStrip1.TabIndex = 25;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.выходToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(57, 24);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(122, 24);
            this.выходToolStripMenuItem.Text = "Выход";
            // 
            // параметрыРасчетаToolStripMenuItem
            // 
            this.параметрыРасчетаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.параметрыМашиныToolStripMenuItem,
            this.параметрыШестерниToolStripMenuItem,
            this.параметрыДифференциалаToolStripMenuItem,
            this.параметрыРаздаточнойКоробкиToolStripMenuItem,
            this.выборКинематическоСхемыToolStripMenuItem});
            this.параметрыРасчетаToolStripMenuItem.Name = "параметрыРасчетаToolStripMenuItem";
            this.параметрыРасчетаToolStripMenuItem.Size = new System.Drawing.Size(102, 24);
            this.параметрыРасчетаToolStripMenuItem.Text = "Параметры";
            // 
            // параметрыМашиныToolStripMenuItem
            // 
            this.параметрыМашиныToolStripMenuItem.Name = "параметрыМашиныToolStripMenuItem";
            this.параметрыМашиныToolStripMenuItem.Size = new System.Drawing.Size(314, 24);
            this.параметрыМашиныToolStripMenuItem.Text = "параметры машины";
            this.параметрыМашиныToolStripMenuItem.Click += new System.EventHandler(this.параметрыМашиныToolStripMenuItem_Click);
            // 
            // параметрыШестерниToolStripMenuItem
            // 
            this.параметрыШестерниToolStripMenuItem.Name = "параметрыШестерниToolStripMenuItem";
            this.параметрыШестерниToolStripMenuItem.Size = new System.Drawing.Size(314, 24);
            this.параметрыШестерниToolStripMenuItem.Text = "параметры зубчатого колеса";
            this.параметрыШестерниToolStripMenuItem.Click += new System.EventHandler(this.параметрыШестерниToolStripMenuItem_Click);
            // 
            // параметрыДифференциалаToolStripMenuItem
            // 
            this.параметрыДифференциалаToolStripMenuItem.Name = "параметрыДифференциалаToolStripMenuItem";
            this.параметрыДифференциалаToolStripMenuItem.Size = new System.Drawing.Size(314, 24);
            this.параметрыДифференциалаToolStripMenuItem.Text = "параметры дифференциала";
            this.параметрыДифференциалаToolStripMenuItem.Click += new System.EventHandler(this.параметрыДифференциалаToolStripMenuItem_Click);
            // 
            // параметрыРаздаточнойКоробкиToolStripMenuItem
            // 
            this.параметрыРаздаточнойКоробкиToolStripMenuItem.Name = "параметрыРаздаточнойКоробкиToolStripMenuItem";
            this.параметрыРаздаточнойКоробкиToolStripMenuItem.Size = new System.Drawing.Size(314, 24);
            this.параметрыРаздаточнойКоробкиToolStripMenuItem.Text = "параметры раздаточной коробки";
            this.параметрыРаздаточнойКоробкиToolStripMenuItem.Click += new System.EventHandler(this.параметрыРаздаточнойКоробкиToolStripMenuItem_Click);
            // 
            // выборКинематическоСхемыToolStripMenuItem
            // 
            this.выборКинематическоСхемыToolStripMenuItem.Name = "выборКинематическоСхемыToolStripMenuItem";
            this.выборКинематическоСхемыToolStripMenuItem.Size = new System.Drawing.Size(314, 24);
            this.выборКинематическоСхемыToolStripMenuItem.Text = "выбор кинематической схемы";
            this.выборКинематическоСхемыToolStripMenuItem.Click += new System.EventHandler(this.выборКинематическоСхемыToolStripMenuItem_Click);
            // 
            // проверочныйРасчетToolStripMenuItem
            // 
            this.проверочныйРасчетToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.нагрузочныйРежимToolStripMenuItem});
            this.проверочныйРасчетToolStripMenuItem.Name = "проверочныйРасчетToolStripMenuItem";
            this.проверочныйРасчетToolStripMenuItem.Size = new System.Drawing.Size(90, 24);
            this.проверочныйРасчетToolStripMenuItem.Text = "Проверка";
            // 
            // помощьToolStripMenuItem
            // 
            this.помощьToolStripMenuItem.Name = "помощьToolStripMenuItem";
            this.помощьToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
            this.помощьToolStripMenuItem.Text = "Помощь";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(13, 288);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(232, 50);
            this.button2.TabIndex = 26;
            this.button2.Text = "График";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(493, 288);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(232, 50);
            this.button3.TabIndex = 0;
            this.button3.Text = "Проверить";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // нагрузочныйРежимToolStripMenuItem
            // 
            this.нагрузочныйРежимToolStripMenuItem.Name = "нагрузочныйРежимToolStripMenuItem";
            this.нагрузочныйРежимToolStripMenuItem.Size = new System.Drawing.Size(224, 24);
            this.нагрузочныйРежимToolStripMenuItem.Text = "Нагрузочный режим";
            this.нагрузочныйРежимToolStripMenuItem.Click += new System.EventHandler(this.нагрузочныйРежимToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(754, 370);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem параметрыРасчетаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem параметрыМашиныToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem параметрыШестерниToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem параметрыДифференциалаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem параметрыРаздаточнойКоробкиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem помощьToolStripMenuItem;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ToolStripMenuItem выборКинематическоСхемыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem проверочныйРасчетToolStripMenuItem;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ToolStripMenuItem нагрузочныйРежимToolStripMenuItem;
    }
}

