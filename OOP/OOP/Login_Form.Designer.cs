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
            this.branches_comboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.quarters_comboBox = new System.Windows.Forms.ComboBox();
            this.save_result_button = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // login_button
            // 
            this.login_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.login_button.Location = new System.Drawing.Point(12, 95);
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
            this.show_result_button.Location = new System.Drawing.Point(175, 95);
            this.show_result_button.Name = "show_result_button";
            this.show_result_button.Size = new System.Drawing.Size(263, 25);
            this.show_result_button.TabIndex = 3;
            this.show_result_button.Text = "Показать итоговую таблицу";
            this.show_result_button.UseVisualStyleBackColor = true;
            this.show_result_button.Click += new System.EventHandler(this.Show_result_button_Click);
            // 
            // branches_comboBox
            // 
            this.branches_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.branches_comboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.branches_comboBox.FormattingEnabled = true;
            this.branches_comboBox.Location = new System.Drawing.Point(191, 12);
            this.branches_comboBox.MaxLength = 32;
            this.branches_comboBox.Name = "branches_comboBox";
            this.branches_comboBox.Size = new System.Drawing.Size(247, 24);
            this.branches_comboBox.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(12, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Квартал:";
            // 
            // quarters_comboBox
            // 
            this.quarters_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.quarters_comboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.quarters_comboBox.FormattingEnabled = true;
            this.quarters_comboBox.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.quarters_comboBox.Location = new System.Drawing.Point(85, 48);
            this.quarters_comboBox.MaxLength = 32;
            this.quarters_comboBox.Name = "quarters_comboBox";
            this.quarters_comboBox.Size = new System.Drawing.Size(56, 24);
            this.quarters_comboBox.TabIndex = 6;
            // 
            // save_result_button
            // 
            this.save_result_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.save_result_button.Location = new System.Drawing.Point(191, 48);
            this.save_result_button.Name = "save_result_button";
            this.save_result_button.Size = new System.Drawing.Size(247, 25);
            this.save_result_button.TabIndex = 7;
            this.save_result_button.Text = "Сохранить итоговую таблицу";
            this.save_result_button.UseVisualStyleBackColor = true;
            this.save_result_button.Click += new System.EventHandler(this.Save_result_button_Click);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "Изображение|*.bmp";
            // 
            // Login_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 132);
            this.Controls.Add(this.save_result_button);
            this.Controls.Add(this.quarters_comboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.branches_comboBox);
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
        private System.Windows.Forms.ComboBox branches_comboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox quarters_comboBox;
        private System.Windows.Forms.Button save_result_button;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}