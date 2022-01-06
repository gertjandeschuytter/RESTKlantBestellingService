using BusinessLayer_KlantBestelling;
using BusinessLayer_KlantBestelling.Interfaces;
using DataLayer_KlantBestelling.Repositories_Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer_KlantBestelling.Repositories {
    public class BestellingRepositoryADO : IBestellingRepository {

        private readonly string _connectionString;

        public BestellingRepositoryADO(string connectieString)
        {
            this._connectionString = connectieString;
        }

        private SqlConnection GetConnection()
        {
            SqlConnection connection = new(_connectionString);
            return connection;
        }


        public IEnumerable<Bestelling> GeefBestellingenKlant(int id)
        {
            SqlConnection conn = GetConnection();
            string sql = "SELECT * FROM bestelling b LEFT JOIN klant k  ON b.KlantId = k.KlantId WHERE k.KlantId = @KlantId";

            using (SqlCommand comm = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    comm.CommandText = sql;
                    comm.Parameters.AddWithValue("@KlantId", id);
                    SqlDataReader reader = comm.ExecuteReader();
                    Klant k = null;
                    List<Bestelling> bestellingen = new List<Bestelling>();
                    while (reader.Read())
                    {
                        if (k == null) k = new Klant((int)reader["KlantId"], (string)reader["Name"], (string)reader["Adres"]);

                        Bestelling b = new(
                            (int)reader["BestellingId"], k, (int)reader["Aantal"], (int)reader["Product"]);
                        bestellingen.Add(b);
                    }
                    reader.Close();
                    return bestellingen;
                }
                catch (Exception ex)
                {
                    throw new BestellingRepositoryADOException("GeefBestellingenKlant", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public bool HeeftKlantBestellingen(int klantId)
        {
            SqlConnection conn = GetConnection();
            string sql = "SELECT COUNT(*) FROM [dbo].Bestelling WHERE KlantId = @klantId";
            using (SqlCommand command = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    command.CommandText = sql;
                    command.Parameters.AddWithValue("@klantId", klantId);
                    int n = (int)command.ExecuteScalar();
                    if (n > 0) return true; else return false;
                }
                catch (Exception ex)
                {
                    throw new BestellingRepositoryADOException("HeeftKlantBestellingen ", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public bool BestaatBestelling(int BestellingId)
        {
            SqlConnection conn = GetConnection();
            string sql = "SELECT count(*) FROM [dbo].Bestelling WHERE BestellingId = @BestellingId";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@BestellingId", BestellingId);
                    int n = (int)cmd.ExecuteScalar();
                    if (n > 0) return true; else return false;
                }
                catch (Exception ex)
                {
                    throw new BestellingRepositoryADOException("BestaatBestelling " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public Bestelling BestellingToevoegen(Bestelling b)
        {
            SqlConnection conn = GetConnection();
            string sql = "INSERT INTO [dbo].Bestelling (Product, Aantal, KlantId) OUTPUT INSERTED.bestellingId VALUES (@Product, @Aantal, @KlantId)";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.Add("@Product", SqlDbType.Int);
                    cmd.Parameters.Add("@Aantal", SqlDbType.Int);
                    cmd.Parameters.Add("@KlantId", SqlDbType.Int);
                    cmd.Parameters["@Product"].Value = (int)b.Product;
                    cmd.Parameters["@Aantal"].Value = b.Aantal;
                    cmd.Parameters["@KlantId"].Value = b.Klant.KlantId;
                    int id = (int)cmd.ExecuteScalar();
                    return new Bestelling(id, b.Klant, b.Aantal, (int)b.Product);
                }
                catch (Exception ex)
                {
                    throw new BestellingRepositoryADOException("BestellingToevoegen " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public Bestelling BestellingTonen(int bestellingId)
        {
            SqlConnection conn = GetConnection();
            string sql = "SELECT * FROM [dbo].Klant k INNER JOIN [dbo].Bestelling b ON k.KlantId = b.KlantId WHERE b.bestellingId = @bestellingId";
            Klant klant = null;
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.Add("@bestellingId", SqlDbType.Int);
                    cmd.Parameters["@bestellingId"].Value = bestellingId;
                    IDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    if (klant == null)
                    {
                        klant = new((int)reader["KlantId"], (string)reader["Name"], (string)reader["Adres"]);
                    }
                    Bestelling bestelling = new((int)reader["bestellingId"], klant, (int)reader["Aantal"], (int)reader["Product"]);
                    reader.Close();
                    return bestelling;
                }
                catch (Exception ex)
                {
                    throw new BestellingRepositoryADOException("BestellingTonen ", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public void VerwijderBestelling(int bestellingId)
        {
            SqlConnection conn = GetConnection();
            string sql = "DELETE FROM [dbo].Bestelling WHERE bestellingId = @bestellingId";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@bestellingId", bestellingId);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new BestellingRepositoryADOException("VerwijderBestelling ", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public void UpdateBestelling(Bestelling bestelling)
        {
            SqlConnection conn = GetConnection();
            string sql = "UPDATE [dbo].Bestelling SET Product = @Product, Aantal = @Aantal, KlantId = @KlantId WHERE BestellingId = @BestellingId";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.Add("@Product", SqlDbType.Int);
                    cmd.Parameters.Add("@Aantal", SqlDbType.Int);
                    cmd.Parameters.Add("@KlantId", SqlDbType.Int);
                    cmd.Parameters.Add("@BestellingId", SqlDbType.Int);
                    cmd.Parameters["@Product"].Value = (int)bestelling.Product;
                    cmd.Parameters["@Aantal"].Value = bestelling.Aantal;
                    cmd.Parameters["@KlantId"].Value = bestelling.Klant.KlantId;
                    cmd.Parameters["@BestellingId"].Value = bestelling.BestellingId;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new BestellingRepositoryADOException("BestellingUpdaten ", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
