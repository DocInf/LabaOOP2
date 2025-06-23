using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabaOOP2
{
    public class Flood : NaturalPhenomenon
    {
        public decimal WaterLevel { get; set; }
        public decimal FlowSpeed { get; set; }

        private Random rnd = new Random();
        public override void PerformDamageControl()
        {
            MessageBox.Show("Выполнена выкачка остатков воды.");
        }

        public override void Forecast()
        {
        }


        public void Forecast(int id)
        {
            MessageBox.Show($"Прогнозирование выполнено для наводнения ID = {id}");
        }

        public override void AssessDamage()
        {
            MessageBox.Show("Оценка ущерба от наводнения завершена.");
        }

        public override void Evacuate()
        {
        }

        public void Evacuate(int id)
        {
            MessageBox.Show($"Эвакуация выполнена для наводнения ID = {id}");
        }

        public void MeasureWaterLevel(int id, string connStr)
        {
            Random rand = new Random();
            decimal delta = rand.Next(0, 2) == 0 ? -2 : 2;

            using var conn = new Npgsql.NpgsqlConnection(connStr);
            conn.Open();
            using var cmd = new Npgsql.NpgsqlCommand("UPDATE floods SET water_level = water_level + @delta WHERE id = @id", conn);
            cmd.Parameters.AddWithValue("delta", delta);
            cmd.Parameters.AddWithValue("id", id);
            cmd.ExecuteNonQuery();

            MessageBox.Show($"Уровень воды изменён на {delta} у наводнения с ID = {id}.");
        }

        public void TrackPrecipitation(int id)
        {
            MessageBox.Show($"Операция 'Отслеживание осадков' выполнена для наводнения с ID = {id}.");
        }

        public void InstallBarriers(int id)
        {
            MessageBox.Show($"Операция 'Установка барьеров' выполнена для наводнения с ID = {id}.");
        }

    }


}
