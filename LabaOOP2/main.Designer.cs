using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using LabaOOP2;
using Npgsql;

public class MainForm : Form
{
    private DataGridView dgvEarthquakes = new DataGridView();
    private DataGridView dgvFloods = new DataGridView();
    private Button btnAddEarthquake = new Button();
    private Button btnDeleteEarthquake = new Button();
    private Button btnAddFlood = new Button();
    private Button btnDeleteFlood = new Button();
    private Button btnRefresh = new Button();
    private Button btnForecastEarthquake = new Button();
    private Button btnEvacuateEarthquake = new Button();
    private Button btnAftershocksEarthquake = new Button();

    private Button btnForecastFlood = new Button();
    private Button btnEvacuateFlood = new Button();
    private Button btnBarriersFlood = new Button();

    private BindingList<Flood> floods = new BindingList<Flood>();



    private string connStr = "Host=localhost;Username=docinf;Password=1500;Database=oopLaba2";

    public MainForm()
    {
        this.Text = "Natural Disasters";
        this.Size = new System.Drawing.Size(900, 600);

        dgvFloods.DataSource = floods;

        dgvEarthquakes.Location = new System.Drawing.Point(10, 10);
        dgvEarthquakes.Size = new System.Drawing.Size(850, 150);
        dgvFloods.Location = new System.Drawing.Point(10, 210);
        dgvFloods.Size = new System.Drawing.Size(850, 150);

        btnAddEarthquake.Text = "Добавить землетрясение";
        btnAddEarthquake.Location = new System.Drawing.Point(10, 170);
        btnAddEarthquake.Click += BtnAddEarthquake_Click;

        btnDeleteEarthquake.Text = "Удалить землетрясение";
        btnDeleteEarthquake.Location = new System.Drawing.Point(200, 170);
        btnDeleteEarthquake.Click += BtnDeleteEarthquake_Click;

        btnAddFlood.Text = "Добавить наводнение";
        btnAddFlood.Location = new System.Drawing.Point(10, 370);
        btnAddFlood.Click += BtnAddFlood_Click;

        btnDeleteFlood.Text = "Удалить наводнение";
        btnDeleteFlood.Location = new System.Drawing.Point(200, 370);
        btnDeleteFlood.Click += BtnDeleteFlood_Click;

        btnRefresh.Text = "Обновить данные";
        btnRefresh.Location = new System.Drawing.Point(10, 580);
        btnRefresh.Click += BtnRefresh_Click;



        // ==== Группа для Землетрясения ====
        GroupBox gbEarthquakeActions = new GroupBox();
        gbEarthquakeActions.Text = "Действия для землетрясения";
        gbEarthquakeActions.Location = new System.Drawing.Point(10, 460);
        gbEarthquakeActions.Size = new System.Drawing.Size(420, 160);

        // Кнопки землетрясений
        Button btnMeasureGroundwater = new Button();
        btnMeasureGroundwater.Text = "Изм. грунт. воды";
        btnMeasureGroundwater.Location = new System.Drawing.Point(10, 20);
        btnMeasureGroundwater.Size = new System.Drawing.Size(120, 22);
        btnMeasureGroundwater.Click += BtnMeasureGroundwater_Click;

        Button btnRestoreDestruction = new Button();
        btnRestoreDestruction.Text = "Восстановление";
        btnRestoreDestruction.Location = new System.Drawing.Point(140, 20);
        btnRestoreDestruction.Click += BtnRestoreDestruction_Click;

        Button btnCountAftershocks = new Button();
        btnCountAftershocks.Text = "Афтершоки +1";
        btnCountAftershocks.Location = new System.Drawing.Point(270, 20);
        btnCountAftershocks.Click += BtnCountAftershocks_Click;

        Button btnEarthquakeForecast = new Button();
        btnEarthquakeForecast.Text = "Прогноз";
        btnEarthquakeForecast.Location = new System.Drawing.Point(10, 60);
        btnEarthquakeForecast.Click += BtnEarthquakeForecast_Click;

        Button btnEarthquakeEvacuate = new Button();
        btnEarthquakeEvacuate.Text = "Эвакуация";
        btnEarthquakeEvacuate.Location = new System.Drawing.Point(140, 60);
        btnEarthquakeEvacuate.Click += BtnEarthquakeEvacuate_Click;

        // Добавим кнопки в GroupBox
        gbEarthquakeActions.Controls.Add(btnMeasureGroundwater);
        gbEarthquakeActions.Controls.Add(btnRestoreDestruction);
        gbEarthquakeActions.Controls.Add(btnCountAftershocks);
        gbEarthquakeActions.Controls.Add(btnEarthquakeForecast);
        gbEarthquakeActions.Controls.Add(btnEarthquakeEvacuate);

        // Добавим GroupBox на форму
        this.Controls.Add(gbEarthquakeActions);

        // ==== Группа для Наводнения ====
        GroupBox gbFloodActions = new GroupBox();
        gbFloodActions.Text = "Действия для наводнения";
        gbFloodActions.Location = new System.Drawing.Point(450, 460);
        gbFloodActions.Size = new System.Drawing.Size(420, 160);

        // Кнопки наводнений
        Button btnMeasureWaterLevel = new Button();
        btnMeasureWaterLevel.Text = "Изм. уровня воды";
        btnMeasureWaterLevel.Location = new System.Drawing.Point(10, 20);
        btnMeasureWaterLevel.Size = new System.Drawing.Size(120, 22);
        btnMeasureWaterLevel.Click += BtnMeasureWaterLevel_Click;

        Button btnInstallBarriers = new Button();
        btnInstallBarriers.Text = "Барьеры";
        btnInstallBarriers.Location = new System.Drawing.Point(140, 20);
        btnInstallBarriers.Click += BtnInstallBarriers_Click;

        Button btnTrackPrecipitation = new Button();
        btnTrackPrecipitation.Text = "Осадки";
        btnTrackPrecipitation.Location = new System.Drawing.Point(270, 20);
        btnTrackPrecipitation.Click += BtnTrackPrecipitation_Click;

        Button btnFloodForecast = new Button();
        btnFloodForecast.Text = "Прогноз";
        btnFloodForecast.Location = new System.Drawing.Point(10, 60);
        btnFloodForecast.Click += BtnFloodForecast_Click;

        Button btnFloodEvacuate = new Button();
        btnFloodEvacuate.Text = "Эвакуация";
        btnFloodEvacuate.Location = new System.Drawing.Point(140, 60);
        btnFloodEvacuate.Click += BtnFloodEvacuate_Click;

        // Добавим кнопки в GroupBox
        gbFloodActions.Controls.Add(btnMeasureWaterLevel);
        gbFloodActions.Controls.Add(btnInstallBarriers);
        gbFloodActions.Controls.Add(btnTrackPrecipitation);
        gbFloodActions.Controls.Add(btnFloodForecast);
        gbFloodActions.Controls.Add(btnFloodEvacuate);

        // Добавим GroupBox на форму
        this.Controls.Add(gbFloodActions);
        this.Controls.Add(dgvEarthquakes);
        this.Controls.Add(dgvFloods);
        this.Controls.Add(btnAddEarthquake);
        this.Controls.Add(btnDeleteEarthquake);
        this.Controls.Add(btnAddFlood);
        this.Controls.Add(btnDeleteFlood);
        this.Controls.Add(btnRefresh);

        LoadData();
    }

    private void LoadData()
    {
        using var conn = new NpgsqlConnection(connStr);
        conn.Open();

        var cmd = new NpgsqlCommand(@"
        SELECT 
            e.id, 
            np.duration, 
            np.scale, 
            e.magnitude, 
            e.epicenter, 
            e.groundwater_change, 
            e.aftershock_count 
        FROM earthquakes e
        JOIN natural_phenomena np ON e.natural_phenomenon_id = np.id", conn);

        using var reader = cmd.ExecuteReader();
        var dt = new DataTable();
        dt.Load(reader);

        foreach (DataColumn column in dt.Columns)
        {
            if (column.ColumnName == "id") column.ReadOnly = true; // ID не редактируем
            else column.ReadOnly = false; // Остальные — редактируемы
        }

        dgvEarthquakes.DataSource = dt;

        // Настройка заголовков (по желанию)
        dgvEarthquakes.ReadOnly = false;
        dgvEarthquakes.AllowUserToAddRows = false;
        dgvEarthquakes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvEarthquakes.CellValueChanged += DgvEarthquakes_CellValueChanged;

        dgvEarthquakes.Columns["id"].HeaderText = "ID";
        dgvEarthquakes.Columns["duration"].HeaderText = "Длительность";
        dgvEarthquakes.Columns["scale"].HeaderText = "Масштабность";
        dgvEarthquakes.Columns["magnitude"].HeaderText = "Магнитуда";
        dgvEarthquakes.Columns["epicenter"].HeaderText = "Эпицентр";
        dgvEarthquakes.Columns["groundwater_change"].HeaderText = "Изм. грунтовых вод";
        dgvEarthquakes.Columns["aftershock_count"].HeaderText = "Афтершоки";

        var cmdFloods = new NpgsqlCommand(@"
        SELECT 
            f.id,
            np.duration,
            np.scale,
            f.water_level,
            f.flow_speed
        FROM floods f
        JOIN natural_phenomena np ON f.natural_phenomenon_id = np.id", conn);

        using var readerFloods = cmdFloods.ExecuteReader();
        var dtFloods = new DataTable();
        dtFloods.Load(readerFloods);
        dgvFloods.DataSource = dtFloods;

        // Убираем только-чтение в DataTable
        foreach (DataColumn column in dtFloods.Columns)
        {
            if (column.ColumnName != "id")
                column.ReadOnly = false;
        }

        // Привязка к DataGridView
        dgvFloods.DataSource = dtFloods;

        // Настройка грида
        foreach (DataGridViewColumn column in dgvFloods.Columns)
        {
            if (column.Name != "id")
                column.ReadOnly = false;
        }

        dgvFloods.ReadOnly = false;
        dgvFloods.AllowUserToAddRows = false;
        dgvFloods.AllowUserToDeleteRows = false;
        dgvFloods.CellValueChanged += DgvFloods_CellValueChanged;
        dgvFloods.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;


        dgvFloods.Columns["id"].HeaderText = "ID";
        dgvFloods.Columns["duration"].HeaderText = "Длительность";
        dgvFloods.Columns["scale"].HeaderText = "Масштабность";
        dgvFloods.Columns["water_level"].HeaderText = "Уровень воды";
        dgvFloods.Columns["flow_speed"].HeaderText = "Скорость течения";
    }

    private void DgvEarthquakes_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
        if (dgvEarthquakes.CurrentRow != null)
        {
            try
            {
                int id = (int)dgvEarthquakes.CurrentRow.Cells["id"].Value;
                string duration = dgvEarthquakes.CurrentRow.Cells["duration"].Value.ToString();
                string scale = dgvEarthquakes.CurrentRow.Cells["scale"].Value.ToString();
                int magnitude = Convert.ToInt32(dgvEarthquakes.CurrentRow.Cells["magnitude"].Value);
                string epicenter = dgvEarthquakes.CurrentRow.Cells["epicenter"].Value.ToString();
                decimal groundwaterChange = Convert.ToDecimal(dgvEarthquakes.CurrentRow.Cells["groundwater_change"].Value);
                int aftershockCount = Convert.ToInt32(dgvEarthquakes.CurrentRow.Cells["aftershock_count"].Value);

                using var conn = new NpgsqlConnection(connStr);
                conn.Open();

                using var cmd = new NpgsqlCommand(@"
                UPDATE natural_phenomena 
                SET duration = @duration, scale = @scale 
                WHERE id = (SELECT natural_phenomenon_id FROM earthquakes WHERE id = @id);

                UPDATE earthquakes
                SET magnitude = @magnitude,
                    epicenter = @epicenter,
                    groundwater_change = @groundwater_change,
                    aftershock_count = @aftershock_count
                WHERE id = @id;
            ", conn);

                cmd.Parameters.AddWithValue("id", id);
                cmd.Parameters.AddWithValue("duration", TimeSpan.Parse(duration));
                cmd.Parameters.AddWithValue("scale", scale);
                cmd.Parameters.AddWithValue("magnitude", magnitude);
                cmd.Parameters.AddWithValue("epicenter", epicenter);
                cmd.Parameters.AddWithValue("groundwater_change", groundwaterChange);
                cmd.Parameters.AddWithValue("aftershock_count", aftershockCount);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении изменений: " + ex.Message);
            }
        }
    }

    private void DgvFloods_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
        if (dgvFloods.CurrentRow != null)
        {
            try
            {
                int id = (int)dgvFloods.CurrentRow.Cells["id"].Value;
                string duration = dgvFloods.CurrentRow.Cells["duration"].Value.ToString();
                string scale = dgvFloods.CurrentRow.Cells["scale"].Value.ToString();
                decimal waterLevel = Convert.ToDecimal(dgvFloods.CurrentRow.Cells["water_level"].Value);
                decimal flowSpeed = Convert.ToDecimal(dgvFloods.CurrentRow.Cells["flow_speed"].Value);

                using var conn = new NpgsqlConnection(connStr);
                conn.Open();

                using var cmd = new NpgsqlCommand(@"
                UPDATE natural_phenomena 
                SET duration = @duration, scale = @scale 
                WHERE id = (SELECT natural_phenomenon_id FROM floods WHERE id = @id);
                
                UPDATE floods
                SET water_level = @waterLevel, flow_speed = @flowSpeed
                WHERE id = @id;
            ", conn);

                cmd.Parameters.AddWithValue("id", id);
                cmd.Parameters.AddWithValue("duration", TimeSpan.Parse(duration));
                cmd.Parameters.AddWithValue("scale", scale);
                cmd.Parameters.AddWithValue("waterLevel", waterLevel);
                cmd.Parameters.AddWithValue("flowSpeed", flowSpeed);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении изменений: " + ex.Message);
            }
        }
    }



    private void BtnAddEarthquake_Click(object sender, EventArgs e)
    {
        var form = new AddEarthquakeForm();
        if (form.ShowDialog() == DialogResult.OK)
        {
            LoadData();
        }
    }

    private void BtnDeleteEarthquake_Click(object sender, EventArgs e)
    {
        if (dgvEarthquakes.CurrentRow != null)
        {
            int id = (int)dgvEarthquakes.CurrentRow.Cells["id"].Value;
            using var conn = new NpgsqlConnection(connStr);
            conn.Open();
            new NpgsqlCommand($"DELETE FROM earthquakes WHERE id = {id}", conn).ExecuteNonQuery();
            LoadData();
        }
    }

    private void BtnAddFlood_Click(object sender, EventArgs e)
    {
        var form = new AddFloodForm();
        if (form.ShowDialog() == DialogResult.OK)
        {
            LoadData();
        }
    }

    private void BtnDeleteFlood_Click(object sender, EventArgs e)
    {
        if (dgvFloods.CurrentRow != null)
        {
            int id = (int)dgvFloods.CurrentRow.Cells["id"].Value;
            using var conn = new NpgsqlConnection(connStr);
            conn.Open();
            new NpgsqlCommand($"DELETE FROM floods WHERE id = {id}", conn).ExecuteNonQuery();
            LoadData();
        }
    }

    // === Earthquake ===
    private void BtnMeasureGroundwater_Click(object sender, EventArgs e)
    {
        if (dgvEarthquakes.CurrentRow != null)
        {
            int id = (int)dgvEarthquakes.CurrentRow.Cells["id"].Value;
            new Earthquake().MeasureGroundwaterChange(id, connStr);
            LoadData();
        }
    }

    private void BtnRestoreDestruction_Click(object sender, EventArgs e)
    {
        if (dgvEarthquakes.CurrentRow != null)
        {
            int id = (int)dgvEarthquakes.CurrentRow.Cells["id"].Value;
            new Earthquake().RestoreDestruction(id);
        }
    }

    private void BtnCountAftershocks_Click(object sender, EventArgs e)
    {
        if (dgvEarthquakes.CurrentRow != null)
        {
            int id = (int)dgvEarthquakes.CurrentRow.Cells["id"].Value;
            new Earthquake().CountAftershocks(id, connStr);
            LoadData();
        }
    }

    private void BtnEarthquakeForecast_Click(object sender, EventArgs e)
    {
        if (dgvEarthquakes.CurrentRow != null)
        {
            int id = (int)dgvEarthquakes.CurrentRow.Cells["id"].Value;
            new Earthquake().Forecast(id);
        }
    }

    private void BtnEarthquakeEvacuate_Click(object sender, EventArgs e)
    {
        if (dgvEarthquakes.CurrentRow != null)
        {
            int id = (int)dgvEarthquakes.CurrentRow.Cells["id"].Value;
            new Earthquake().Evacuate(id);
        }
    }

    // === Flood ===
    private void BtnMeasureWaterLevel_Click(object sender, EventArgs e)
    {
        if (dgvFloods.CurrentRow != null)
        {
            int id = (int)dgvFloods.CurrentRow.Cells["id"].Value;
            new Flood().MeasureWaterLevel(id, connStr);
            LoadData();
        }
    }

    private void BtnInstallBarriers_Click(object sender, EventArgs e)
    {
        if (dgvFloods.CurrentRow != null)
        {
            int id = (int)dgvFloods.CurrentRow.Cells["id"].Value;
            new Flood().InstallBarriers(id);
        }
    }

    private void BtnTrackPrecipitation_Click(object sender, EventArgs e)
    {
        if (dgvFloods.CurrentRow != null)
        {
            int id = (int)dgvFloods.CurrentRow.Cells["id"].Value;
            new Flood().TrackPrecipitation(id);
        }
    }

    private void BtnFloodForecast_Click(object sender, EventArgs e)
    {
        if (dgvFloods.CurrentRow != null)
        {
            int id = (int)dgvFloods.CurrentRow.Cells["id"].Value;
            new Flood().Forecast(id);
        } 
    }

    private void BtnFloodEvacuate_Click(object sender, EventArgs e)
    {
        if (dgvFloods.CurrentRow != null)
        {
            int id = (int)dgvFloods.CurrentRow.Cells["id"].Value;
            new Flood().Evacuate(id);
        }
    }




    private void BtnRefresh_Click(object sender, EventArgs e)
    {
        LoadData();
    }
}