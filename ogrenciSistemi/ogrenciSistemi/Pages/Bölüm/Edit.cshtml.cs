using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ogrenciSistemi.Pages.Bölüm
{
    public class EditModel : PageModel
    {
        public BolumInfo bolumInfo = new BolumInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String id = Request.Query["id"];
            try
            {
                String connectionString = "Data Source=DESKTOP-1OMDOOA;Initial Catalog=tablolar;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Bölüm WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                bolumInfo.id = "" + reader.GetInt32(0);
                                bolumInfo.BölümKodu = "" + reader.GetInt32(1);
                                bolumInfo.BölümAdı = reader.GetString(2);

                            }

                        }
                    }
                }
            }

            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            bolumInfo.id = Request.Form["id"];
            bolumInfo.BölümKodu = Request.Form["bölümkodu"];
            bolumInfo.BölümAdı = Request.Form["bölümadı"];

            if (bolumInfo.id.Length == 0 || bolumInfo.BölümKodu.Length == 0 || bolumInfo.BölümAdı.Length == 0)
            {
                errorMessage = "All the field are required";
                return;
            }

            try
            {
                String connectionString = "Data Source=DESKTOP-1OMDOOA;Initial Catalog=tablolar;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE Bölüm" + "SET BölümKodu=@BölümKodu, BölümAdı=@BölümAdı" + "WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@BölümKodu", bolumInfo.BölümKodu);
                        command.Parameters.AddWithValue("@BölümAdı", bolumInfo.BölümAdı);
                        command.Parameters.AddWithValue("@id", bolumInfo.id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("http://localhost:5039/B%C3%B6l%C3%BCm/IndexBolum");

        }
    }
}
