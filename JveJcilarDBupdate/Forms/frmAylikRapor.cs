﻿using JveJcilarDBupdate.Repostory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JveJcilarDBupdate.Forms
{
    public partial class frmAylikRapor : Form
    {
        public frmAylikRapor()
        {
            InitializeComponent();
        }
        private RaporRepostory _raporRepostory;
        private void btnAylikRapor_Click(object sender, EventArgs e)
        {
            _raporRepostory = new RaporRepostory();
            var allData = _raporRepostory.Get();
            var queryAylikRapor = allData
                .Where(a => a.EklenmeZamani.Month == DateTime.Now.Month)
                .GroupBy(a => new
                {
                    a.EklenmeZamani.Day
                })
                .Select(a => new
                {
                    Gün = $"{a.Key.Day}.Gün",
                    Toplam = a.Sum(p => p.Adet * p.Fiyat).ToString("c")
                })
                .ToList();
            dgvAylikRapor.DataSource = queryAylikRapor;
            dgvAylikRapor.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvAylikRapor.DefaultCellStyle.Font = new Font("Tahoma", 15);
            dgvAylikRapor.AutoSize = true;
        }

    }
}
