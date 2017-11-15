using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using ExcelDataReader; will remove when interop is working. 
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
            //actual loading of sheet. 
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
                //load excel sheet
                var NenaFile = new Excel.Application();
                NenaFile.DisplayAlerts = false;
                NenaFile.Visible = false;
                var NenaWorkbook = NenaFile.Workbooks.Open(txtBox1.Text, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                Excel.Worksheet NenaSheet = NenaWorkbook.Sheets[1];


                //current limit on dv list is 11562
                Excel.Range rawNenaColumn_N = NenaSheet.get_Range("N2", "N11562"); 
                Excel.Range rawNenaColumn_L = NenaSheet.get_Range("J2", "J11562");
                Excel.Range rawNenaColumn_O = NenaSheet.get_Range("O2", "O11562");
                Excel.Range rawNenaColumn_P = NenaSheet.get_Range("P2", "P11562");
                Excel.Range rawNenaColumn_Q = NenaSheet.get_Range("Q2", "Q11562");
                Excel.Range rawNenaColumn_R = NenaSheet.get_Range("R2", "R11562");
                Excel.Range rawNenaColumn_S = NenaSheet.get_Range("S2", "S11562");
                


                //populate arrays from excel sheet, popualte array with cell value(s). 
                Array Column_N = (Array)rawNenaColumn_N.Cells.Value;  
                Array Column_L = (Array)rawNenaColumn_L.Cells.Value;
                Array Column_O = (Array)rawNenaColumn_O.Cells.Value;
                Array Column_P = (Array)rawNenaColumn_P.Cells.Value;
                Array Column_Q = (Array)rawNenaColumn_Q.Cells.Value;
                Array Column_R = (Array)rawNenaColumn_R.Cells.Value;
                Array Column_S = (Array)rawNenaColumn_S.Cells.Value;
                string addr = string.Empty;
                string space = " ";

                //iterate over columns, move data over and aggregate streets seperated by name. 
                for (int r = 1; r <= Column_N.Length; r++)
                {
                    lblCounter.Text = r.ToString();
                    if(Column_N.GetValue(r,1) == null)
                    {
                        Column_N.SetValue(" ", r, 1);
                    }
                    string N_Direction = Column_N.GetValue(r, 1).ToString();
                    //string L_Driection = Column_L.GetValue(r,1).ToString();
                    if (Column_O.GetValue(r, 1) == null)  
                    {
                        Column_O.SetValue(" ", r, 1); 
                    }
                    string col_O = Column_O.GetValue(r,1).ToString();
                    if (Column_P.GetValue(r, 1) == null)
                    {
                        Column_P.SetValue(" ", r, 1);
                    }
                    string col_P = Column_P.GetValue(r,1).ToString();
                    if (Column_Q.GetValue(r, 1) == null)
                    {
                        Column_Q.SetValue(" ", r, 1);
                    }
                    string col_Q = Column_Q.GetValue(r,1).ToString();
                    if (Column_R.GetValue(r, 1) == null)
                    {
                        Column_R.SetValue(" ", r, 1);
                    }
                    string col_R = Column_R.GetValue(r,1).ToString();
                    if (Column_S.GetValue(r, 1) == null)
                    {
                        Column_S.SetValue(" ", r, 1);
                    }
                    string col_S = Column_S.GetValue(r,1).ToString();
                    switch (N_Direction)
                    {
                        case "S":
                        case "N":
                        case "E":
                        case "W":
                        case "SW":
                        case "SE":
                        case "NE":
                        case "NW":
                            {
                                
                                NenaSheet.Cells[RowIndex: $"J{r}"] = N_Direction.ToString();
                            }
                            break;
                        default:
                            {
                                addr += N_Direction + space + col_O + space;
                                addr += col_P + space + col_Q + space + col_R + space;
                                addr += col_S;
                                NenaSheet.Cells[RowIndex: $"J{r}"] = addr;
                            }
                            break;
                    }
                    //Excel.Range rawNenaColumn_K = NenaSheet.get_Range("K2", "K11562");
                    //Excel.Range TestColumnJ = NenaSheet.get_Range("J2","J11562");
                    //MessageBox.Show("Breakpoint!");
                    
                }
                //NenaWorkbook.Save();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
            }
            MessageBox.Show("Done.");
        }
    }
}
