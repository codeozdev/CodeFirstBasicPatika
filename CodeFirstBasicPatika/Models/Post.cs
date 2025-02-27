using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CodeFirstBasicPatika.Models;


public class Post
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public int UserId { get; set; }
    
    [ForeignKey("UserId")]
    
    [JsonIgnore]
    public User User { get; set; }
}


// [JsonIgnore] eklemedigimizde postlari cekerken user bilgileri de cekilir ve bu da bir donguye sebep olur ve 500 hatasi aliriz 