namespace SMT_Data_Gatekeeper
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
            dgvResults = new DataGridView();
            btnLoadBOM = new Button();
            lblStatus = new Label();
            btnLoadXY = new Button();
            btnValidate = new Button();
            btnSendAPI = new Button();
            btnExport = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvResults).BeginInit();
            SuspendLayout();
            // 
            // dgvResults
            // 
            dgvResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvResults.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvResults.Location = new Point(159, 34);
            dgvResults.Name = "dgvResults";
            dgvResults.Size = new Size(517, 311);
            dgvResults.TabIndex = 0;
            dgvResults.CellContentClick += dgvResults_CellContentClick;
            // 
            // btnLoadBOM
            // 
            btnLoadBOM.Font = new Font("Segoe UI Emoji", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLoadBOM.Location = new Point(12, 34);
            btnLoadBOM.Name = "btnLoadBOM";
            btnLoadBOM.Size = new Size(93, 23);
            btnLoadBOM.TabIndex = 1;
            btnLoadBOM.Text = "1.Load BOM\r\n";
            btnLoadBOM.UseVisualStyleBackColor = true;
            btnLoadBOM.Click += btnLoadBOM_Click;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Segoe UI Emoji", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblStatus.Location = new Point(159, 366);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(90, 16);
            lblStatus.TabIndex = 2;
            lblStatus.Text = "Status: Ready";
            // 
            // btnLoadXY
            // 
            btnLoadXY.Font = new Font("Segoe UI Emoji", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLoadXY.Location = new Point(12, 100);
            btnLoadXY.Name = "btnLoadXY";
            btnLoadXY.Size = new Size(93, 23);
            btnLoadXY.TabIndex = 3;
            btnLoadXY.Text = "2.Load XY Data";
            btnLoadXY.UseVisualStyleBackColor = true;
            btnLoadXY.Click += btnLoadXY_Click;
            // 
            // btnValidate
            // 
            btnValidate.Font = new Font("Segoe UI Emoji", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnValidate.Location = new Point(12, 174);
            btnValidate.Name = "btnValidate";
            btnValidate.Size = new Size(93, 23);
            btnValidate.TabIndex = 4;
            btnValidate.Text = "3.Validate Data";
            btnValidate.UseVisualStyleBackColor = true;
            btnValidate.Click += btnValidate_Click;
            // 
            // btnSendAPI
            // 
            btnSendAPI.Font = new Font("Segoe UI Emoji", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSendAPI.Location = new Point(12, 248);
            btnSendAPI.Name = "btnSendAPI";
            btnSendAPI.Size = new Size(116, 23);
            btnSendAPI.TabIndex = 5;
            btnSendAPI.Text = "4.Send to API";
            btnSendAPI.UseVisualStyleBackColor = true;
            btnSendAPI.Click += btnSendAPI_Click;
            // 
            // btnExport
            // 
            btnExport.Font = new Font("Segoe UI Emoji", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnExport.Location = new Point(12, 322);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(116, 23);
            btnExport.TabIndex = 6;
            btnExport.Text = "5.Export Report\r\n";
            btnExport.UseVisualStyleBackColor = true;
            btnExport.Click += btnExport_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1008, 835);
            Controls.Add(btnExport);
            Controls.Add(btnSendAPI);
            Controls.Add(btnValidate);
            Controls.Add(btnLoadXY);
            Controls.Add(lblStatus);
            Controls.Add(btnLoadBOM);
            Controls.Add(dgvResults);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dgvResults).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvResults;
        private Button btnLoadBOM;
        private Label lblStatus;
        private Button btnLoadXY;
        private Button btnValidate;
        private Button btnSendAPI;
        private Button btnExport;
    }
}
