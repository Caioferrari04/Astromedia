using Astromedia.DTO;
using RestSharp;

namespace Astromedia.Services;

public class ImgurService
{
    private readonly RestClient client = new("https://api.imgur.com");

    public async Task<RestResponse<Root>> UploadImagem(IFormFile foto)
    {
        var request = new RestRequest("/3/image", Method.Post);
        request.AddHeader("Authorization", $"Client-ID {Environment.GetEnvironmentVariable("CLIENT_ID")}");
        request.AlwaysMultipartFormData = true;

        byte[] arquivo;
        using (var ms = new MemoryStream())
        {
            foto.CopyTo(ms);
            arquivo = ms.ToArray();
        }


        request.AddFile("image", arquivo, foto.FileName);
        RestResponse response = await client.ExecuteAsync(request);
        return client.Deserialize<Root>(response);
    }
}