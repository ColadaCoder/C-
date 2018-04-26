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
    public partial class frmSelectOrderby : Form
    {
        BaseClass.BaseInfo baseinfo = new EMS.BaseClass.BaseInfo();//创建BaseInfo类的对象
        public frmSelectOrderby()
        {
            InitializeComponent();
        }

        private void frmSelectOrderby_Load(object sender, EventArgs e)
        {
            DataSet ds = null;//创建数据集对象
            ds = baseinfo.SetUnitsList("tb_units");//获取往来单位信息
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)//遍历往来单位信息数据集
            {
                cmbUnits.Items.Add(ds.Tables[0].Rows[i]["fullname"].ToString());//显示往来单位名称
            }
            ds = baseinfo.SetHandleList("tb_employee");//获取职员信息
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)//遍历职员信息数据
            {
                cmbHandle.Items.Add(ds.Tables[0].Rows[i]["fullname"].ToString());//显示职员名称
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            SaleStock.frmSellStockDesc sellStockDesc = new EMS.SaleStock.frmSellStockDesc();//创建商品销售排行榜窗体对象
            DataSet ds = null;//创建数据集对象
            if (rdbSaleSum.Checked)//判断“按销售金额排行”单选按钮是否选中
            {
                //按销售金额排行查询数据
                ds = baseinfo.GetTSumDesc(cmbHandle.Text, cmbUnits.Text, dtpStar.Value, dtpEnd.Value, "tb_desc");
                sellStockDesc.dgvStockList.DataSource = ds.Tables[0].DefaultView;//在商品销售排行榜窗体中显示查询到的数据
            }
            else
            {
                //按销售数量排行查询数据
                ds = baseinfo.GetQtyDesc(cmbHandle.Text, cmbUnits.Text, dtpStar.Value, dtpEnd.Value, "tb_desc");
                sellStockDesc.dgvStockList.DataSource = ds.Tables[0].DefaultView;//在商品销售排行榜窗体中显示查询到的数据
            }
            sellStockDesc.Show();//显示商品销售排行榜窗体
            this.Close();//关闭当前窗体
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();//关闭当前窗体
        }
    }
}