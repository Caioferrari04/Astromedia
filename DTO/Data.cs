namespace Astromedia.DTO;

public class Data
{
    public string id { get; set; }
    public object title { get; set; }
    public object description { get; set; }
    public int datetime { get; set; }
    public string type { get; set; }
    public bool animated { get; set; }
    public int width { get; set; }
    public int height { get; set; }
    public int size { get; set; }
    public int views { get; set; }
    public int bandwidth { get; set; }
    public object vote { get; set; }
    public bool favorite { get; set; }
    public object nsfw { get; set; }
    public object section { get; set; }
    public object account_url { get; set; }
    public int account_id { get; set; }
    public bool is_ad { get; set; }
    public bool in_most_viral { get; set; }
    public List<object> tags { get; set; }
    public int ad_type { get; set; }
    public string ad_url { get; set; }
    public bool in_gallery { get; set; }
    public string deletehash { get; set; }
    public string name { get; set; }
    public string link { get; set; }
}
