using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExcelDataReader;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;


namespace Nera_Converter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }
        

        private void btn1_Click(object sender, EventArgs e)
        {
            OpenFileDialog excelFile = new OpenFileDialog();
            excelFile.ShowDialog();
            excelFile.Title = "Choose Excel File: ";
            excelFile.InitialDirectory = @"C:\";
            excelFile.DefaultExt = "xlsx"+"xls";
            excelFile.CheckFileExists = true;
            excelFile.CheckPathExists = true; 
            if(excelFile.ShowDialog() == DialogResult.OK)
            {
                txtBox1.Text = excelFile.FileName;
                btn2.Enabled = true;    
            }
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            try
            {


                var NenaFile = new Excel.Application();
                NenaFile.DisplayAlerts = false;
                NenaFile.Visible = false;
                var NenaWorkbook = (Excel.Workbook)(NenaFile.Workbooks.Open(txtBox1.Text, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value));
                Excel.Worksheet NenaSheet = NenaWorkbook.Sheets[1];
                Excel.Range rawNenaColumn_N = NenaSheet.Range["N2","N11562"].Value2;
                Excel.Range formattedColumn_StreetDir = NenaSheet.Range["J2", "J11562"].Value2;
                string[] Column_N_Array = rawNenaColumn_N.Value2.ToString();
                string[] Column_J_Array = formattedColumn_StreetDir.Value2.ToString();
                for(int i = 0; i <= Column_N_Array.Length; i++)
                {
                    switch (Column_N_Array[i])
                    {
                        case "S":
                            Column_J_Array[i] = Column_N_Array[i];
                            break;
                        case "N":
                            Column_J_Array[i] = Column_N_Array[i];
                            break;
                        case "E":
                            Column_J_Array[i] = Column_N_Array[i];
                            break;
                        case "W":
                            Column_J_Array[i] = Column_N_Array[i];
                            break;

                    }

                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
            }

            /*ExcelData Reader can't write to excel file 
            var file = new FileInfo(txtBox1.Text);
            using (var stream = new FileStream(txtBox1.Text, FileMode.Open, FileAccess.Read))
            {
                IExcelDataReader excelReader;
                if (file.Extension == ".xls")
                {
                    excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                }
                else if (file.Extension == ".xlsx")
                {
                    excelReader = ExcelReaderFactory.CreateReader(stream);
                }
                else throw new Exception("Invalid File");
                
                while(excelReader.Read())
                {
                    var direction = excelReader.GetValue(13);
                    switch (direction.ToString())
                    {
                        case "S"
                            excelReader.
                            break;
                        case "N":
                            break;
                        case "E":
                            break;
                        case "W":
                            break;
                    }
                }
            }
            */
        }
    }
}
