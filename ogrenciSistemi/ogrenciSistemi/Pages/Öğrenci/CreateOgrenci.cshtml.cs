using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ogrenciSistemi.Pages.Öğrenci
{
    public class CreateOgrenciModel : PageModel
    {
        public OgrenciInfo ogrenciInfo = new OgrenciInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void Onpost()
        {
            ogrenciInfo.ÖğrenciNo = Request.Form["öğrencino"];
            ogrenciInfo.İsim = Request.Form["isim"];
            ogrenciInfo.Cinsiyet = Request.Form["cinsiyet"];
            ogrenciInfo.Bölüm = Request.Form["bölüm"];

            if (ogrenciInfo.ÖğrenciNo.Length == 0 || ogrenciInfo.İsim.Length == 0 || ogrenciInfo.Cinsiyet.Length == 0 || ogrenciInfo.Bölüm.Length == 0)
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
                    String sql = "INSERT INTO Ögrenci" + "(ÖgrenciNo, İsim, Cinsiyet, Bölüm) VALUES" + "(@ÖgrenciNo, @İsim, @Cinsiyet, @Bölüm);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ÖgrenciNo", ogrenciInfo.ÖğrenciNo);
                        command.Parameters.AddWithValue("@İsim", ogrenciInfo.İsim);
                        command.Parameters.AddWithValue("@Cinsiyet", ogrenciInfo.Cinsiyet);
                        command.Parameters.AddWithValue("@Bölüm", ogrenciInfo.Bölüm);

                        command.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            ogrenciInfo.ÖğrenciNo = "";
            ogrenciInfo.İsim = "";
            ogrenciInfo.Cinsiyet = "";
            ogrenciInfo.Bölüm = "";
            successMessage = "New Öğrenci added correctly";

            Response.Redirect("http://localhost:5039/%C3%96%C4%9Frenci/IndexOgrenci");
        }

    }
}
