using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EMS.SelectDataDialog
{
    public partial class frmSelectUnits : Form
    {
        public BuyStock.frmBuyStock buyStock;
        public SaleStock.frmResellStock resellStock;
        public SaleStock.frmSellStock sellStock;
        public BuyStock.frmRebuyStock reBuyStock;

        public string M_str_object = "";

        public frmSelectUnits()
        {
            InitializeComponent();
        }

        private void SelectUnits_Load(object sender, EventArgs e)
        {
            BaseClass.BaseInfo baseinfo = new EMS.BaseClass.BaseInfo();
            DataSet ds = baseinfo.GetAllUnits("tb_Units");
            dgvSelectUnitsList.DataSource = ds.Tables[0].DefaultView;

            //设置列表标题
            dgvSelectUnitsList.Columns[0].HeaderText = "单位编号";
            dgvSelectUnitsList.Columns[1].HeaderText = "单位名称";
            dgvSelectUnitsList.Columns[2].HeaderText = "税号";
            dgvSelectUnitsList.Columns[3].HeaderText = "单位电话";
            dgvSelectUnitsList.Columns[4].HeaderText = "联系人";
            dgvSelectUnitsList.Columns[5].HeaderText = "单位地址";
            dgvSelectUnitsList.Columns[6].HeaderText = "开户行及账号";
            dgvSelectUnitsList.Columns[7].HeaderText = "累计应收款";
            dgvSelectUnitsList.Columns[8].HeaderText = "累计应付款";
        }

        private void dgvSelectUnitsList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (M_str_object == "BuyStock")
            {
                buyStock.txtUnits.Text = dgvSelectUnitsList[1, e.RowIndex].Value.ToString();　//选中经手人
                this.Close();
            }
            if (M_str_object == "ResellStock")
            {
                resellStock.txtUnits.Text = dgvSelectUnitsList[1, e.RowIndex].Value.ToString();　//选中经手人
                this.Close();
            }
            if (M_str_object == "RebuyStock")
            {
                reBuyStock.txtUnits.Text = dgvSelectUnitsList[1, e.RowIndex].Value.ToString();　//选中经手人
                this.Close();
            }
            if (M_str_object == "SellStock")
            {
                sellStock.txtUnits.Text = dgvSelectUnitsList[1, e.RowIndex].Value.ToString();　//选中经手人
                this.Close();
            }
        }
    }
}