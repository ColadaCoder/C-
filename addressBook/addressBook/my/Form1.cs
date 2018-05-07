using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace my
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //dataGridView1.Columns[0].Width = 30;
            //dataGridView1.Columns[1].Width = 40;
            // dataGridView1.Columns[2].Width = 40;
            //dataGridView1.Columns[3].Width = 80;
            //dataGridView1.Columns[4].Width = 80;
            //dataGridView1.Columns[5].Width = 100;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

        }
        //定义数据库连接字符串
        string strCon = "Data Source=.;Database=db_EMS;Integrated Security=True";
        DataClasses1DataContext linq;          //声明Linq连接对象

        private void Form1_Load_1(object sender, EventArgs e)
        {
            BindInfo();
        }

        //用BindInfo方法查询
        private void BindInfo() {
            linq = new DataClasses1DataContext(strCon);
            if (textBox1.Text == "")
            {   
                //获取个人所有信息
                var result = from info in linq.tb_student
                             select new
                             {
                                 序号 = info.id,
                                 姓名 = info.name,
                                 性别 = info.sex,
                                 联系方式 = info.phone,
                                 QQ号 = info.qq,
                                 微信号 = info.weixin,
                                 家庭详细地址 = info.address
                             };
                dataGridView1.DataSource = result;
            }
            else {
                switch (comboBox1.Text) {
                    //序号查询
                    case "序号":
                        //根据序号查询通讯录
                        var resultid = from info in linq.tb_student
                                       where info.id == Convert.ToDouble(textBox1.Text)
                                     select new
                                     {
                                         序号 = info.id,
                                         姓名 = info.name,
                                         性别 = info.sex,
                                         联系方式 = info.phone,
                                         QQ号 = info.qq,
                                         微信号 = info.weixin,
                                         家庭详细地址 = info.address
                                     };
                        dataGridView1.DataSource = resultid;
                        break;
                    //姓名查询
                    case "姓名":
                        //根据姓名查询通讯录
                        var resultname = from info in linq.tb_student
                                       where info.name == textBox1.Text
                                       select new
                                       {
                                           序号 = info.id,
                                           姓名 = info.name,
                                           性别 = info.sex,
                                           联系方式 = info.phone,
                                           QQ号 = info.qq,
                                           微信号 = info.weixin,
                                           家庭详细地址 = info.address
                                       };
                        dataGridView1.DataSource = resultname;
                        break;
                    //姓名查询
                    case "电话":
                        //根据电话查询通讯录
                        var resultphone = from info in linq.tb_student
                                         where info.phone == Convert.ToDouble(textBox1.Text)
                                         select new
                                         {
                                             序号 = info.id,
                                             姓名 = info.name,
                                             性别 = info.sex,
                                             联系方式 = info.phone,
                                             QQ号 = info.qq,
                                             微信号 = info.weixin,
                                             家庭详细地址 = info.address
                                         };
                        dataGridView1.DataSource = resultphone;
                        break;
                }
            }
             
        }



        private void button1_Click(object sender, EventArgs e)
        {
            BindInfo();
        }
        //添加按钮
        private void button2_Click(object sender, EventArgs e)
        {
            linq = new DataClasses1DataContext(strCon); //创建Linq连接对象
            tb_student student = new tb_student();//创建tb_student类对象
            //为tb_student类中的个人信息赋值
            student.id = Convert.ToDouble(textBox2.Text);//序号
            student.name = textBox3.Text;//姓名
            student.sex = comboBox2.Text;//性别
            student.qq = Convert.ToDouble(textBox4.Text);//QQ
            student.weixin = Convert.ToDouble(textBox6.Text);//微信
            student.phone = Convert.ToDouble(textBox7.Text);//联系电话
            student.address = textBox5.Text;//地址
            linq.tb_student.InsertOnSubmit(student);//添加个人信息
            linq.SubmitChanges();//提交操作
            MessageBox.Show("提交成功！");
            AddBindInfo();

        }
        //AddBindInfo添加数据方法
        private void AddBindInfo() {
            linq = new DataClasses1DataContext(strCon);//创建Linq连接对象
            //获取个人所有信息
            var result = from info in linq.tb_student
                         select new
                         {
                             序号 = info.id,
                             姓名 = info.name,
                             性别 = info.sex,
                             联系方式 = info.phone,
                             QQ号 = info.qq,
                             微信号 = info.weixin,
                             家庭详细地址 = info.address
                         };
            dataGridView1.DataSource = result;//对dataGridView1进行数据绑定
        }


        //在DataGridView中选中记录会在相应的文本框中
        string strID = "";
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            strID = Convert.ToString(dataGridView1[0, e.RowIndex].Value).Trim();
            linq = new DataClasses1DataContext(strCon); //创建Linq连接对象
            //获取选中的商品ID
            textBox2.Text = Convert.ToString(dataGridView1[0, e.RowIndex].Value).Trim();
            //根据选中的个人信息序号获取其详细信息，并生产一个表
            var result = from info in linq.tb_student
                         where info.id == Convert.ToDouble(textBox2.Text)
                         select new
                         {
                             id = info.id,
                             name = info.name,
                             sex = info.sex,
                             phone = info.phone,
                             qq = info.qq,
                             weixin = info.weixin,
                             address = info.address
                         };
            //相应的文本框及下拉列表中显示选择商品的信息
            foreach (var item in result)
            {
                textBox2.Text = item.id.ToString();
                textBox3.Text = item.name;
                comboBox2.Text = item.sex;
                textBox4.Text = item.qq.ToString();
                textBox6.Text = item.weixin.ToString();
                textBox7.Text = item.phone.ToString();
                textBox5.Text = item.address;
            }
        }
        //修改按钮
        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                MessageBox.Show("请选择要修改的信息");
                return;
            }
            linq = new DataClasses1DataContext(strCon); //创建Linq连接对象
            //查询要修改的信息
            var resultInfo = from student in linq.tb_student
                         where student.id == Convert.ToDouble(textBox2.Text)
                         select student;
            //查询指定的个人信息
            foreach (tb_student student in resultInfo) {
                student.id = Convert.ToDouble(textBox2.Text);
                student.name = textBox3.Text;
                student.sex = comboBox2.Text;
                student.qq = Convert.ToDouble(textBox4.Text);
                student.weixin = Convert.ToDouble(textBox6.Text);
                student.phone = Convert.ToDouble(textBox7.Text);
                student.address = textBox5.Text;
                
            }
            linq.SubmitChanges();
            MessageBox.Show("修改信息成功");
            AddBindInfo();


        }
        //删除数据
        private void button4_Click(object sender, EventArgs e)
        {
            if (strID == "") {
                MessageBox.Show("请选择要删除的记录");
                return;
            }
            linq = new DataClasses1DataContext(strCon); //创建Linq连接对象
            //查找要删除信息
            var resultDelete = from student in linq.tb_student
                             where student.id == Convert.ToDouble(textBox2.Text)
                             select student;
            linq.tb_student.DeleteAllOnSubmit(resultDelete);
            linq.SubmitChanges();
            MessageBox.Show("删除信息成功");
            AddBindInfo();

        }



    }
}
