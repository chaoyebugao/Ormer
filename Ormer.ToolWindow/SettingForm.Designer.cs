namespace Ormer.ToolWindow
{
    partial class SettingForm
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
            this.tabControl_main = new System.Windows.Forms.TabControl();
            this.generators = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label_defaultItem = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl_main.SuspendLayout();
            this.generators.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl_main
            // 
            this.tabControl_main.Controls.Add(this.generators);
            this.tabControl_main.Controls.Add(this.tabPage2);
            this.tabControl_main.Location = new System.Drawing.Point(12, 12);
            this.tabControl_main.Name = "tabControl_main";
            this.tabControl_main.SelectedIndex = 0;
            this.tabControl_main.Size = new System.Drawing.Size(563, 629);
            this.tabControl_main.TabIndex = 0;
            // 
            // generators
            // 
            this.generators.Controls.Add(this.label2);
            this.generators.Controls.Add(this.label_defaultItem);
            this.generators.Controls.Add(this.comboBox1);
            this.generators.Location = new System.Drawing.Point(4, 22);
            this.generators.Name = "generators";
            this.generators.Padding = new System.Windows.Forms.Padding(3);
            this.generators.Size = new System.Drawing.Size(555, 603);
            this.generators.TabIndex = 0;
            this.generators.Text = "Generators";
            this.generators.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(555, 603);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(58, 85);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 20);
            this.comboBox1.TabIndex = 0;
            // 
            // label_defaultItem
            // 
            this.label_defaultItem.AutoSize = true;
            this.label_defaultItem.Location = new System.Drawing.Point(17, 20);
            this.label_defaultItem.Name = "label_defaultItem";
            this.label_defaultItem.Size = new System.Drawing.Size(41, 12);
            this.label_defaultItem.TabIndex = 1;
            this.label_defaultItem.Text = "默认项";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(64, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "label1";
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(587, 653);
            this.Controls.Add(this.tabControl_main);
            this.Name = "SettingForm";
            this.Text = "SettingForm";
            this.tabControl_main.ResumeLayout(false);
            this.generators.ResumeLayout(false);
            this.generators.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl_main;
        private System.Windows.Forms.TabPage generators;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label_defaultItem;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label2;
    }
}