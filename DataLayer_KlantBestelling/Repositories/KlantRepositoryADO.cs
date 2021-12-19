using BusinessLayer_KlantBestelling;
using BusinessLayer_KlantBestelling.Interfaces;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DataLayer_KlantBestelling {
    public class KlantRepositoryADO : IKlantRepository {
        private string connectionString;

        public KlantRepositoryADO(string connectionString) {
            this.connectionString = connectionString;
        }
        private SqlConnection GetConnection() {
            SqlConnection conn = new SqlConnection(connectionString);
            return conn;
        }

        public Klant GeefKlant(int klantId) {
            SqlConnection conn = GetConnection();
            string query = "SELECT * FROM [dbo].klant WHERE KlantId=@KlantId";
            using (SqlCommand cmd = conn.CreateCommand()) {
                try {
                    conn.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@KlantId", klantId);
                    IDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    Klant klant = new Klant((int)reader["KlantId"], (string)reader["Name"], (string)reader["Adres"]);
                    reader.Close();
                    return klant;
                } catch (Exception ex) {
                    throw new KlantRepositoryADOException("GeefKlant ", ex);
                } finally {
                    conn.Close();
                }
            }
        }

        public bool BestaatKlant(int KlantId) {
            SqlConnection conn = GetConnection();
            string sql = "SELECT count(*) FROM [dbo].[Klant] WHERE KlantId = @KlantId";
            using (SqlCommand cmd = conn.CreateCommand()) {
                try {
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@KlantId", KlantId);
                    int n = (int)cmd.ExecuteScalar();
                    if (n > 0) return true; else return false;
                } catch (Exception ex) {
                    throw new KlantRepositoryADOException("BestaatKlant ", ex);
                } finally {
                    conn.Close();
                }
            }
        }
        public bool BestaatKlant(Klant klant) {
            SqlConnection conn = GetConnection();
            string sql = "SELECT count(*) FROM [dbo].[Klant] WHERE Name = @Name AND Adres = @Adres";
            using (SqlCommand cmd = conn.CreateCommand()) {
                try {
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@Name", klant.Name);
                    cmd.Parameters.AddWithValue("@Adres", klant.Adres);
                    int n = (int)cmd.ExecuteScalar();
                    if (n > 0) return true; else return false;
                } catch (Exception ex) {
                    throw new KlantRepositoryADOException("BestaatKlant ", ex);
                } finally {
                    conn.Close();
                }
            }
        }

        public Klant KlantToevoegen(Klant klant) {
            SqlConnection conn = GetConnection();
            string sql = "INSERT INTO [dbo].[Klant] (Name, Adres) OUTPUT INSERTED.KlantId VALUES (@Name, @Adres);";
            using (SqlCommand cmd = conn.CreateCommand()) {
                try {
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.Add("@Name", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@Adres", SqlDbType.NVarChar);
                    cmd.Parameters["@Name"].Value = klant.Name;
                    cmd.Parameters["@Adres"].Value = klant.Adres;
                    int id = (int)cmd.ExecuteScalar();
                    Klant k = new Klant(klant.Name, klant.Adres);
                    k.ZetKlantId(id);
                    return k;
                } catch (Exception ex) {
                    throw new KlantRepositoryADOException("KlantToevoegen ", ex);
                } finally {
                    conn.Close();
                }
            }
        }

        public void VerwijderKlant(int klantId) {
            SqlConnection conn = GetConnection();
            string sql = "DELETE FROM [dbo].Klant WHERE klantId = @klantId";
            using (SqlCommand cmd = conn.CreateCommand()) {
                try {
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@klantId", klantId);
                    cmd.ExecuteNonQuery();
                } catch (Exception ex) {
                    throw new KlantRepositoryADOException("VerwijderKlant ", ex);
                } finally {
                    conn.Close();
                }
            }
        }

        public void KlantUpdaten(Klant klant) {
            SqlConnection conn = GetConnection();
            string sql = "UPDATE [dbo].[Klant] SET Name = @Name, Adres = @Adres WHERE KlantId = @KlantId";
            using (SqlCommand cmd = conn.CreateCommand()) {
                try {
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.Add("@Name", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@Adres", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@KlantId", SqlDbType.Int);
                    cmd.Parameters["@Name"].Value = klant.Name;
                    cmd.Parameters["@Adres"].Value = klant.Adres;
                    cmd.Parameters["@KlantId"].Value = klant.KlantId;
                    cmd.ExecuteNonQuery();
                } catch (Exception ex) {
                    throw new KlantRepositoryADOException("KlantUpdaten " + ex.Message);
                } finally {
                    conn.Close();
                }
            }
        }
    }
}
