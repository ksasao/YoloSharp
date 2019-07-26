namespace YoloSharpTest
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBoxBackend = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxTarget = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxUseNMS = new System.Windows.Forms.CheckBox();
            this.num_confidence = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.num_nms = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_confidence)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_nms)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(0, 56);
            this.textBox1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(1092, 94);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "画像を Drag & Drop してください";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1092, 396);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.DragDrop += new System.Windows.Forms.DragEventHandler(this.pictureBox1_DragDrop);
            this.pictureBox1.DragEnter += new System.Windows.Forms.DragEventHandler(this.pictureBox1_DragEnter);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Cursor = System.Windows.Forms.Cursors.SizeNS;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.textBox1);
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pictureBox1);
            this.splitContainer1.Size = new System.Drawing.Size(1092, 552);
            this.splitContainer1.SplitterDistance = 150;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.comboBoxBackend);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.comboBoxTarget);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cbxUseNMS);
            this.panel1.Controls.Add(this.num_confidence);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.num_nms);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1092, 56);
            this.panel1.TabIndex = 1;
            // 
            // comboBoxBackend
            // 
            this.comboBoxBackend.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBackend.FormattingEnabled = true;
            this.comboBoxBackend.Location = new System.Drawing.Point(108, 10);
            this.comboBoxBackend.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxBackend.Name = "comboBoxBackend";
            this.comboBoxBackend.Size = new System.Drawing.Size(136, 26);
            this.comboBoxBackend.TabIndex = 3;
            this.comboBoxBackend.SelectedIndexChanged += new System.EventHandler(this.comboBoxBackend_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 16);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "Backend: ";
            // 
            // comboBoxTarget
            // 
            this.comboBoxTarget.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTarget.FormattingEnabled = true;
            this.comboBoxTarget.Location = new System.Drawing.Point(341, 10);
            this.comboBoxTarget.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxTarget.Name = "comboBoxTarget";
            this.comboBoxTarget.Size = new System.Drawing.Size(184, 26);
            this.comboBoxTarget.TabIndex = 1;
            this.comboBoxTarget.SelectedIndexChanged += new System.EventHandler(this.comboBoxTarget_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(262, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Target: ";
            // 
            // cbxUseNMS
            // 
            this.cbxUseNMS.AutoSize = true;
            this.cbxUseNMS.Location = new System.Drawing.Point(585, 15);
            this.cbxUseNMS.Margin = new System.Windows.Forms.Padding(2);
            this.cbxUseNMS.Name = "cbxUseNMS";
            this.cbxUseNMS.Size = new System.Drawing.Size(101, 22);
            this.cbxUseNMS.TabIndex = 4;
            this.cbxUseNMS.Text = "use NMS";
            this.cbxUseNMS.CheckedChanged += new System.EventHandler(this.cbxUseNMS_CheckedChanged);
            // 
            // num_confidence
            // 
            this.num_confidence.AutoSize = true;
            this.num_confidence.DecimalPlaces = 1;
            this.num_confidence.Enabled = false;
            this.num_confidence.Increment = 0.1M;
            this.num_confidence.Location = new System.Drawing.Point(828, 14);
            this.num_confidence.Margin = new System.Windows.Forms.Padding(2);
            this.num_confidence.Maximum = 1.0M;
            this.num_confidence.Name = "num_confidence";
            this.num_confidence.Size = new System.Drawing.Size(54, 25);
            this.num_confidence.TabIndex = 5;
            this.num_confidence.Value = 0.2M;
            this.num_confidence.ValueChanged += new System.EventHandler(this.num_confidence_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(723, 16);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 18);
            this.label3.TabIndex = 6;
            this.label3.Text = "Confidence: ";
            // 
            // num_nms
            // 
            this.num_nms.AutoSize = true;
            this.num_nms.DecimalPlaces = 1;
            this.num_nms.Enabled = false;
            this.num_nms.Increment = 0.1M;
            this.num_nms.Location = new System.Drawing.Point(1000, 14);
            this.num_nms.Margin = new System.Windows.Forms.Padding(2);
            this.num_nms.Maximum = 1.0M;
            this.num_nms.Name = "num_nms";
            this.num_nms.Size = new System.Drawing.Size(54, 25);
            this.num_nms.TabIndex = 6;
            this.num_nms.Value = 0.5M;
            this.num_nms.ValueChanged += new System.EventHandler(this.num_nms_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(943, 16);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 18);
            this.label4.TabIndex = 6;
            this.label4.Text = "NMS: ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1092, 552);
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "Form1";
            this.Text = "YoloSharp";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_confidence)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_nms)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox comboBoxTarget;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxBackend;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbxUseNMS;
        private System.Windows.Forms.NumericUpDown num_confidence;
        private System.Windows.Forms.NumericUpDown num_nms;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}

