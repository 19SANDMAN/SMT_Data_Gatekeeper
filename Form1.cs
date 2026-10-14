using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO; // ฟังก์ชันเสริมสำหรับอ่านไฟล์
using System.Text.Json;

namespace SMT_Data_Gatekeeper
{
    public partial class Form1 : Form
    {
        // สร้างตัวแปรสำหรับเก็บข้อมูลตาราง BOM และ XY
        DataTable dtBOM = new DataTable();
        DataTable dtXY = new DataTable();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void dgvResults_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        // --- ฟังก์ชันเสริม: สำหรับอ่านไฟล์ CSV (เวอร์ชันอัปเกรด ดักจับ Error) ---
        private DataTable ReadCSV(string filePath)
        {
            DataTable dt = new DataTable();
            try
            {
                // คำสั่งอ่านไฟล์
                string[] lines = File.ReadAllLines(filePath);
                if (lines.Length > 0)
                {
                    string[] headers = lines[0].Split(',');
                    foreach (string header in headers)
                    {
                        dt.Columns.Add(header.Trim());
                    }
                    for (int i = 1; i < lines.Length; i++)
                    {
                        string[] rows = lines[i].Split(',');
                        dt.Rows.Add(rows);
                    }
                }
            }
            catch (IOException) // ดักจับกรณีไฟล์ถูกเปิดค้างไว้
            {
                MessageBox.Show("ไม่สามารถอ่านไฟล์ได้!\nกรุณาปิดไฟล์ CSV ใน Excel ก่อนแล้วลองใหม่อีกครั้งครับ", "File In Use Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex) // ดักจับ Error ทั่วไปอื่นๆ
            {
                MessageBox.Show("เกิดข้อผิดพลาด: " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }

        // --- โค้ดทำงานของปุ่มที่ 1 ---
        private void btnLoadBOM_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                lblStatus.Text = "Status: Loaded BOM - " + filePath;

                // สั่งให้อ่านไฟล์และเอาข้อมูลไปโชว์ในตาราง
                dtBOM = ReadCSV(filePath);
                dgvResults.DataSource = dtBOM;
            }
        }

        // --- โค้ดทำงานของปุ่มที่ 2 ---
        private void btnLoadXY_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                lblStatus.Text = "Status: Loaded XY Data - " + filePath;

                // สั่งให้อ่านไฟล์และเอาข้อมูลไปโชว์ในตาราง
                dtXY = ReadCSV(filePath);
                dgvResults.DataSource = dtXY;
            }
        }

        private void btnValidate_Click(object sender, EventArgs e)
        {
            // 1. เช็กก่อนว่าโหลดไฟล์มาครบหรือยัง
            if (dtBOM.Rows.Count == 0 || dtXY.Rows.Count == 0)
            {
                MessageBox.Show("Please load both BOM and XY Data first!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. สร้างตารางใหม่สำหรับแสดงผลลัพธ์การตรวจสอบ
            DataTable dtResult = new DataTable();
            dtResult.Columns.Add("Ref_Des");
            dtResult.Columns.Add("Part_Number");
            dtResult.Columns.Add("Status");

            // 3. นำ BOM ไปเช็กใน XY (หาชิ้นส่วนที่พิกัดหายไป)
            foreach (DataRow row in dtBOM.Rows)
            {
                string refDes = row["Ref_Des"].ToString();
                string partNum = row["Part_Number"].ToString();

                // ค้นหาชื่อ Part ในตาราง XY
                DataRow[] xyRows = dtXY.Select($"Ref_Des = '{refDes}'");
                if (xyRows.Length == 0)
                {
                    dtResult.Rows.Add(refDes, partNum, "Error: Missing in XY Data");
                }
                else
                {
                    dtResult.Rows.Add(refDes, partNum, "OK");
                }
            }

            // 4. นำ XY ไปเช็กกลับใน BOM (หาชิ้นส่วนที่เกินมา ไม่มีในใบสั่ง)
            foreach (DataRow row in dtXY.Rows)
            {
                string refDes = row["Ref_Des"].ToString();

                // ค้นหาชื่อ Part ในตาราง BOM
                DataRow[] bomRows = dtBOM.Select($"Ref_Des = '{refDes}'");
                if (bomRows.Length == 0)
                {
                    dtResult.Rows.Add(refDes, "Unknown", "Error: Missing in BOM");
                }
            }

            // 5. นำผลลัพธ์ไปแสดงบน DataGridView
            dgvResults.DataSource = dtResult;

            // 6. ไฮไลต์สี เพื่อให้เห็นจุดที่ผิดปกติชัดเจน
            foreach (DataGridViewRow row in dgvResults.Rows)
            {
                if (row.Cells["Status"].Value != null)
                {
                    string status = row.Cells["Status"].Value.ToString();
                    if (status.Contains("Error"))
                    {
                        row.DefaultCellStyle.BackColor = Color.Red; // ไฮไลต์สีแดงสำหรับ Error
                        row.DefaultCellStyle.ForeColor = Color.White;
                    }
                    else if (status == "OK")
                    {
                        row.DefaultCellStyle.BackColor = Color.LightGreen; // สีเขียวสำหรับ Part ที่ปกติ
                    }
                }
            }

            lblStatus.Text = "Status: Validation Completed!";
        }

        private void btnSendAPI_Click(object sender, EventArgs e)
        {
            // 1. ดึงข้อมูลจากตารางมาเตรียมแพ็ค
            DataTable dtResult = (DataTable)dgvResults.DataSource;
            if (dtResult == null || dtResult.Rows.Count == 0)
            {
                MessageBox.Show("No data to send! Please validate data first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. กรองเอาเฉพาะข้อมูลที่ Error
            var errorList = dtResult.AsEnumerable()
                .Where(row => row.Field<string>("Status").Contains("Error"))
                .Select(row => new
                {
                    RefDes = row.Field<string>("Ref_Des"),
                    PartNumber = row.Field<string>("Part_Number"),
                    ErrorDetail = row.Field<string>("Status")
                }).ToList();

            if (errorList.Count == 0)
            {
                MessageBox.Show("All data is OK. No errors to report.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 3. แพ็คข้อมูลเป็น JSON
            string jsonPayload = JsonSerializer.Serialize(new
            {
                LineNumber = "SMT-Line-01",
                TotalErrors = errorList.Count,
                ErrorItems = errorList
            }, new JsonSerializerOptions { WriteIndented = true });

            // 4. โชว์ JSON ที่แพ็คเสร็จแล้วบนหน้าจอ (จำลองการยิง API)
            MessageBox.Show("JSON Payload Ready for API:\n\n" + jsonPayload, "API Simulation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            lblStatus.Text = "Status: API Payload Generated Successfully!";
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            // 1. เช็กว่าตารางมีข้อมูลให้ Export ไหม
            DataTable dtResult = (DataTable)dgvResults.DataSource;
            if (dtResult == null || dtResult.Rows.Count == 0)
            {
                MessageBox.Show("No data to export! Please validate data first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. เปิดหน้าต่างให้เลือกที่เซฟไฟล์ (SaveFileDialog)
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV files (*.csv)|*.csv|Text files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog.Title = "Save Validation Report";
            saveFileDialog.FileName = "SMT_Validation_Report.csv"; // ตั้งชื่อไฟล์เริ่มต้นให้

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // 3. เริ่มเขียนข้อมูลลงไฟล์
                    using (System.IO.StreamWriter sw = new System.IO.StreamWriter(saveFileDialog.FileName))
                    {
                        // เขียนหัวคอลัมน์
                        sw.WriteLine("Ref_Des,Part_Number,Status");

                        // ดึงข้อมูลแต่ละบรรทัดมาเขียนลงไฟล์
                        foreach (DataRow row in dtResult.Rows)
                        {
                            string refDes = row["Ref_Des"].ToString();
                            string partNum = row["Part_Number"].ToString();
                            string status = row["Status"].ToString();

                            // ใช้เครื่องหมาย "" คล่อมข้อมูลไว้ ป้องกันปัญหาเครื่องหมายลูกน้ำซ้อนกัน
                            sw.WriteLine($"\"{refDes}\",\"{partNum}\",\"{status}\"");
                        }
                    }

                    MessageBox.Show("Report exported successfully!\n\nSaved at: " + saveFileDialog.FileName, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblStatus.Text = "Status: Report Exported Successfully!";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}