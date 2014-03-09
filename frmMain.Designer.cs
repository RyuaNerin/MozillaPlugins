namespace MozillaPlugins
{
	partial class frmMain
	{
		/// <summary>
		/// 필수 디자이너 변수입니다.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 사용 중인 모든 리소스를 정리합니다.
		/// </summary>
		/// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form 디자이너에서 생성한 코드

		/// <summary>
		/// 디자이너 지원에 필요한 메서드입니다.
		/// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
		/// </summary>
		private void InitializeComponent()
		{
			this.lsvList = new System.Windows.Forms.ListView();
			this.lsvList0 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lsvList1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lsvList2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lsvList3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lsvList4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.btnDelete = new System.Windows.Forms.Button();
			this.bgwLoad = new System.ComponentModel.BackgroundWorker();
			this.bgwDelete = new System.ComponentModel.BackgroundWorker();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lsvList
			// 
			this.lsvList.CheckBoxes = true;
			this.lsvList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lsvList0,
            this.lsvList1,
            this.lsvList2,
            this.lsvList3,
            this.lsvList4});
			this.lsvList.FullRowSelect = true;
			this.lsvList.GridLines = true;
			this.lsvList.Location = new System.Drawing.Point(12, 12);
			this.lsvList.MultiSelect = false;
			this.lsvList.Name = "lsvList";
			this.lsvList.ShowGroups = false;
			this.lsvList.Size = new System.Drawing.Size(427, 189);
			this.lsvList.TabIndex = 0;
			this.lsvList.UseCompatibleStateImageBehavior = false;
			this.lsvList.View = System.Windows.Forms.View.Details;
			// 
			// lsvList0
			// 
			this.lsvList0.Text = "KeyName";
			this.lsvList0.Width = 170;
			// 
			// lsvList1
			// 
			this.lsvList1.Text = "ProductName";
			this.lsvList1.Width = 160;
			// 
			// lsvList2
			// 
			this.lsvList2.Text = "Description";
			this.lsvList2.Width = 250;
			// 
			// lsvList3
			// 
			this.lsvList3.Text = "Path";
			this.lsvList3.Width = 250;
			// 
			// lsvList4
			// 
			this.lsvList4.Text = "Registry";
			this.lsvList4.Width = 200;
			// 
			// btnDelete
			// 
			this.btnDelete.Enabled = false;
			this.btnDelete.Location = new System.Drawing.Point(333, 207);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(106, 35);
			this.btnDelete.TabIndex = 1;
			this.btnDelete.Text = "삭제";
			this.btnDelete.UseVisualStyleBackColor = true;
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// bgwLoad
			// 
			this.bgwLoad.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwLoad_DoWork);
			this.bgwLoad.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwLoad_RunWorkerCompleted);
			// 
			// bgwDelete
			// 
			this.bgwDelete.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwDelete_DoWork);
			this.bgwDelete.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwDelete_RunWorkerCompleted);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.label1.ForeColor = System.Drawing.Color.Blue;
			this.label1.Location = new System.Drawing.Point(12, 204);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(106, 15);
			this.label1.TabIndex = 2;
			this.label1.Text = "파랑 : 삭제 비추천";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.label2.ForeColor = System.Drawing.Color.Red;
			this.label2.Location = new System.Drawing.Point(12, 219);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(94, 15);
			this.label2.TabIndex = 2;
			this.label2.Text = "빨강 : 삭제 추천\r\n";
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(451, 254);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.lsvList);
			this.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.Name = "frmMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "MozillaPlugins";
			this.Load += new System.EventHandler(this.frmMain_Load);
			this.Resize += new System.EventHandler(this.frmMain_Resize);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListView lsvList;
		private System.Windows.Forms.ColumnHeader lsvList0;
		private System.Windows.Forms.ColumnHeader lsvList1;
		private System.Windows.Forms.ColumnHeader lsvList2;
		private System.Windows.Forms.ColumnHeader lsvList3;
		private System.Windows.Forms.ColumnHeader lsvList4;
		private System.Windows.Forms.Button btnDelete;
		private System.ComponentModel.BackgroundWorker bgwLoad;
		private System.ComponentModel.BackgroundWorker bgwDelete;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;

	}
}

