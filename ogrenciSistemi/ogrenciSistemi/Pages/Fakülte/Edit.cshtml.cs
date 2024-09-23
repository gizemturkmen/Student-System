using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ogrenciSistemi.Pages.Fakülte
{
    public class EditModel : PageModel
    {
        public FakulteInfo fakulteInfo = new FakulteInfo();
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
                    String sql = "SELECT * FROM Fakülte WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                fakulteInfo.id = "" + reader.GetInt32(0);
                                fakulteInfo.FakülteKodu = "" + reader.GetInt32(1);
                                fakulteInfo.FakülteAdı = reader.GetString(2);
                                fakulteInfo.BölümSayısı = reader.GetString(3);

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
            fakulteInfo.id = Request.Form["id"];
            fakulteInfo.FakülteKodu = Request.Form["fakültekodu"];
            fakulteInfo.FakülteAdı = Request.Form["fakülteadı"];
            fakulteInfo.BölümSayısı = Request.Form["bölümsayısı"];

            if (fakulteInfo.id.Length == 0 || fakulteInfo.FakülteKodu.Length == 0 || fakulteInfo.FakülteAdı.Length == 0 || fakulteInfo.BölümSayısı.Length == 0)
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
                    String sql = "UPDATE Fakülte" + "SET FakülteKodu=@FakülteKodu, İsim=@İsim, Cinsiyet=@Cinsiyet, Bölüm=@Bölüm" + "WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@FakülteKodu", fakulteInfo.FakülteKodu);
                        command.Parameters.AddWithValue("@FakülteAdı", fakulteInfo.FakülteAdı);
                        command.Parameters.AddWithValue("@BölümSayısı", fakulteInfo.BölümSayısı);
                        command.Parameters.AddWithValue("@id", fakulteInfo.id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("http://localhost:5039/Fak%C3%BClte/IndexFakulte");

        }
    }
}
