using System;
using System.Windows.Forms;
using Npgsql;

public class AddFloodForm : Form
{
    private TextBox txtDuration = new TextBox();
    private TextBox txtScale = new TextBox();
    private NumericUpDown numWaterLevel = new NumericUpDown();
    private NumericUpDown numFlowSpeed = new NumericUpDown();
    private Button btnSave = new Button();

    public AddFloodForm()
    {
        this.Text = "Добавить наводнение";
        this.Size = new System.Drawing.Size(400, 250);

        var labels = new[] { "Длительность (ч:м:с)", "Масштабность", "Уровень воды", "Скорость течения" };
        var controls = new Control[] { txtDuration, txtScale, numWaterLevel, numFlowSpeed };

        for (int i = 0; i < labels.Length; i++)
        {
            var lbl = new Label() { Text = labels[i], Location = new System.Drawing.Point(10, 10 + i * 30), AutoSize = true };
            controls[i].Location = new System.Drawing.Point(200, 10 + i * 30);
            controls[i].Width = 150;
            this.Controls.Add(lbl);
            this.Controls.Add(controls[i]);
        }

        btnSave.Text = "Сохранить";
        btnSave.Location = new System.Drawing.Point(130, 160);
        btnSave.Click += BtnSave_Click;
        this.Controls.Add(btnSave);
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        if (!TimeSpan.TryParse(txtDuration.Text, out TimeSpan duration))
        {
            MessageBox.Show("Введите длительность в формате ч:м:с", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        if (string.IsNullOrWhiteSpace(txtScale.Text))
        {
            MessageBox.Show("Введите масштабность", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        if (numWaterLevel.Value < 0 || numFlowSpeed.Value < 0)
        {
            MessageBox.Show("Значения не могут быть отрицательными", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        string connStr = "Host=localhost;Username=docinf;Password=1500;Database=oopLaba2";

        using var conn = new NpgsqlConnection(connStr);
        conn.Open();

        // 1. Вставка в natural_phenomena
        var insertPhenomenaCmd = new NpgsqlCommand(
            "INSERT INTO natural_phenomena (duration, scale) VALUES (@duration, @scale) RETURNING id", conn);
        insertPhenomenaCmd.Parameters.AddWithValue("duration", duration);
        insertPhenomenaCmd.Parameters.AddWithValue("scale", txtScale.Text);
        int naturalPhenomenonId = (int)insertPhenomenaCmd.ExecuteScalar();

        // 2. Вставка в floods с использованием внешнего ключа
        var insertFloodCmd = new NpgsqlCommand(
            "INSERT INTO floods (natural_phenomenon_id, water_level, flow_speed) VALUES (@npid, @waterLevel, @flowSpeed)", conn);
        insertFloodCmd.Parameters.AddWithValue("npid", naturalPhenomenonId);
        insertFloodCmd.Parameters.AddWithValue("waterLevel", numWaterLevel.Value);
        insertFloodCmd.Parameters.AddWithValue("flowSpeed", numFlowSpeed.Value);
        insertFloodCmd.ExecuteNonQuery();

        this.DialogResult = DialogResult.OK;
        this.Close();
    }
}

