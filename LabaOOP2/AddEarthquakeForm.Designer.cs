using System;
using System.Windows.Forms;
using Npgsql;

public class AddEarthquakeForm : Form
{
    private TextBox txtDuration = new TextBox();
    private TextBox txtScale = new TextBox();
    private NumericUpDown numMagnitude = new NumericUpDown();
    private TextBox txtEpicenter = new TextBox();
    private NumericUpDown numGroundwater = new NumericUpDown();
    private NumericUpDown numAftershocks = new NumericUpDown();
    private Button btnSave = new Button();

    public AddEarthquakeForm()
    {
        this.Text = "Добавить землетрясение";
        this.Size = new System.Drawing.Size(400, 300);

        var labels = new[] { "Длительность (ч:м:с)", "Масштабность", "Магнитуда", "Эпицентр", "Изменение грунтовых вод", "Афтершоки" };
        var controls = new Control[] { txtDuration, txtScale, numMagnitude, txtEpicenter, numGroundwater, numAftershocks };

        for (int i = 0; i < labels.Length; i++)
        {
            var lbl = new Label() { Text = labels[i], Location = new System.Drawing.Point(10, 10 + i * 30), AutoSize = true };
            controls[i].Location = new System.Drawing.Point(200, 10 + i * 30);
            controls[i].Width = 150;
            this.Controls.Add(lbl);
            this.Controls.Add(controls[i]);
        }

        btnSave.Text = "Сохранить";
        btnSave.Location = new System.Drawing.Point(130, 210);
        btnSave.Click += BtnSave_Click;
        this.Controls.Add(btnSave);
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        // Валидация
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

        if (string.IsNullOrWhiteSpace(txtEpicenter.Text))
        {
            MessageBox.Show("Введите эпицентр", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        if (numMagnitude.Value <= 0)
        {
            MessageBox.Show("Магнитуда должна быть больше 0", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        if (numGroundwater.Value < 0 || numAftershocks.Value < 0)
        {
            MessageBox.Show("Значения не могут быть отрицательными", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        string connStr = "Host=localhost;Username=docinf;Password=1500;Database=oopLaba2";

        using (var conn = new NpgsqlConnection(connStr))
        {
            conn.Open();

            // Вставка в natural_phenomena
            var cmdPhen = new NpgsqlCommand("INSERT INTO natural_phenomena (duration, scale) VALUES (@d, @s) RETURNING id", conn);
            cmdPhen.Parameters.AddWithValue("d", duration);
            cmdPhen.Parameters.AddWithValue("s", txtScale.Text);
            int phenId = (int)cmdPhen.ExecuteScalar();

            // Вставка в earthquakes
            var cmdEq = new NpgsqlCommand("INSERT INTO earthquakes (natural_phenomenon_id, magnitude, epicenter, groundwater_change, aftershock_count) VALUES (@id, @m, @e, @g, @a)", conn);
            cmdEq.Parameters.AddWithValue("id", phenId);
            cmdEq.Parameters.AddWithValue("m", (int)numMagnitude.Value);
            cmdEq.Parameters.AddWithValue("e", txtEpicenter.Text);
            cmdEq.Parameters.AddWithValue("g", (decimal)numGroundwater.Value);
            cmdEq.Parameters.AddWithValue("a", (int)numAftershocks.Value);
            cmdEq.ExecuteNonQuery();
        }

        MessageBox.Show("Землетрясение добавлено успешно!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
        this.DialogResult = DialogResult.OK;
        this.Close();
    }
}
