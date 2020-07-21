namespace TestWindowsForms
{
    partial class Form2
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
            this.CreateForm = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // CreateForm
            // 
            this.CreateForm.Location = new System.Drawing.Point(584, 204);
            this.CreateForm.Name = "CreateForm";
            this.CreateForm.Size = new System.Drawing.Size(163, 41);
            this.CreateForm.TabIndex = 0;
            this.CreateForm.Text = "Create Form";
            this.CreateForm.UseVisualStyleBackColor = true;
            this.CreateForm.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(612, 157);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Provided Text";
            // 
            // Form2
            // 
            this.ClientSize = new System.Drawing.Size(802, 371);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CreateForm);
            this.Name = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Create_Form;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button CreateForm;
        private System.Windows.Forms.Label label2;
    }
}