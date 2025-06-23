using System;
using System.Windows.Forms;
using Npgsql;

namespace LabaOOP2
{
    public class Earthquake : NaturalPhenomenon
    {
        public int Magnitude { get; set; }
        public string Epicenter { get; set; }
        public decimal GroundwaterChange { get; set; }
        public int AftershockCount { get; set; }

        public override void Forecast()
        {
            MessageBox.Show("Прогнозирование землетрясения выполнено.");
        }

        public void Forecast(int id)
        {
            MessageBox.Show($"Прогнозирование выполнено для землетрясения ID = {id}");
        }

        public override void Evacuate()
        {
            MessageBox.Show("Эвакуация при землетрясении выполнена.");
        }

        public void Evacuate(int id)
        {
            MessageBox.Show($"Эвакуация выполнена для землетрясения ID = {id}");
        }

        public override void PerformDamageControl()
        {
            MessageBox.Show("Выполнено восстановление разрушений.");
        }

        public override void AssessDamage()
        {
            MessageBox.Show("Оценка ущерба от землетрясения завершена.");
        }

        public void InstallShockAbsorbers()
        {
            MessageBox.Show("Установлены амортизаторы ударов.");
        }

        public void RestoreDestruction(int id)
        {
            MessageBox.Show($"Операция 'Восстановление разрушений' выполнена для землетрясения ID = {id}.");
        }

        public void MeasureGroundwaterChange(int id, string connStr)
        {
            Random rand = new Random();
            decimal delta = rand.Next(0, 2) == 0 ? -2 : 2;

            using var conn = new NpgsqlConnection(connStr);
            conn.Open();

            using var cmd = new NpgsqlCommand("UPDATE earthquakes SET groundwater_change = groundwater_change + @delta WHERE id = @id", conn);
            cmd.Parameters.AddWithValue("delta", delta);
            cmd.Parameters.AddWithValue("id", id);
            cmd.ExecuteNonQuery();

            MessageBox.Show($"Изменение грунтовых вод: {delta} для землетрясения ID = {id}");
        }

        public void CountAftershocks(int id, string connStr)
        {
            using var conn = new NpgsqlConnection(connStr);
            conn.Open();

            using var cmd = new NpgsqlCommand("UPDATE earthquakes SET aftershock_count = aftershock_count + 1 WHERE id = @id", conn);
            cmd.Parameters.AddWithValue("id", id);
            cmd.ExecuteNonQuery();

            MessageBox.Show($"Афтершоки увеличены на 1 для землетрясения ID = {id}.");
        }
    }
}
