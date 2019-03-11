namespace OOP
{
    partial class Login_Form
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
            this.login_button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.show_result_button = new System.Windows.Forms.Button();
            this.name_comboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // login_button
            // 
            this.login_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.login_button.Location = new System.Drawing.Point(12, 41);
            this.login_button.Name = "login_button";
            this.login_button.Size = new System.Drawing.Size(157, 25);
            this.login_button.TabIndex = 1;
            this.login_button.Text = "Войти";
            this.login_button.UseVisualStyleBackColor = true;
            this.login_button.Click += new System.EventHandler(this.Login_button_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Наименование филиала:";
            // 
            // show_result_button
            // 
            this.show_result_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.show_result_button.Location = new System.Drawing.Point(175, 41);
            this.show_result_button.Name = "show_result_button";
            this.show_result_button.Size = new System.Drawing.Size(263, 25);
            this.show_result_button.TabIndex = 3;
            this.show_result_button.Text = "Показать итоговую таблицу";
            this.show_result_button.UseVisualStyleBackColor = true;
            // 
            // name_comboBox
            // 
            this.name_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.name_comboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.name_comboBox.FormattingEnabled = true;
            this.name_comboBox.Items.AddRange(new object[] {
            "Филиал первый",
            "Филиал второй",
            "Филиал для теста"});
            this.name_comboBox.Location = new System.Drawing.Point(191, 12);
            this.name_comboBox.MaxLength = 32;
            this.name_comboBox.Name = "name_comboBox";
            this.name_comboBox.Size = new System.Drawing.Size(247, 24);
            this.name_comboBox.TabIndex = 4;
            // 
            // Login_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 78);
            this.Controls.Add(this.name_comboBox);
            this.Controls.Add(this.show_result_button);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.login_button);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Login_Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Лучший филиал: Вход";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button login_button;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button show_result_button;
        private System.Windows.Forms.ComboBox name_comboBox;
    }
}