using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ogrenciSistemi.Pages.Ders
{
    public class CreateDersModel : PageModel
    {
        public DersInfo dersInfo = new DersInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void Onpost()
        {
            dersInfo.DersKodu = Request.Form["derskodu"];
            dersInfo.DersAd� = Request.Form["dersad�"];
            dersInfo.AKTS = Request.Form["akts"];

            if (dersInfo.DersKodu.Length == 0 || dersInfo.DersAd�.Length == 0 || dersInfo.AKTS.Length == 0)
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
                    String sql = "INSERT INTO Ders" + "(DersKodu, DersAd�, AKTS) VALUES" + "(@DersKodu, @DersAd�, @AKTS);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@DersKodu", dersInfo.DersKodu);
                        command.Parameters.AddWithValue("@DersAd�", dersInfo.DersAd�);
                        command.Parameters.AddWithValue("@AKTS", dersInfo.AKTS);

                        command.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            dersInfo.DersKodu = "";
            dersInfo.DersAd� = "";
            dersInfo.AKTS = "";
            successMessage = "New ��renci added correctly";

            Response.Redirect("http://localhost:5039/Ders/IndexDers");
        }
    }
}
