namespace project
{
    partial class choose_time
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
            this.dateTimeinTXT = new System.Windows.Forms.DateTimePicker();
            this.dateTXT = new System.Windows.Forms.DateTimePicker();
            this.insertBTN = new System.Windows.Forms.Button();
            this.dateTimeoutTXT = new System.Windows.Forms.DateTimePicker();
            this.dataEquipment = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataEquipment)).BeginInit();
            this.SuspendLayout();
            // 
            // dateTimeinTXT
            // 
            this.dateTimeinTXT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTimeinTXT.CalendarFont = new System.Drawing.Font("Anakotmai Medium", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.dateTimeinTXT.CalendarForeColor = System.Drawing.Color.Black;
            this.dateTimeinTXT.CalendarMonthBackground = System.Drawing.Color.Black;
            this.dateTimeinTXT.CalendarTrailingForeColor = System.Drawing.Color.White;
            this.dateTimeinTXT.CustomFormat = "hh:mm";
            this.dateTimeinTXT.Font = new System.Drawing.Font("FC Ekaluck", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.dateTimeinTXT.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimeinTXT.Location = new System.Drawing.Point(160, 265);
            this.dateTimeinTXT.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dateTimeinTXT.Name = "dateTimeinTXT";
            this.dateTimeinTXT.ShowUpDown = true;
            this.dateTimeinTXT.Size = new System.Drawing.Size(186, 39);
            this.dateTimeinTXT.TabIndex = 0;
            // 
            // dateTXT
            // 
            this.dateTXT.CalendarFont = new System.Drawing.Font("Anakotmai Medium", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.dateTXT.CustomFormat = " dd/MM/yy";
            this.dateTXT.Font = new System.Drawing.Font("FC Ekaluck", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.dateTXT.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTXT.Location = new System.Drawing.Point(160, 162);
            this.dateTXT.Name = "dateTXT";
            this.dateTXT.Size = new System.Drawing.Size(186, 39);
            this.dateTXT.TabIndex = 1;
            this.dateTXT.ValueChanged += new System.EventHandler(this.dateTXT_ValueChanged);
            // 
            // insertBTN
            // 
            this.insertBTN.Font = new System.Drawing.Font("Anakotmai Medium", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.insertBTN.Location = new System.Drawing.Point(181, 452);
            this.insertBTN.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.insertBTN.Name = "insertBTN";
            this.insertBTN.Size = new System.Drawing.Size(144, 38);
            this.insertBTN.TabIndex = 2;
            this.insertBTN.Text = "จอง";
            this.insertBTN.UseVisualStyleBackColor = true;
            this.insertBTN.Click += new System.EventHandler(this.insertBTN_Click);
            // 
            // dateTimeoutTXT
            // 
            this.dateTimeoutTXT.CalendarFont = new System.Drawing.Font("Anakotmai Medium", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.dateTimeoutTXT.CustomFormat = "hh : mm";
            this.dateTimeoutTXT.Font = new System.Drawing.Font("FC Ekaluck", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.dateTimeoutTXT.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimeoutTXT.Location = new System.Drawing.Point(160, 361);
            this.dateTimeoutTXT.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dateTimeoutTXT.Name = "dateTimeoutTXT";
            this.dateTimeoutTXT.ShowUpDown = true;
            this.dateTimeoutTXT.Size = new System.Drawing.Size(186, 39);
            this.dateTimeoutTXT.TabIndex = 6;
            // 
            // dataEquipment
            // 
            this.dataEquipment.AllowUserToAddRows = false;
            this.dataEquipment.AllowUserToDeleteRows = false;
            this.dataEquipment.BackgroundColor = System.Drawing.Color.White;
            this.dataEquipment.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataEquipment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataEquipment.GridColor = System.Drawing.Color.Black;
            this.dataEquipment.Location = new System.Drawing.Point(586, 78);
            this.dataEquipment.Margin = new System.Windows.Forms.Padding(4);
            this.dataEquipment.Name = "dataEquipment";
            this.dataEquipment.ReadOnly = true;
            this.dataEquipment.RowHeadersVisible = false;
            this.dataEquipment.RowHeadersWidth = 51;
            this.dataEquipment.Size = new System.Drawing.Size(500, 479);
            this.dataEquipment.TabIndex = 7;
            this.dataEquipment.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataEquipment_CellContentClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Black;
            this.label1.Font = new System.Drawing.Font("Anakotmai Medium", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.ForeColor = System.Drawing.Color.Lime;
            this.label1.Location = new System.Drawing.Point(90, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(449, 52);
            this.label1.TabIndex = 8;
            this.label1.Text = "เลือกวันและเวลาที่ต้องการจอง";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Black;
            this.label2.Font = new System.Drawing.Font("Anakotmai Medium", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(577, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(176, 52);
            this.label2.TabIndex = 9;
            this.label2.Text = "ตารางเวลา";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Black;
            this.label3.Font = new System.Drawing.Font("Anakotmai Medium", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(203, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 38);
            this.label3.TabIndex = 10;
            this.label3.Text = "เลือกวัน";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Black;
            this.label4.Font = new System.Drawing.Font("Anakotmai Medium", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(176, 225);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(155, 38);
            this.label4.TabIndex = 10;
            this.label4.Text = "เลือกเวลาเริ่ม";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Black;
            this.label5.Font = new System.Drawing.Font("Anakotmai Medium", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(171, 321);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(164, 38);
            this.label5.TabIndex = 11;
            this.label5.Text = "เลือกเวลาออก";
            // 
            // choose_time
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1133, 601);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataEquipment);
            this.Controls.Add(this.dateTimeoutTXT);
            this.Controls.Add(this.insertBTN);
            this.Controls.Add(this.dateTXT);
            this.Controls.Add(this.dateTimeinTXT);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "choose_time";
            this.ShowIcon = false;
            this.Text = "choose_time";
            this.Load += new System.EventHandler(this.choose_time_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataEquipment)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DateTimePicker dateTimeoutTXT;
        public System.Windows.Forms.Button insertBTN;
        private System.Windows.Forms.DataGridView dataEquipment;
        public System.Windows.Forms.DateTimePicker dateTimeinTXT;
        public System.Windows.Forms.DateTimePicker dateTXT;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}