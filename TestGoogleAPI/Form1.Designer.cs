namespace TestGoogleAPI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtCellNameSet = new TextBox();
            txtCellValue = new TextBox();
            SetButton = new Button();
            label1 = new Label();
            label2 = new Label();
            txtCellNameGet = new TextBox();
            GetButton = new Button();
            StartButton = new Button();
            txtCellGetValue = new TextBox();
            txtCreateSheetName = new TextBox();
            CreateButton = new Button();
            SuspendLayout();
            // 
            // txtCellNameSet
            // 
            txtCellNameSet.Location = new Point(63, 43);
            txtCellNameSet.Name = "txtCellNameSet";
            txtCellNameSet.Size = new Size(150, 31);
            txtCellNameSet.TabIndex = 0;
            // 
            // txtCellValue
            // 
            txtCellValue.Location = new Point(235, 43);
            txtCellValue.Name = "txtCellValue";
            txtCellValue.Size = new Size(150, 31);
            txtCellValue.TabIndex = 1;
            // 
            // SetButton
            // 
            SetButton.Enabled = false;
            SetButton.Location = new Point(405, 41);
            SetButton.Name = "SetButton";
            SetButton.Size = new Size(112, 34);
            SetButton.TabIndex = 2;
            SetButton.Text = "Set";
            SetButton.UseVisualStyleBackColor = true;
            SetButton.Click += SetButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(63, 15);
            label1.Name = "label1";
            label1.Size = new Size(111, 25);
            label1.TabIndex = 3;
            label1.Text = "Name of cell";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(235, 15);
            label2.Name = "label2";
            label2.Size = new Size(54, 25);
            label2.TabIndex = 4;
            label2.Text = "Value";
            // 
            // txtCellNameGet
            // 
            txtCellNameGet.Location = new Point(63, 107);
            txtCellNameGet.Name = "txtCellNameGet";
            txtCellNameGet.Size = new Size(150, 31);
            txtCellNameGet.TabIndex = 5;
            // 
            // GetButton
            // 
            GetButton.Enabled = false;
            GetButton.Location = new Point(405, 105);
            GetButton.Name = "GetButton";
            GetButton.Size = new Size(112, 34);
            GetButton.TabIndex = 6;
            GetButton.Text = "Get";
            GetButton.UseVisualStyleBackColor = true;
            GetButton.Click += GetButton_Click;
            // 
            // StartButton
            // 
            StartButton.Location = new Point(62, 175);
            StartButton.Name = "StartButton";
            StartButton.Size = new Size(112, 34);
            StartButton.TabIndex = 8;
            StartButton.Text = "Start";
            StartButton.UseVisualStyleBackColor = true;
            StartButton.Click += StartButton_Click;
            // 
            // txtCellGetValue
            // 
            txtCellGetValue.Location = new Point(235, 107);
            txtCellGetValue.Name = "txtCellGetValue";
            txtCellGetValue.ReadOnly = true;
            txtCellGetValue.Size = new Size(150, 31);
            txtCellGetValue.TabIndex = 9;
            // 
            // txtCreateSheetName
            // 
            txtCreateSheetName.Location = new Point(63, 254);
            txtCreateSheetName.Name = "txtCreateSheetName";
            txtCreateSheetName.Size = new Size(150, 31);
            txtCreateSheetName.TabIndex = 10;
            // 
            // CreateButton
            // 
            CreateButton.Location = new Point(405, 251);
            CreateButton.Name = "CreateButton";
            CreateButton.Size = new Size(112, 34);
            CreateButton.TabIndex = 11;
            CreateButton.Text = "Create";
            CreateButton.UseVisualStyleBackColor = true;
            CreateButton.Click += CreateButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(CreateButton);
            Controls.Add(txtCreateSheetName);
            Controls.Add(txtCellGetValue);
            Controls.Add(StartButton);
            Controls.Add(GetButton);
            Controls.Add(txtCellNameGet);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(SetButton);
            Controls.Add(txtCellValue);
            Controls.Add(txtCellNameSet);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtCellNameSet;
        private TextBox txtCellValue;
        private Button SetButton;
        private Label label1;
        private Label label2;
        private TextBox txtCellNameGet;
        private Button GetButton;
        private Button StartButton;
        private TextBox txtCellGetValue;
        private TextBox txtCreateSheetName;
        private Button CreateButton;
    }
}