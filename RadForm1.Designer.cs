namespace GenerateArkaveImport
{
    partial class RadForm1
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
            this.TBserverName = new System.Windows.Forms.TextBox();
            this.TBLogin = new System.Windows.Forms.TextBox();
            this.TBpassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnconx = new System.Windows.Forms.Button();
            this.BtnGenerer = new System.Windows.Forms.Button();
            this.BtnAnnuler = new System.Windows.Forms.Button();
            this.CBtranche = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // TBserverName
            // 
            this.TBserverName.Location = new System.Drawing.Point(275, 25);
            this.TBserverName.Name = "TBserverName";
            this.TBserverName.Size = new System.Drawing.Size(129, 20);
            this.TBserverName.TabIndex = 0;
            // 
            // TBLogin
            // 
            this.TBLogin.Location = new System.Drawing.Point(275, 65);
            this.TBLogin.Name = "TBLogin";
            this.TBLogin.Size = new System.Drawing.Size(129, 20);
            this.TBLogin.TabIndex = 1;
            // 
            // TBpassword
            // 
            this.TBpassword.Location = new System.Drawing.Point(275, 104);
            this.TBpassword.Name = "TBpassword";
            this.TBpassword.PasswordChar = '*';
            this.TBpassword.Size = new System.Drawing.Size(129, 20);
            this.TBpassword.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Historic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(150, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Serveur         :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Historic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(150, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Login            :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Historic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(150, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Password       :";
            // 
            // btnconx
            // 
            this.btnconx.Location = new System.Drawing.Point(41, 186);
            this.btnconx.Name = "btnconx";
            this.btnconx.Size = new System.Drawing.Size(121, 23);
            this.btnconx.TabIndex = 6;
            this.btnconx.Text = "Connexion";
            this.btnconx.UseVisualStyleBackColor = true;
            this.btnconx.Click += new System.EventHandler(this.btnconx_Click);
            // 
            // BtnGenerer
            // 
            this.BtnGenerer.Enabled = false;
            this.BtnGenerer.Location = new System.Drawing.Point(339, 231);
            this.BtnGenerer.Name = "BtnGenerer";
            this.BtnGenerer.Size = new System.Drawing.Size(121, 23);
            this.BtnGenerer.TabIndex = 7;
            this.BtnGenerer.Text = "Generer";
            this.BtnGenerer.UseVisualStyleBackColor = true;
            // 
            // BtnAnnuler
            // 
            this.BtnAnnuler.Location = new System.Drawing.Point(41, 231);
            this.BtnAnnuler.Name = "BtnAnnuler";
            this.BtnAnnuler.Size = new System.Drawing.Size(121, 23);
            this.BtnAnnuler.TabIndex = 8;
            this.BtnAnnuler.Text = "Annuler";
            this.BtnAnnuler.UseVisualStyleBackColor = true;
            // 
            // CBtranche
            // 
            this.CBtranche.FormattingEnabled = true;
            this.CBtranche.Location = new System.Drawing.Point(339, 186);
            this.CBtranche.Name = "CBtranche";
            this.CBtranche.Size = new System.Drawing.Size(121, 21);
            this.CBtranche.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Historic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(243, 188);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "Tranche     :";
            // 
            // RadForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 266);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.CBtranche);
            this.Controls.Add(this.BtnAnnuler);
            this.Controls.Add(this.BtnGenerer);
            this.Controls.Add(this.btnconx);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TBpassword);
            this.Controls.Add(this.TBLogin);
            this.Controls.Add(this.TBserverName);
            this.Name = "RadForm1";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "RadForm1";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TBserverName;
        private System.Windows.Forms.TextBox TBLogin;
        private System.Windows.Forms.TextBox TBpassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnconx;
        private System.Windows.Forms.Button BtnGenerer;
        private System.Windows.Forms.Button BtnAnnuler;
        private System.Windows.Forms.ComboBox CBtranche;
        private System.Windows.Forms.Label label4;
    }
}