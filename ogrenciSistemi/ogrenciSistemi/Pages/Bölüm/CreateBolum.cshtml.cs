using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ogrenciSistemi.Pages.Bölüm
{
    public class CreateBolumModel : PageModel
    {
        public BolumInfo bolumInfo = new BolumInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void Onpost()
        {
            bolumInfo.BölümKodu = Request.Form["bölümkodu"];
            bolumInfo.BölümAdı = Request.Form["bölümadı"];

            if (bolumInfo.BölümKodu.Length == 0 || bolumInfo.BölümAdı.Length == 0)
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
                    String sql = "INSERT INTO Bölüm" + "(BölümKodu, BölümAdı) VALUES" + "(@BölümKodu, @BölümAdı);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@BölümKodu", bolumInfo.BölümKodu);
                        command.Parameters.AddWithValue("@BölümAdı", bolumInfo.BölümAdı);

                        command.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            bolumInfo.BölümKodu = "";
            bolumInfo.BölümAdı = "";
            successMessage = "New bölüm added correctly";

            Response.Redirect("http://localhost:5039/B%C3%B6l%C3%BCm/IndexBolum");
        }
    }
}
