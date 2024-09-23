using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ogrenciSistemi.Pages.Bölüm;
using System.Data.SqlClient;

namespace ogrenciSistemi.Pages.Ders
{
    public class EditModel : PageModel
    {
        public DersInfo dersInfo = new DersInfo();
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
                    String sql = "SELECT * FROM Ders WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader readerr = command.ExecuteReader())
                        {
                            if (readerr.Read())
                            {
                                dersInfo.id = "" + readerr.GetInt32(0);
                                dersInfo.DersKodu = "" + readerr.GetInt32(1);
                                dersInfo.DersAdý = readerr.GetString(2);
                                dersInfo.AKTS = "" + readerr.GetInt32(3);

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
            dersInfo.id = Request.Form["id"];
            dersInfo.DersKodu = Request.Form["derskodu"];
            dersInfo.DersAdý = Request.Form["dersadý"];
            dersInfo.AKTS = Request.Form["akts"];

            if (dersInfo.id.Length == 0 || dersInfo.DersKodu.Length == 0 || dersInfo.DersAdý.Length == 0 || dersInfo.AKTS.Length == 0)
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
                    String sql = "UPDATE Ders" + "SET DersKodu=@DersKodu, DersAdý=@DersAdý, AKTS=@AKTS" + "WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@DersKodu", dersInfo.DersKodu);
                        command.Parameters.AddWithValue("@DersAdý", dersInfo.DersAdý);
                        command.Parameters.AddWithValue("@AKTS", dersInfo.AKTS);
                        command.Parameters.AddWithValue("@id", dersInfo.id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("http://localhost:5039/Ders/IndexDers");

        }
    }
}
