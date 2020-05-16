namespace GraphGlWinFormsClient
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
            this.listView1 = new System.Windows.Forms.ListView();
            this.loadingDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.vehicleType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cargoDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.routFrom = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.routTo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.routFromCountry = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.routToCountry = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.loadingDate,
            this.vehicleType,
            this.cargoDescription,
            this.routFrom,
            this.routTo,
            this.routFromCountry,
            this.routToCountry});
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(-1, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(844, 451);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // loadingDate
            // 
            this.loadingDate.Text = "loadingDate";
            this.loadingDate.Width = 120;
            // 
            // vehicleType
            // 
            this.vehicleType.Text = "vehicleType";
            this.vehicleType.Width = 120;
            // 
            // cargoDescription
            // 
            this.cargoDescription.Text = "cargoDescription";
            this.cargoDescription.Width = 120;
            // 
            // routFrom
            // 
            this.routFrom.Text = "routFrom";
            this.routFrom.Width = 120;
            // 
            // routTo
            // 
            this.routTo.Text = "routTo";
            this.routTo.Width = 120;
            // 
            // routFromCountry
            // 
            this.routFromCountry.Text = "routFromCountry";
            this.routFromCountry.Width = 120;
            // 
            // routToCountry
            // 
            this.routToCountry.Text = "routToCountry";
            this.routToCountry.Width = 120;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(845, 450);
            this.Controls.Add(this.listView1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader loadingDate;
        private System.Windows.Forms.ColumnHeader vehicleType;
        private System.Windows.Forms.ColumnHeader cargoDescription;
        private System.Windows.Forms.ColumnHeader routFrom;
        private System.Windows.Forms.ColumnHeader routTo;
        private System.Windows.Forms.ColumnHeader routFromCountry;
        private System.Windows.Forms.ColumnHeader routToCountry;
    }
}

