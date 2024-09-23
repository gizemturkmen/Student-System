using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ogrenciSistemi.Pages.Fakülte
{
    public class CreateFakulteModel : PageModel
    {
        public FakulteInfo fakulteInfo = new FakulteInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void Onpost()
        {
            fakulteInfo.FakülteKodu = Request.Form["FakülteKodu"];
            fakulteInfo.FakülteAdı = Request.Form["FakülteAdı"];
            fakulteInfo.BölümSayısı = Request.Form["BölümSayısı"];

            if (fakulteInfo.FakülteKodu.Length == 0 || fakulteInfo.FakülteAdı.Length == 0 || fakulteInfo.BölümSayısı.Length == 0)
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
                    String sql = "INSERT INTO Fakülte" + "(FakülteKodu, FakülteAdı, BölümSayısı) VALUES" + "(@FakülteKodu, @FakülteAdı, @BölümSayısı);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@FakülteKodu", fakulteInfo.FakülteKodu);
                        command.Parameters.AddWithValue("@FakülteAdı", fakulteInfo.FakülteAdı);
                        command.Parameters.AddWithValue("@BölümSayısı", fakulteInfo.BölümSayısı);

                        command.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            fakulteInfo.FakülteKodu = "";
            fakulteInfo.FakülteAdı = "";
            fakulteInfo.BölümSayısı = "";
            successMessage = "New Öğrenci added correctly";

            Response.Redirect("http://localhost:5039/Fak%C3%BClte/IndexFakulte");
        }
    }
}
