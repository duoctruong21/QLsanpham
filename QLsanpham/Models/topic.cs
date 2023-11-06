using QLsanpham.Models.EF;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLsanpham.Models
{
    public class topic
    {
        public int Id { get; set; }

        public string? TopicName { get; set; }

        public string? TopicDescription { get; set; }

        public string? TopicImg { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string? Alias { get; set; }
        public IFormFile? Img { get; set; }

        public virtual ICollection<Song> Songs { get; set; } = new List<Song>();

        public virtual ICollection<Topicwithsong> Topicwithsongs { get; set; } = new List<Topicwithsong>();
    }
}
